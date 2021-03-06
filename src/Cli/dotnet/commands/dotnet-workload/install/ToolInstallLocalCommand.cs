// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadManifest;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.DotNet.Workloads.Workload.Common;
using Microsoft.Extensions.EnvironmentAbstractions;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal class WorkloadInstallLocalCommand : CommandBase
    {
        private readonly IWorkloadManifestFinder _workloadManifestFinder;
        private readonly IWorkloadManifestEditor _workloadManifestEditor;
        private readonly ILocalWorkloadsResolverCache _localWorkloadsResolverCache;
        private readonly WorkloadInstallLocalInstaller _workloadLocalPackageInstaller;
        private readonly IReporter _reporter;

        private readonly string _explicitManifestFile;

        public WorkloadInstallLocalCommand(
            ParseResult parseResult,
            IWorkloadPackageInstaller workloadPackageInstaller = null,
            IWorkloadManifestFinder workloadManifestFinder = null,
            IWorkloadManifestEditor workloadManifestEditor = null,
            ILocalWorkloadsResolverCache localWorkloadsResolverCache = null,
            IReporter reporter = null)
            : base(parseResult)
        {
            _explicitManifestFile = parseResult.ValueForOption<string>(WorkloadAppliedOption.WorkloadManifestOptionAlias);

            _reporter = (reporter ?? Reporter.Output);

            _workloadManifestFinder = workloadManifestFinder ??
                                  new WorkloadManifestFinder(new DirectoryPath(Directory.GetCurrentDirectory()));
            _workloadManifestEditor = workloadManifestEditor ?? new WorkloadManifestEditor();
            _localWorkloadsResolverCache = localWorkloadsResolverCache ?? new LocalWorkloadsResolverCache();
            _workloadLocalPackageInstaller = new WorkloadInstallLocalInstaller(parseResult, workloadPackageInstaller);
        }

        public override int Execute()
        {
            FilePath manifestFile = GetManifestFilePath();

            return Install(manifestFile);
        }

        public int Install(FilePath manifestFile)
        {
            IWorkloadPackage workloadDownloadedPackage =
                _workloadLocalPackageInstaller.Install(manifestFile);

            _workloadManifestEditor.Add(
                manifestFile,
                workloadDownloadedPackage.Id,
                workloadDownloadedPackage.Version,
                workloadDownloadedPackage.Commands.Select(c => c.Name).ToArray());

            _localWorkloadsResolverCache.SaveWorkloadPackage(
                workloadDownloadedPackage,
                _workloadLocalPackageInstaller.TargetFrameworkToInstall);

            _reporter.WriteLine(
                string.Format(
                    LocalizableStrings.LocalWorkloadInstallationSucceeded,
                    string.Join(", ", workloadDownloadedPackage.Commands.Select(c => c.Name)),
                    workloadDownloadedPackage.Id,
                    workloadDownloadedPackage.Version.ToNormalizedString(),
                    manifestFile.Value).Green());

            return 0;
        }

        private FilePath GetManifestFilePath()
        {
            try
            {
                return string.IsNullOrWhiteSpace(_explicitManifestFile)
                    ? _workloadManifestFinder.FindFirst()
                    : new FilePath(_explicitManifestFile);
            }
            catch (WorkloadManifestCannotBeFoundException e)
            {
                throw new GracefulException(new[]
                    {
                        e.Message,
                        LocalizableStrings.NoManifestGuide
                    },
                    verboseMessages: new[] {e.VerboseMessage},
                    isUserError: false);
            }
        }
    }
}
