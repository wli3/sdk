// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Linq;
using Microsoft.DotNet.Cli.Utils;

namespace Microsoft.DotNet.Workloads.Workload.Common
{
    internal class WorkloadAppliedOption
    {
        public static string[] GlobalOptionAliases = new string[] { "--global", "-g" };
        public static Option GlobalOption(string description) => new Option<bool>(GlobalOptionAliases, description);

        public static string LocalOptionAlias = "--local";
        public static Option LocalOption(string description) => new Option<bool>(LocalOptionAlias, description);

        public static string WorkloadPathOptionAlias = "--workload-path";
        public static Option WorkloadPathOption(string description, string argumentName) => new Option<string>(WorkloadPathOptionAlias, description)
        {
            ArgumentHelpName = argumentName
        };

        public static string WorkloadManifestOptionAlias = "--workload-manifest";
        public static Option WorkloadManifestOption(string description, string argumentName) => new Option<string>(WorkloadManifestOptionAlias, description, arity: ArgumentArity.ZeroOrOne)
        {
            ArgumentHelpName = argumentName
        };

        internal static void EnsureNoConflictGlobalLocalWorkloadPathOption(
            ParseResult parseResult,
            string message)
        {
            List<string> options = new List<string>();
            if (parseResult.HasOption(GlobalOptionAliases.First()))
            {
                options.Add(GlobalOptionAliases.First().Trim('-'));
            }

            if (parseResult.HasOption(LocalOptionAlias))
            {
                options.Add(LocalOptionAlias.Trim('-'));
            }

            if (!String.IsNullOrWhiteSpace(parseResult.ValueForOption<string>(WorkloadPathOptionAlias)))
            {
                options.Add(WorkloadPathOptionAlias.Trim('-'));
            }

            if (options.Count > 1)
            {

                throw new GracefulException(
                    string.Format(
                        message,
                        string.Join(" ", options)));
            }
        }

        internal static void EnsureWorkloadManifestAndOnlyLocalFlagCombination(ParseResult parseResult)
        {
            if (GlobalOrWorkloadPath(parseResult) &&
                !string.IsNullOrWhiteSpace(parseResult.ValueForOption<string>(WorkloadManifestOptionAlias)))
            {
                throw new GracefulException(
                    string.Format(
                        LocalizableStrings.OnlyLocalOptionSupportManifestFileOption));
            }
        }

        private static bool GlobalOrWorkloadPath(ParseResult parseResult)
        {
            return parseResult.HasOption(GlobalOptionAliases.First()) ||
                   !string.IsNullOrWhiteSpace(parseResult.ValueForOption<string>(WorkloadPathOptionAlias));
        }
    }
}
