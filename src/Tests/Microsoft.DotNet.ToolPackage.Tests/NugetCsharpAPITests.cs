// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.DotNet.Cli.PackageDownload;
using Microsoft.NET.TestFramework;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.ToolPackage.Tests
{
    public class NugetCsharpApiTests : SdkTest
    {
        public NugetCsharpApiTests(ITestOutputHelper log) : base(log)
        {
        }

        [Fact]
        public async Task ItShouldDownloadFileWithProcess()
        {
            TestLogger logger = new TestLogger(Log);
            CancellationToken cancellationToken = CancellationToken.None;
            SourceCacheContext cache = new();
            cache.DirectDownload = true;
            cache.NoCache = true;
            SourceRepository source = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            HttpSourceResource httpSourceResource =
                await source.GetResourceAsync<HttpSourceResource>(cancellationToken);
            DownloadProcessMonitor downloadProcessMonitor = new();
            HttpSource httpSource = httpSourceResource.HttpSource;
            ConsolePrintDownloadProcessReporter consolePrintDownloadProcessReporter = new(logger);
            consolePrintDownloadProcessReporter.Subscribe(downloadProcessMonitor);
            httpSource.RetryHandler = new HttpRetryHandlerWithProgress(downloadProcessMonitor);
            FindPackageByIdResource findPackageByIdResource =
                await source.GetResourceAsync<FindPackageByIdResource>(cancellationToken);

            string packageId = "Newtonsoft.Json";
            NuGetVersion packageVersion = new("12.0.1");

            MemoryStream stream = new();
            bool successful = await findPackageByIdResource.CopyNupkgToStreamAsync(packageId,
                packageVersion,
                stream,
                cache,
                logger,
                cancellationToken);
            successful.Should().BeTrue();
            logger.Messages.Should().Contain("Finished 100%");
        }
    }
}
