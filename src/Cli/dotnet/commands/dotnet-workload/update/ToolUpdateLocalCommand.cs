// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadManifest;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.DotNet.Workloads.Workload.Common;
using Microsoft.DotNet.Workloads.Workload.Install;
using Microsoft.Extensions.EnvironmentAbstractions;

namespace Microsoft.DotNet.Workloads.Workload.Update
{
    internal class WorkloadUpdateLocalCommand : CommandBase
    {
        private readonly IWorkloadManifestFinder _workloadManifestFinder;
        private readonly IWorkloadManifestEditor _workloadManifestEditor;
        private readonly ILocalWorkloadsResolverCache _localWorkloadsResolverCache;
        private readonly IWorkloadPackageInstaller _workloadPackageInstaller;
        private readonly WorkloadInstallLocalInstaller _workloadLocalPackageInstaller;
        private readonly Lazy<WorkloadInstallLocalCommand> _workloadInstallLocalCommand;
        private readonly IReporter _reporter;

        private readonly PackageId _packageId;
        private readonly string _explicitManifestFile;

        public WorkloadUpdateLocalCommand(
            ParseResult parseResult,
            IWorkloadPackageInstaller workloadPackageInstaller = null,
            IWorkloadManifestFinder workloadManifestFinder = null,
            IWorkloadManifestEditor workloadManifestEditor = null,
            ILocalWorkloadsResolverCache localWorkloadsResolverCache = null,
            IReporter reporter = null)
            : base(parseResult)
        {
            _packageId = new PackageId(parseResult.ValueForArgument<string>(WorkloadUpdateCommandParser.PackageIdArgument));
            _explicitManifestFile = parseResult.ValueForOption<string>(WorkloadUpdateCommandParser.WorkloadManifestOption);

            _reporter = (reporter ?? Reporter.Output);

            if (workloadPackageInstaller == null)
            {
                (IWorkloadPackageStore,
                    IWorkloadPackageStoreQuery,
                    IWorkloadPackageInstaller installer) workloadPackageStoresAndInstaller
                        = WorkloadPackageFactory.CreateWorkloadPackageStoresAndInstaller(
                            additionalRestoreArguments: parseResult.OptionValuesToBeForwarded(WorkloadUpdateCommandParser.GetCommand()));
                _workloadPackageInstaller = workloadPackageStoresAndInstaller.installer;
            }
            else
            {
                _workloadPackageInstaller = workloadPackageInstaller;
            }

            _workloadManifestFinder = workloadManifestFinder ??
                                  new WorkloadManifestFinder(new DirectoryPath(Directory.GetCurrentDirectory()));
            _workloadManifestEditor = workloadManifestEditor ?? new WorkloadManifestEditor();
            _localWorkloadsResolverCache = localWorkloadsResolverCache ?? new LocalWorkloadsResolverCache();
            _workloadLocalPackageInstaller = new WorkloadInstallLocalInstaller(parseResult, workloadPackageInstaller);
            _workloadInstallLocalCommand = new Lazy<WorkloadInstallLocalCommand>(
                () => new WorkloadInstallLocalCommand(
                    parseResult,
                    _workloadPackageInstaller,
                    _workloadManifestFinder,
                    _workloadManifestEditor,
                    _localWorkloadsResolverCache,
                    _reporter));
        }

        public override int Execute()
        {
            (FilePath? manifestFileOptional, string warningMessage) = 
                _workloadManifestFinder.ExplicitManifestOrFindManifestContainPackageId(_explicitManifestFile, _packageId);

            var manifestFile = manifestFileOptional ?? _workloadManifestFinder.FindFirst();

            var workloadDownloadedPackage = _workloadLocalPackageInstaller.Install(manifestFile);
            var existingPackageWithPackageId =
                _workloadManifestFinder
                    .Find(manifestFile)
                    .Where(p => p.PackageId.Equals(_packageId));

            if (!existingPackageWithPackageId.Any())
            {
                return _workloadInstallLocalCommand.Value.Install(manifestFile);
            }

            var existingPackage = existingPackageWithPackageId.Single();
            if (existingPackage.Version > workloadDownloadedPackage.Version)
            {
                throw new GracefulException(new[]
                    {
                        string.Format(
                            LocalizableStrings.UpdateLocaWorkloadToLowerVersion,
                            workloadDownloadedPackage.Version.ToNormalizedString(),
                            existingPackage.Version.ToNormalizedString(),
                            manifestFile.Value)
                    },
                    isUserError: false);
            }

            if (existingPackage.Version != workloadDownloadedPackage.Version)
            {
                _workloadManifestEditor.Edit(
                    manifestFile,
                    _packageId,
                    workloadDownloadedPackage.Version,
                    workloadDownloadedPackage.Commands.Select(c => c.Name).ToArray());
            }

            _localWorkloadsResolverCache.SaveWorkloadPackage(
                workloadDownloadedPackage,
                _workloadLocalPackageInstaller.TargetFrameworkToInstall);

            if (warningMessage != null)
            {
                _reporter.WriteLine(warningMessage.Yellow());
            }

            if (existingPackage.Version == workloadDownloadedPackage.Version)
            {
                _reporter.WriteLine(
                    string.Format(
                        LocalizableStrings.UpdateLocaWorkloadSucceededVersionNoChange,
                        workloadDownloadedPackage.Id,
                        existingPackage.Version.ToNormalizedString(),
                        manifestFile.Value));
            }
            else
            {
                _reporter.WriteLine(
                    string.Format(
                        LocalizableStrings.UpdateLocalWorkloadSucceeded,
                        workloadDownloadedPackage.Id,
                        existingPackage.Version.ToNormalizedString(),
                        workloadDownloadedPackage.Version.ToNormalizedString(),
                        manifestFile.Value).Green());
            }

            return 0;
        }
    }
}
