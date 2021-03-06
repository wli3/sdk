// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Workloads.Workload.Common;

namespace Microsoft.DotNet.Workloads.Workload.List
{
    internal class WorkloadListCommand : CommandBase
    {
        private readonly WorkloadListGlobalOrWorkloadPathCommand _workloadListGlobalOrWorkloadPathCommand;
        private readonly WorkloadListLocalCommand _workloadListLocalCommand;

        public WorkloadListCommand(
            ParseResult result,
            WorkloadListGlobalOrWorkloadPathCommand workloadListGlobalOrWorkloadPathCommand = null,
            WorkloadListLocalCommand workloadListLocalCommand = null
        )
            : base(result)
        {
            _workloadListGlobalOrWorkloadPathCommand
                = workloadListGlobalOrWorkloadPathCommand ?? new WorkloadListGlobalOrWorkloadPathCommand(result);
            _workloadListLocalCommand
                = workloadListLocalCommand ?? new WorkloadListLocalCommand(result);
        }

        public override int Execute()
        {
            WorkloadAppliedOption.EnsureNoConflictGlobalLocalWorkloadPathOption(
                _parseResult,
                LocalizableStrings.ListWorkloadCommandInvalidGlobalAndLocalAndWorkloadPath);

            if (_parseResult.ValueForOption<bool>(WorkloadListCommandParser.GlobalOption)
                || _parseResult.HasOption(WorkloadListCommandParser.WorkloadPathOption))
            {
                return _workloadListGlobalOrWorkloadPathCommand.Execute();
            }
            else
            {
                return _workloadListLocalCommand.Execute();
            }
        }
    }
}
