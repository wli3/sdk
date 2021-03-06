// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using System.Transactions;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.ShellShim;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.DotNet.Workloads.Workload.Common;
using Microsoft.DotNet.Workloads.Workload.Install;
using Microsoft.DotNet.Workloads.Workload.Uninstall;
using Microsoft.Extensions.EnvironmentAbstractions;
using NuGet.Versioning;

namespace Microsoft.DotNet.Workloads.Workload.Update
{
    internal delegate IShellShimRepository CreateShellShimRepository(DirectoryPath? nonGlobalLocation = null);

    internal delegate (IWorkloadPackageStore, IWorkloadPackageStoreQuery, IWorkloadPackageInstaller, IWorkloadPackageUninstaller) CreateWorkloadPackageStoresAndInstallerAndUninstaller(
        DirectoryPath? nonGlobalLocation = null,
		IEnumerable<string> additionalRestoreArguments = null);

    internal class WorkloadUpdateGlobalOrWorkloadPathCommand : CommandBase
    {
        private readonly IReporter _reporter;
        private readonly IReporter _errorReporter;
        private readonly CreateShellShimRepository _createShellShimRepository;
        private readonly CreateWorkloadPackageStoresAndInstallerAndUninstaller _createWorkloadPackageStoreInstallerUninstaller;

        private readonly PackageId _packageId;
        private readonly string _configFilePath;
        private readonly string _framework;
        private readonly string[] _additionalFeeds;
        private readonly bool _global;
        private readonly string _verbosity;
        private readonly string _workloadPath;
        private readonly IEnumerable<string> _forwardRestoreArguments;
        private readonly string _packageVersion;

        public WorkloadUpdateGlobalOrWorkloadPathCommand(ParseResult parseResult,
            CreateWorkloadPackageStoresAndInstallerAndUninstaller createWorkloadPackageStoreInstallerUninstaller = null,
            CreateShellShimRepository createShellShimRepository = null,
            IReporter reporter = null)
            : base(parseResult)
        {
            _packageId = new PackageId(parseResult.ValueForArgument<string>(WorkloadUninstallCommandParser.PackageIdArgument));
            _configFilePath = parseResult.ValueForOption<string>(WorkloadUpdateCommandParser.ConfigOption);
            _framework = parseResult.ValueForOption<string>(WorkloadUpdateCommandParser.FrameworkOption);
            _additionalFeeds = parseResult.ValueForOption<string[]>(WorkloadUpdateCommandParser.AddSourceOption);
            _packageVersion = parseResult.ValueForOption<string>(WorkloadUpdateCommandParser.VersionOption);
            _global = parseResult.ValueForOption<bool>(WorkloadUpdateCommandParser.GlobalOption);
            _verbosity = Enum.GetName(parseResult.ValueForOption<VerbosityOptions>(WorkloadUpdateCommandParser.VerbosityOption));
            _workloadPath = parseResult.ValueForOption<string>(WorkloadUpdateCommandParser.WorkloadPathOption);
            _forwardRestoreArguments = parseResult.OptionValuesToBeForwarded(WorkloadUpdateCommandParser.GetCommand());

            _createWorkloadPackageStoreInstallerUninstaller = createWorkloadPackageStoreInstallerUninstaller ??
                                                  WorkloadPackageFactory.CreateWorkloadPackageStoresAndInstallerAndUninstaller;

            _createShellShimRepository =
                createShellShimRepository ?? ShellShimRepositoryFactory.CreateShellShimRepository;

            _reporter = (reporter ?? Reporter.Output);
            _errorReporter = (reporter ?? Reporter.Error);
        }

        public override int Execute()
        {
            ValidateArguments();

            DirectoryPath? workloadPath = null;
            if (!string.IsNullOrEmpty(_workloadPath))
            {
                workloadPath = new DirectoryPath(_workloadPath);
            }

            VersionRange versionRange = null;
            if (!string.IsNullOrEmpty(_packageVersion) && !VersionRange.TryParse(_packageVersion, out versionRange))
            {
                throw new GracefulException(
                    string.Format(
                        LocalizableStrings.InvalidNuGetVersionRange,
                        _packageVersion));
            }

            (IWorkloadPackageStore workloadPackageStore,
             IWorkloadPackageStoreQuery workloadPackageStoreQuery,
             IWorkloadPackageInstaller workloadPackageInstaller,
             IWorkloadPackageUninstaller workloadPackageUninstaller) = _createWorkloadPackageStoreInstallerUninstaller(workloadPath, _forwardRestoreArguments);

            IShellShimRepository shellShimRepository = _createShellShimRepository(workloadPath);

            IWorkloadPackage oldPackageNullable = GetOldPackage(workloadPackageStoreQuery);

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                TimeSpan.Zero))
            {
                if (oldPackageNullable != null)
                {
                    RunWithHandlingUninstallError(() =>
                    {
                        foreach (RestoredCommand command in oldPackageNullable.Commands)
                        {
                            shellShimRepository.RemoveShim(command.Name);
                        }

                        workloadPackageUninstaller.Uninstall(oldPackageNullable.PackageDirectory);
                    });
                }

                RunWithHandlingInstallError(() =>
                {
                    IWorkloadPackage newInstalledPackage = workloadPackageInstaller.InstallPackage(
                        new PackageLocation(nugetConfig: GetConfigFile(), additionalFeeds: _additionalFeeds),
                        packageId: _packageId,
                        targetFramework: _framework,
                        versionRange: versionRange,
                        verbosity: _verbosity);

                    EnsureVersionIsHigher(oldPackageNullable, newInstalledPackage);

                    foreach (RestoredCommand command in newInstalledPackage.Commands)
                    {
                        shellShimRepository.CreateShim(command.Executable, command.Name);
                    }

                    PrintSuccessMessage(oldPackageNullable, newInstalledPackage);
                });

                scope.Complete();
            }

            return 0;
        }

        private static void EnsureVersionIsHigher(IWorkloadPackage oldPackageNullable, IWorkloadPackage newInstalledPackage)
        {
            if (oldPackageNullable != null && (newInstalledPackage.Version < oldPackageNullable.Version))
            {
                throw new GracefulException(
                    new[]
                    {
                        string.Format(LocalizableStrings.UpdateToLowerVersion,
                            newInstalledPackage.Version.ToNormalizedString(),
                            oldPackageNullable.Version.ToNormalizedString())
                    },
                    isUserError: false);
            }
        }

        private void ValidateArguments()
        {
            if (!string.IsNullOrEmpty(_configFilePath) && !File.Exists(_configFilePath))
            {
                throw new GracefulException(
                    string.Format(
                        LocalizableStrings.NuGetConfigurationFileDoesNotExist,
                        Path.GetFullPath(_configFilePath)));
            }
        }

        private void RunWithHandlingInstallError(Action installAction)
        {
            try
            {
                installAction();
            }
            catch (Exception ex)
                when (InstallWorkloadCommandLowLevelErrorConverter.ShouldConvertToUserFacingError(ex))
            {
                var message = new List<string>
                {
                    string.Format(LocalizableStrings.UpdateWorkloadFailed, _packageId)
                };
                message.AddRange(
                    InstallWorkloadCommandLowLevelErrorConverter.GetUserFacingMessages(ex, _packageId));


                throw new GracefulException(
                    messages: message,
                    verboseMessages: new[] { ex.ToString() },
                    isUserError: false);
            }
        }

        private void RunWithHandlingUninstallError(Action uninstallAction)
        {
            try
            {
                uninstallAction();
            }
            catch (Exception ex)
                when (WorkloadUninstallCommandLowLevelErrorConverter.ShouldConvertToUserFacingError(ex))
            {
                var message = new List<string>
                {
                    string.Format(LocalizableStrings.UpdateWorkloadFailed, _packageId)
                };
                message.AddRange(
                    WorkloadUninstallCommandLowLevelErrorConverter.GetUserFacingMessages(ex, _packageId));

                throw new GracefulException(
                    messages: message,
                    verboseMessages: new[] { ex.ToString() },
                    isUserError: false);
            }
        }

        private FilePath? GetConfigFile()
        {
            FilePath? configFile = null;
            if (!string.IsNullOrEmpty(_configFilePath))
            {
                configFile = new FilePath(_configFilePath);
            }

            return configFile;
        }

        private IWorkloadPackage GetOldPackage(IWorkloadPackageStoreQuery workloadPackageStoreQuery)
        {
            IWorkloadPackage oldPackageNullable;
            try
            {
                oldPackageNullable = workloadPackageStoreQuery.EnumeratePackageVersions(_packageId).SingleOrDefault();
            }
            catch (InvalidOperationException)
            {
                throw new GracefulException(
                    messages: new[]
                    {
                        string.Format(
                            LocalizableStrings.WorkloadHasMultipleVersionsInstalled,
                            _packageId),
                    },
                    isUserError: false);
            }

            return oldPackageNullable;
        }

        private void PrintSuccessMessage(IWorkloadPackage oldPackage, IWorkloadPackage newInstalledPackage)
        {
            if (oldPackage == null)
            {
                _reporter.WriteLine(
                    string.Format(
                        Install.LocalizableStrings.InstallationSucceeded,
                        string.Join(", ", newInstalledPackage.Commands.Select(c => c.Name)),
                        newInstalledPackage.Id,
                        newInstalledPackage.Version.ToNormalizedString()).Green());
            }
            else if (oldPackage.Version != newInstalledPackage.Version)
            {
                _reporter.WriteLine(
                    string.Format(
                        LocalizableStrings.UpdateSucceeded,
                        newInstalledPackage.Id,
                        oldPackage.Version.ToNormalizedString(),
                        newInstalledPackage.Version.ToNormalizedString()).Green());
            }
            else
            {
                _reporter.WriteLine(
                    string.Format(
                        LocalizableStrings.UpdateSucceededVersionNoChange,
                        newInstalledPackage.Id, newInstalledPackage.Version).Green());
            }
        }
    }
}
