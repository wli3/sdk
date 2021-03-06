// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.Search.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadSearchCommandParser
    {
        public static readonly Argument SearchTermArgument = new Argument<string>(LocalizableStrings.SearchTermArgumentName)
        {
            Description = LocalizableStrings.SearchTermDescription
        };
        public static Command GetCommand()
        {
            var command = new Command("search", LocalizableStrings.CommandDescription);

            command.AddArgument(SearchTermArgument);

            return command;
        }
    }
}
