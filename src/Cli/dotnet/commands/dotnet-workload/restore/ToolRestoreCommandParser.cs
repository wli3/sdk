// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine;
using Microsoft.DotNet.Workloads.Workload.Common;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.Restore.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadRestoreCommandParser
    {
        public static readonly Option ConfigOption = WorkloadInstallCommandParser.ConfigOption;

        public static readonly Option AddSourceOption = WorkloadInstallCommandParser.AddSourceOption;

        public static readonly Option WorkloadManifestOption = WorkloadAppliedOption.WorkloadManifestOption(LocalizableStrings.ManifestPathOptionDescription, LocalizableStrings.ManifestPathOptionName);

        public static readonly Option VerbosityOption = WorkloadInstallCommandParser.VerbosityOption;

        public static Command GetCommand()
        {
            var command = new Command("restore", LocalizableStrings.CommandDescription);

            command.AddOption(ConfigOption);
            command.AddOption(AddSourceOption);
            command.AddOption(WorkloadManifestOption);
            command.AddOption(WorkloadCommandRestorePassThroughOptions.DisableParallelOption);
            command.AddOption(WorkloadCommandRestorePassThroughOptions.IgnoreFailedSourcesOption);
            command.AddOption(WorkloadCommandRestorePassThroughOptions.NoCacheOption);
            command.AddOption(WorkloadCommandRestorePassThroughOptions.InteractiveRestoreOption);
            command.AddOption(VerbosityOption);

            return command;
        }
    }
}
