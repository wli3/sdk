// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine;
using Microsoft.DotNet.Workloads.Workload.Common;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.Update.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadUpdateCommandParser
    {
        public static readonly Argument PackageIdArgument = WorkloadInstallCommandParser.PackageIdArgument;

        public static readonly Option GlobalOption = WorkloadAppliedOption.GlobalOption(LocalizableStrings.GlobalOptionDescription);

        public static readonly Option WorkloadPathOption = WorkloadAppliedOption.WorkloadPathOption(LocalizableStrings.WorkloadPathOptionDescription, LocalizableStrings.WorkloadPathOptionName);

        public static readonly Option LocalOption = WorkloadAppliedOption.LocalOption(LocalizableStrings.LocalOptionDescription);

        public static readonly Option ConfigOption = WorkloadInstallCommandParser.ConfigOption;

        public static readonly Option AddSourceOption = WorkloadInstallCommandParser.AddSourceOption;

        public static readonly Option FrameworkOption = WorkloadInstallCommandParser.FrameworkOption;

        public static readonly Option VersionOption = WorkloadInstallCommandParser.VersionOption;

        public static readonly Option WorkloadManifestOption = WorkloadAppliedOption.WorkloadManifestOption(LocalizableStrings.ManifestPathOptionDescription, LocalizableStrings.ManifestPathOptionName);

        public static readonly Option VerbosityOption = WorkloadInstallCommandParser.VerbosityOption;

        public static Command GetCommand()
        {
            var command = new Command("update", LocalizableStrings.CommandDescription);

            command.AddArgument(PackageIdArgument);
            command.AddOption(GlobalOption);
            command.AddOption(WorkloadPathOption);
            command.AddOption(LocalOption);
            command.AddOption(ConfigOption);
            command.AddOption(AddSourceOption);
            command.AddOption(FrameworkOption);
            command.AddOption(VersionOption);
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
