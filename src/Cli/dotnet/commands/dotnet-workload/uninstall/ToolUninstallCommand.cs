// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.Workloads.Workload.Common;

namespace Microsoft.DotNet.Workloads.Workload.Uninstall
{
    internal class WorkloadUninstallCommand : CommandBase
    {
        private readonly WorkloadUninstallLocalCommand _workloadUninstallLocalCommand;
        private readonly WorkloadUninstallGlobalOrWorkloadPathCommand _workloadUninstallGlobalOrWorkloadPathCommand;
        private readonly bool _global;
        private readonly string _workloadPath;

        public WorkloadUninstallCommand(
            ParseResult result,
            IReporter reporter = null,
            WorkloadUninstallGlobalOrWorkloadPathCommand workloadUninstallGlobalOrWorkloadPathCommand = null,
            WorkloadUninstallLocalCommand workloadUninstallLocalCommand = null)
            : base(result)
        {
            _workloadUninstallLocalCommand
                = workloadUninstallLocalCommand ??
                  new WorkloadUninstallLocalCommand(result);

            _workloadUninstallGlobalOrWorkloadPathCommand =
                workloadUninstallGlobalOrWorkloadPathCommand
                ?? new WorkloadUninstallGlobalOrWorkloadPathCommand(result);

            _global = result.ValueForOption<bool>(WorkloadUninstallCommandParser.GlobalOption);
            _workloadPath = result.ValueForOption<string>(WorkloadUninstallCommandParser.WorkloadPathOption);
        }

        public override int Execute()
        {
            WorkloadAppliedOption.EnsureNoConflictGlobalLocalWorkloadPathOption(
                _parseResult,
                LocalizableStrings.UninstallWorkloadCommandInvalidGlobalAndLocalAndWorkloadPath);

            WorkloadAppliedOption.EnsureWorkloadManifestAndOnlyLocalFlagCombination(_parseResult);

            if (_global || !string.IsNullOrWhiteSpace(_workloadPath))
            {
                return _workloadUninstallGlobalOrWorkloadPathCommand.Execute();
            }
            else
            {
                return _workloadUninstallLocalCommand.Execute();
            }
        }
    }
}
