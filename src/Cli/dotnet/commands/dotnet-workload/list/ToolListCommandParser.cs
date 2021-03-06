// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine;
using Microsoft.DotNet.Workloads.Workload.Common;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.List.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadListCommandParser
    {
        public static readonly Option GlobalOption = WorkloadAppliedOption.GlobalOption(LocalizableStrings.GlobalOptionDescription);

        public static readonly Option LocalOption = WorkloadAppliedOption.LocalOption(LocalizableStrings.LocalOptionDescription);

        public static readonly Option WorkloadPathOption = WorkloadAppliedOption.WorkloadPathOption(LocalizableStrings.WorkloadPathOptionDescription, LocalizableStrings.WorkloadPathOptionName);

        public static Command GetCommand()
        {
            var command = new Command("list", LocalizableStrings.CommandDescription);

            command.AddOption(GlobalOption);
            command.AddOption(LocalOption);
            command.AddOption(WorkloadPathOption);

            return command;
        }
    }
}
