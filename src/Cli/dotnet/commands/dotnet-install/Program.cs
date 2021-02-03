// Copyright(c) .NET Foundation and contributors.All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.PackageDownload;
using Microsoft.DotNet.ToolPackage.Tests;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using NuGet.Credentials;

namespace Microsoft.DotNet.Tools.Install
{
    public class InstallCommand
    {
        public static int Run(string[] args)
        {
            RunAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

            return 0;
        }

        private static async Task RunAsync(string[] args)
        {
            ConsoleLogger consoleLogger = new ConsoleLogger();
            DefaultCredentialServiceUtility.SetupDefaultCredentialService(consoleLogger, false);
            DebugHelper.HandleDebugSwitch(ref args);

            CancellationToken cancellationToken = CancellationToken.None;
            SourceCacheContext cache = new();
            cache.DirectDownload = true;
            cache.NoCache = true;
            SourceRepository source = Repository.Factory.GetCoreV3(
                "https://williamleewul.pkgs.visualstudio.com/_packaging/testingnugetauth/nuget/v3/index.json");
            HttpSourceResource httpSourceResource =
                await source.GetResourceAsync<HttpSourceResource>(cancellationToken);
            DownloadProcessMonitor downloadProcessMonitor = new();
            HttpSource httpSource = httpSourceResource.HttpSource;
            ConsolePrintDownloadProcessReporter consolePrintDownloadProcessReporter = new(consoleLogger);
            consolePrintDownloadProcessReporter.Subscribe(downloadProcessMonitor);
            httpSource.RetryHandler = new HttpRetryHandlerWithProgress(downloadProcessMonitor);
            FindPackageByIdResource findPackageByIdResource =
                await source.GetResourceAsync<FindPackageByIdResource>(cancellationToken);

            string packageId = "xunit";
            NuGetVersion packageVersion = new("2.4.0");

            MemoryStream stream = new();
            bool successful = await findPackageByIdResource.CopyNupkgToStreamAsync(packageId,
                packageVersion,
                stream,
                cache,
                consoleLogger,
                cancellationToken);
        }
    }
}
