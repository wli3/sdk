// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.Extensions.EnvironmentAbstractions;

namespace Microsoft.DotNet.Workloads.Workload.List
{
    internal delegate IWorkloadPackageStoreQuery CreateWorkloadPackageStore(DirectoryPath? nonGlobalLocation = null);

    internal class WorkloadListGlobalOrWorkloadPathCommand : CommandBase
    {
        public const string CommandDelimiter = ", ";
        private readonly IReporter _reporter;
        private readonly IReporter _errorReporter;
        private CreateWorkloadPackageStore _createWorkloadPackageStore;

        public WorkloadListGlobalOrWorkloadPathCommand(
            ParseResult result,
            CreateWorkloadPackageStore createWorkloadPackageStore = null,
            IReporter reporter = null)
            : base(result)
        {
            _reporter = reporter ?? Reporter.Output;
            _errorReporter = reporter ?? Reporter.Error;
            _createWorkloadPackageStore = createWorkloadPackageStore ?? WorkloadPackageFactory.CreateWorkloadPackageStoreQuery;
        }

        public override int Execute()
        {
            var workloadPathOption = _parseResult.ValueForOption<string>(WorkloadListCommandParser.WorkloadPathOption);

            DirectoryPath? workloadPath = null;
            if (!string.IsNullOrWhiteSpace(workloadPathOption))
            {
                if (!Directory.Exists(workloadPathOption))
                {
                    throw new GracefulException(
                        string.Format(
                            LocalizableStrings.InvalidWorkloadPathOption,
                            workloadPathOption));
                }

                workloadPath = new DirectoryPath(workloadPathOption);
            }

            var table = new PrintableTable<IWorkloadPackage>();

            table.AddColumn(
                LocalizableStrings.PackageIdColumn,
                p => p.Id.ToString());
            table.AddColumn(
                LocalizableStrings.VersionColumn,
                p => p.Version.ToNormalizedString());
            table.AddColumn(
                LocalizableStrings.CommandsColumn,
                p => string.Join(CommandDelimiter, p.Commands.Select(c => c.Name)));

            table.PrintRows(GetPackages(workloadPath), l => _reporter.WriteLine(l));
            return 0;
        }

        private IEnumerable<IWorkloadPackage> GetPackages(DirectoryPath? workloadPath)
        {
            return _createWorkloadPackageStore(workloadPath).EnumeratePackages()
                .Where(PackageHasCommands)
                .OrderBy(p => p.Id)
                .ToArray();
        }

        private bool PackageHasCommands(IWorkloadPackage package)
        {
            try
            {
                // Attempt to read the commands collection
                // If it fails, print a warning and treat as no commands
                return package.Commands.Count >= 0;
            }
            catch (Exception ex) when (ex is WorkloadConfigurationException)
            {
                _errorReporter.WriteLine(
                    string.Format(
                        LocalizableStrings.InvalidPackageWarning,
                        package.Id,
                        ex.Message).Yellow());
                return false;
            }
        }
    }
}
