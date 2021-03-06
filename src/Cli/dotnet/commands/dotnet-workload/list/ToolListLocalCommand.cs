// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadManifest;
using Microsoft.Extensions.EnvironmentAbstractions;

namespace Microsoft.DotNet.Workloads.Workload.List
{
    internal class WorkloadListLocalCommand : CommandBase
    {
        private readonly IWorkloadManifestInspector _workloadManifestInspector;
        private readonly IReporter _reporter;
        private const string CommandDelimiter = ", ";

        public WorkloadListLocalCommand(
            ParseResult parseResult,
            IWorkloadManifestInspector workloadManifestInspector = null,
            IReporter reporter = null)
            : base(parseResult)
        {
            _reporter = (reporter ?? Reporter.Output);

            _workloadManifestInspector = workloadManifestInspector ??
                                     new WorkloadManifestFinder(new DirectoryPath(Directory.GetCurrentDirectory()));
        }

        public override int Execute()
        {
            var table = new PrintableTable<(WorkloadManifestPackage workloadManifestPackage, FilePath SourceManifest)>();

            table.AddColumn(
                LocalizableStrings.PackageIdColumn,
                p => p.workloadManifestPackage.PackageId.ToString());
            table.AddColumn(
                LocalizableStrings.VersionColumn,
                p => p.workloadManifestPackage.Version.ToNormalizedString());
            table.AddColumn(
                LocalizableStrings.CommandsColumn,
                p => string.Join(CommandDelimiter, p.workloadManifestPackage.CommandNames.Select(c => c.Value)));
            table.AddColumn(
                LocalizableStrings.ManifestFileColumn,
                p => p.SourceManifest.Value);

            table.PrintRows(_workloadManifestInspector.Inspect(), l => _reporter.WriteLine(l));
            return 0;
        }
    }
}
