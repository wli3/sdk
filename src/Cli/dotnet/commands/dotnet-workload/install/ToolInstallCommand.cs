// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.Parsing;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal class WorkloadInstallCommand : CommandBase
    {
        private readonly string _framework;

        public WorkloadInstallCommand(
            ParseResult parseResult)
            : base(parseResult)
        {
            _framework = parseResult.ValueForOption<string>(WorkloadInstallCommandParser.FrameworkOption);
        }

        public override int Execute()
        {
            // TODO stub
            Reporter.Output.WriteLine("WIP workload install");
            return 0;
        }
    }
}
