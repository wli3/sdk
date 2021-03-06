// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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

namespace Microsoft.DotNet.Workloads.Workload.Uninstall
{
    internal delegate IShellShimRepository CreateShellShimRepository(DirectoryPath? nonGlobalLocation = null);
    internal delegate (IWorkloadPackageStore, IWorkloadPackageStoreQuery, IWorkloadPackageUninstaller) CreateWorkloadPackageStoresAndUninstaller(DirectoryPath? nonGlobalLocation = null);
    internal class WorkloadUninstallGlobalOrWorkloadPathCommand : CommandBase
    {
        private readonly IReporter _reporter;
        private readonly IReporter _errorReporter;
        private CreateShellShimRepository _createShellShimRepository;
        private CreateWorkloadPackageStoresAndUninstaller _createWorkloadPackageStoresAndUninstaller;

        public WorkloadUninstallGlobalOrWorkloadPathCommand(
            ParseResult result,
            CreateWorkloadPackageStoresAndUninstaller createWorkloadPackageStoreAndUninstaller = null,
            CreateShellShimRepository createShellShimRepository = null,
            IReporter reporter = null)
            : base(result)
        {
            _reporter = reporter ?? Reporter.Output;
            _errorReporter = reporter ?? Reporter.Error;

            _createShellShimRepository = createShellShimRepository ?? ShellShimRepositoryFactory.CreateShellShimRepository;
            _createWorkloadPackageStoresAndUninstaller = createWorkloadPackageStoreAndUninstaller ??
                                                    WorkloadPackageFactory.CreateWorkloadPackageStoresAndUninstaller;
        }

        public override int Execute()
        {
            var global = _parseResult.ValueForOption<bool>(WorkloadAppliedOption.GlobalOptionAliases.First());
            var workloadPath = _parseResult.ValueForOption<string>(WorkloadAppliedOption.WorkloadPathOptionAlias);

            DirectoryPath? workloadDirectoryPath = null;
            if (!string.IsNullOrWhiteSpace(workloadPath))
            {
                if (!Directory.Exists(workloadPath))
                {
                    throw new GracefulException(
                        string.Format(
                            LocalizableStrings.InvalidWorkloadPathOption,
                            workloadPath));
                }

                workloadDirectoryPath = new DirectoryPath(workloadPath);
            }

            (IWorkloadPackageStore workloadPackageStore, IWorkloadPackageStoreQuery workloadPackageStoreQuery, IWorkloadPackageUninstaller workloadPackageUninstaller)
                = _createWorkloadPackageStoresAndUninstaller(workloadDirectoryPath);
            IShellShimRepository shellShimRepository = _createShellShimRepository(workloadDirectoryPath);

            var packageId = new PackageId(_parseResult.ValueForArgument<string>(WorkloadInstallCommandParser.PackageIdArgument));
            IWorkloadPackage package = null;
            try
            {
                package = workloadPackageStoreQuery.EnumeratePackageVersions(packageId).SingleOrDefault();
                if (package == null)
                {
                    throw new GracefulException(
                        messages: new[]
                        {
                            string.Format(
                                LocalizableStrings.WorkloadNotInstalled,
                                packageId),
                        },
                    isUserError: false);
                }
            }
            catch (InvalidOperationException)
            {
                throw new GracefulException(
                        messages: new[]
                        {
                            string.Format(
                        LocalizableStrings.WorkloadHasMultipleVersionsInstalled,
                        packageId),
                        },
                    isUserError: false);
            }

            try
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.Zero))
                {
                    foreach (var command in package.Commands)
                    {
                        shellShimRepository.RemoveShim(command.Name);
                    }

                    workloadPackageUninstaller.Uninstall(package.PackageDirectory);

                    scope.Complete();
                }

                _reporter.WriteLine(
                    string.Format(
                        LocalizableStrings.UninstallSucceeded,
                        package.Id,
                        package.Version.ToNormalizedString()).Green());
                return 0;
            }
            catch (Exception ex) when (WorkloadUninstallCommandLowLevelErrorConverter.ShouldConvertToUserFacingError(ex))
            {
                throw new GracefulException(
                    messages: WorkloadUninstallCommandLowLevelErrorConverter.GetUserFacingMessages(ex, packageId),
                    verboseMessages: new[] {ex.ToString()},
                    isUserError: false);
            }
        }
    }
}
