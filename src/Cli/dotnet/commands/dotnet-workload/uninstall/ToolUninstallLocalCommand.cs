// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using System.IO;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadManifest;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.DotNet.Workloads.Workload.Common;
using Microsoft.Extensions.EnvironmentAbstractions;

namespace Microsoft.DotNet.Workloads.Workload.Uninstall
{
    internal class WorkloadUninstallLocalCommand : CommandBase
    {
        private readonly IWorkloadManifestFinder _workloadManifestFinder;
        private readonly IWorkloadManifestEditor _workloadManifestEditor;
        private readonly IReporter _reporter;

        private readonly PackageId _packageId;
        private readonly string _explicitManifestFile;

        public WorkloadUninstallLocalCommand(
            ParseResult parseResult,
            IWorkloadManifestFinder workloadManifestFinder = null,
            IWorkloadManifestEditor workloadManifestEditor = null,
            IReporter reporter = null)
            : base(parseResult)
        {
            _packageId = new PackageId(parseResult.ValueForArgument<string>(WorkloadUninstallCommandParser.PackageIdArgument));
            _explicitManifestFile = parseResult.ValueForOption<string>(WorkloadUninstallCommandParser.WorkloadManifestOption);

            _reporter = (reporter ?? Reporter.Output);

            _workloadManifestFinder = workloadManifestFinder ??
                                  new WorkloadManifestFinder(new DirectoryPath(Directory.GetCurrentDirectory()));
            _workloadManifestEditor = workloadManifestEditor ?? new WorkloadManifestEditor();
        }

        public override int Execute()
        {
            (FilePath? manifestFileOptional, string warningMessage) =
                _workloadManifestFinder.ExplicitManifestOrFindManifestContainPackageId(_explicitManifestFile, _packageId);

            if (!manifestFileOptional.HasValue)
            {
                throw new GracefulException(
                    new[] { string.Format(LocalizableStrings.NoManifestFileContainPackageId, _packageId) }, 
                    isUserError: false);
            }

            var manifestFile = manifestFileOptional.Value;

            _workloadManifestEditor.Remove(manifestFile, _packageId);

            if (warningMessage != null)
            {
                _reporter.WriteLine(warningMessage.Yellow());
            }

            _reporter.WriteLine(
                string.Format(
                    LocalizableStrings.UninstallLocalWorkloadSucceeded,
                    _packageId,
                    manifestFile.Value).Green());
            return 0;
        }
    }
}
