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
using Microsoft.Extensions.EnvironmentAbstractions;
using NuGet.Versioning;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal delegate IShellShimRepository CreateShellShimRepository(DirectoryPath? nonGlobalLocation = null);
    internal delegate (IWorkloadPackageStore, IWorkloadPackageStoreQuery, IWorkloadPackageInstaller) CreateWorkloadPackageStoresAndInstaller(
		DirectoryPath? nonGlobalLocation = null,
		IEnumerable<string> forwardRestoreArguments = null);

    internal class WorkloadInstallGlobalOrWorkloadPathCommand : CommandBase
    {
        private readonly IEnvironmentPathInstruction _environmentPathInstruction;
        private readonly IReporter _reporter;
        private readonly IReporter _errorReporter;
        private CreateShellShimRepository _createShellShimRepository;
        private CreateWorkloadPackageStoresAndInstaller _createWorkloadPackageStoresAndInstaller;

        private readonly PackageId _packageId;
        private readonly string _packageVersion;
        private readonly string _configFilePath;
        private readonly string _framework;
        private readonly string[] _source;
        private readonly bool _global;
        private readonly string _verbosity;
        private readonly string _workloadPath;
        private IEnumerable<string> _forwardRestoreArguments;

        public WorkloadInstallGlobalOrWorkloadPathCommand(
            ParseResult parseResult,
            CreateWorkloadPackageStoresAndInstaller createWorkloadPackageStoreAndInstaller = null,
            CreateShellShimRepository createShellShimRepository = null,
            IEnvironmentPathInstruction environmentPathInstruction = null,
            IReporter reporter = null)
            : base(parseResult)
        {
            _packageId = new PackageId(parseResult.ValueForArgument<string>(WorkloadInstallCommandParser.PackageIdArgument));
            _packageVersion = parseResult.ValueForOption<string>(WorkloadInstallCommandParser.VersionOption);
            _configFilePath = parseResult.ValueForOption<string>(WorkloadInstallCommandParser.ConfigOption);
            _framework = parseResult.ValueForOption<string>(WorkloadInstallCommandParser.FrameworkOption);
            _source = parseResult.ValueForOption<string[]>(WorkloadInstallCommandParser.AddSourceOption);
            _global = parseResult.ValueForOption<bool>(WorkloadAppliedOption.GlobalOptionAliases.First());
            _verbosity = Enum.GetName(parseResult.ValueForOption<VerbosityOptions>(WorkloadInstallCommandParser.VerbosityOption));
            _workloadPath = parseResult.ValueForOption<string>(WorkloadAppliedOption.WorkloadPathOptionAlias);

            _createWorkloadPackageStoresAndInstaller = createWorkloadPackageStoreAndInstaller ?? WorkloadPackageFactory.CreateWorkloadPackageStoresAndInstaller;
			_forwardRestoreArguments = parseResult.OptionValuesToBeForwarded(WorkloadInstallCommandParser.GetCommand());

            _environmentPathInstruction = environmentPathInstruction
                ?? EnvironmentPathFactory.CreateEnvironmentPathInstruction();
            _createShellShimRepository = createShellShimRepository ?? ShellShimRepositoryFactory.CreateShellShimRepository;

            _reporter = (reporter ?? Reporter.Output);
            _errorReporter = (reporter ?? Reporter.Error);
        }

        public override int Execute()
        {
            if (!string.IsNullOrEmpty(_configFilePath) && !File.Exists(_configFilePath))
            {
                throw new GracefulException(
                    string.Format(
                        LocalizableStrings.NuGetConfigurationFileDoesNotExist,
                        Path.GetFullPath(_configFilePath)));
            }

            VersionRange versionRange = null;
            if (!string.IsNullOrEmpty(_packageVersion) && !VersionRange.TryParse(_packageVersion, out versionRange))
            {
                throw new GracefulException(
                    string.Format(
                        LocalizableStrings.InvalidNuGetVersionRange,
                        _packageVersion));
            }

            DirectoryPath? workloadPath = null;
            if (!string.IsNullOrEmpty(_workloadPath))
            {
                workloadPath = new DirectoryPath(_workloadPath);
            }

            (IWorkloadPackageStore workloadPackageStore, IWorkloadPackageStoreQuery workloadPackageStoreQuery, IWorkloadPackageInstaller workloadPackageInstaller) =
                _createWorkloadPackageStoresAndInstaller(workloadPath, _forwardRestoreArguments);
            IShellShimRepository shellShimRepository = _createShellShimRepository(workloadPath);

            // Prevent installation if any version of the package is installed
            if (workloadPackageStoreQuery.EnumeratePackageVersions(_packageId).FirstOrDefault() != null)
            {
                _errorReporter.WriteLine(string.Format(LocalizableStrings.WorkloadAlreadyInstalled, _packageId).Red());
                return 1;
            }

            FilePath? configFile = null;
            if (!string.IsNullOrEmpty(_configFilePath))
            {
                configFile = new FilePath(_configFilePath);
            }

            try
            {
                IWorkloadPackage package = null;
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.Zero))
                {
                    package = workloadPackageInstaller.InstallPackage(
                        new PackageLocation(nugetConfig: configFile, additionalFeeds: _source),
                        packageId: _packageId,
                        versionRange: versionRange,
                        targetFramework: _framework, verbosity: _verbosity);

                    foreach (var command in package.Commands)
                    {
                        shellShimRepository.CreateShim(command.Executable, command.Name, package.PackagedShims);
                    }

                    scope.Complete();
                }

                foreach (string w in package.Warnings)
                {
                    _reporter.WriteLine(w.Yellow());
                }

                if (_global)
                {
                    _environmentPathInstruction.PrintAddPathInstructionIfPathDoesNotExist();
                }

                _reporter.WriteLine(
                    string.Format(
                        LocalizableStrings.InstallationSucceeded,
                        string.Join(", ", package.Commands.Select(c => c.Name)),
                        package.Id,
                        package.Version.ToNormalizedString()).Green());
                return 0;
            }
            catch (Exception ex) when (InstallWorkloadCommandLowLevelErrorConverter.ShouldConvertToUserFacingError(ex))
            {
                throw new GracefulException(
                    messages: InstallWorkloadCommandLowLevelErrorConverter.GetUserFacingMessages(ex, _packageId),
                    verboseMessages: new[] {ex.ToString()},
                    isUserError: false);
            }
        }
    }
}
