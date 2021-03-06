// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine.Parsing;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.NugetSearch;

namespace Microsoft.DotNet.Workloads.Workload.Search
{
    internal class WorkloadSearchCommand : CommandBase
    {
        private readonly INugetWorkloadSearchApiRequest _nugetWorkloadSearchApiRequest;
        private readonly SearchResultPrinter _searchResultPrinter;

        public WorkloadSearchCommand(
            ParseResult result,
            INugetWorkloadSearchApiRequest nugetWorkloadSearchApiRequest = null
        )
            : base(result)
        {
            _nugetWorkloadSearchApiRequest = nugetWorkloadSearchApiRequest ?? new NugetWorkloadSearchApiRequest();
            _searchResultPrinter = new SearchResultPrinter(Reporter.Output);
        }

        public override int Execute()
        {
            var isDetailed = _parseResult.ValueForOption<bool>(WorkloadSearchCommandParser.DetailOption);
            NugetSearchApiParameter nugetSearchApiParameter = new NugetSearchApiParameter(_parseResult);
            IReadOnlyCollection<SearchResultPackage> searchResultPackages =
                NugetSearchApiResultDeserializer.Deserialize(
                    _nugetWorkloadSearchApiRequest.GetResult(nugetSearchApiParameter).GetAwaiter().GetResult());

            _searchResultPrinter.Print(isDetailed, searchResultPackages);

            return 0;
        }
    }
}
