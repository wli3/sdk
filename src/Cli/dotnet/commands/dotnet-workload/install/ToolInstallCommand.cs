// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.Workloads.Workload.Common;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal class WorkloadInstallCommand : CommandBase
    {
        private readonly WorkloadInstallLocalCommand _workloadInstallLocalCommand;
        private readonly WorkloadInstallGlobalOrWorkloadPathCommand _workloadInstallGlobalOrWorkloadPathCommand;
        private readonly bool _global;
        private readonly string _workloadPath;
        private readonly bool _local;
        private readonly string _framework;

        public WorkloadInstallCommand(
            ParseResult parseResult,
            WorkloadInstallGlobalOrWorkloadPathCommand workloadInstallGlobalOrWorkloadPathCommand = null,
            WorkloadInstallLocalCommand workloadInstallLocalCommand = null)
            : base(parseResult)
        {
            _workloadInstallLocalCommand =
                workloadInstallLocalCommand
                ?? new WorkloadInstallLocalCommand(_parseResult);

            _workloadInstallGlobalOrWorkloadPathCommand =
                workloadInstallGlobalOrWorkloadPathCommand
                ?? new WorkloadInstallGlobalOrWorkloadPathCommand(_parseResult);

            _global = parseResult.ValueForOption<bool>(WorkloadAppliedOption.GlobalOptionAliases.First());
            _local = parseResult.ValueForOption<bool>(WorkloadAppliedOption.LocalOptionAlias);
            _workloadPath = parseResult.ValueForOption<string>(WorkloadAppliedOption.WorkloadPathOptionAlias);
            _framework = parseResult.ValueForOption<string>(WorkloadInstallCommandParser.FrameworkOption);
        }

        public override int Execute()
        {
            WorkloadAppliedOption.EnsureNoConflictGlobalLocalWorkloadPathOption(
                _parseResult,
                LocalizableStrings.InstallWorkloadCommandInvalidGlobalAndLocalAndWorkloadPath);

            WorkloadAppliedOption.EnsureWorkloadManifestAndOnlyLocalFlagCombination(
                _parseResult);

            if (_global || !string.IsNullOrWhiteSpace(_workloadPath))
            {
                return _workloadInstallGlobalOrWorkloadPathCommand.Execute();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_framework))
                {
                    throw new GracefulException(
                        string.Format(
                            LocalizableStrings.LocalOptionDoesNotSupportFrameworkOption));
                }

                return _workloadInstallLocalCommand.Execute();
            }
        }
    }
}
