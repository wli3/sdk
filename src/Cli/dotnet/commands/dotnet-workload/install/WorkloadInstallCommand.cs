// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.Linq;
using System.Threading;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal class WorkloadInstallCommand : CommandBase
    {
        private IReadOnlyCollection<string> _workloadIds;

        public WorkloadInstallCommand(
            ParseResult parseResult)
            : base(parseResult)
        {
            _workloadIds = parseResult.ValueForArgument<IReadOnlyCollection<string>>(WorkloadInstallCommandParser.WorkloadIdArgument);
        }

        public override int Execute()
        {
            // TODO stub
            Reporter.Output.WriteLine($"WIP workload install {string.Join("; ",_workloadIds)}");
            var allowedMockWorkloads = new List<string> {"mobile-ios", "mobile-android"};
            if (_workloadIds.Except(allowedMockWorkloads).Any())
            {
                Reporter.Output.WriteLine("Only support \"mobile-ios\", \"mobile-android\" in the mock");
            }
            
            if (_workloadIds.Contains("mobile-ios"))
            {
                SourceRepository source = Repository.Factory.GetCoreV3("https://williamleewul.pkgs.visualstudio.com/_packaging/testfeed/nuget/v3/index.json");
                var serviceIndexResource = source.GetResourceAsync<ServiceIndexResourceV3>().Result;
                var packageBaseAddress = serviceIndexResource?.GetServiceEntryUris(ServiceTypes.PackageBaseAddress);

                // Print URL to download
                Reporter.Output.WriteLine(nupkgUrl(packageBaseAddress.First().ToString(), "Microsoft.iOS.Bundle",
                    NuGetVersion.Parse("6.0.100")));
            }

            return 0;
        }
        
        public string nupkgUrl(string baseUri, string id, NuGetVersion version)
        {
            return baseUri + id.ToLowerInvariant() + "/" + version.ToNormalizedString() + "/" + id.ToLowerInvariant() +
                   "." +
                   version.ToNormalizedString() + ".nupkg";
        }
    }
}
