// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.Workloads.Workload.Common;

namespace Microsoft.DotNet.Workloads.Workload.Update
{
    internal class WorkloadUpdateCommand : CommandBase
    {
        private readonly WorkloadUpdateLocalCommand _workloadUpdateLocalCommand;
        private readonly WorkloadUpdateGlobalOrWorkloadPathCommand _workloadUpdateGlobalOrWorkloadPathCommand;
        private readonly bool _global;
        private readonly string _workloadPath;

        public WorkloadUpdateCommand(
            ParseResult result,
            IReporter reporter = null,
            WorkloadUpdateGlobalOrWorkloadPathCommand workloadUpdateGlobalOrWorkloadPathCommand = null,
            WorkloadUpdateLocalCommand workloadUpdateLocalCommand = null)
            : base(result)
        {
            _workloadUpdateLocalCommand
                = workloadUpdateLocalCommand ??
                  new WorkloadUpdateLocalCommand(result);

            _workloadUpdateGlobalOrWorkloadPathCommand =
                workloadUpdateGlobalOrWorkloadPathCommand
                ?? new WorkloadUpdateGlobalOrWorkloadPathCommand(result);

            _global = result.ValueForOption<bool>(WorkloadUpdateCommandParser.GlobalOption);
            _workloadPath = result.ValueForOption<string>(WorkloadUpdateCommandParser.WorkloadPathOption);
        }

        public override int Execute()
        {
            WorkloadAppliedOption.EnsureNoConflictGlobalLocalWorkloadPathOption(
                _parseResult,
                LocalizableStrings.UpdateWorkloadCommandInvalidGlobalAndLocalAndWorkloadPath);

            WorkloadAppliedOption.EnsureWorkloadManifestAndOnlyLocalFlagCombination(_parseResult);

            if (_global || !string.IsNullOrWhiteSpace(_workloadPath))
            {
                return _workloadUpdateGlobalOrWorkloadPathCommand.Execute();
            }
            else
            {
                return _workloadUpdateLocalCommand.Execute();
            }
        }
    }
}
