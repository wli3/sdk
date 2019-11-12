// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests
{
    public class GivenAGenerateRuntimeConfigurationFiles
    {
        private XDocument _generatedDocument = null;
        public GivenAGenerateRuntimeConfigurationFiles()
        {
        }


    //    GenerateRuntimeConfigurationFiles
    //Assembly = C:\work\sdk\artifacts\bin\Debug\Sdks\Microsoft.NET.Sdk\targets\..\tools\net472/Microsoft.NET.Build.Tasks.dll
    //Parameters
    //    TargetFrameworkMoniker = .NETCoreApp,Version=v3.1
    //    TargetFramework = netcoreapp3.1
    //    RuntimeConfigPath = C:\work\NETCoreCppCliTest\NETCoreCppCliTest\Debug\NETCoreCppCliTest.runtimeconfig.json
    //    RuntimeConfigDevPath = C:\work\NETCoreCppCliTest\NETCoreCppCliTest\Debug\NETCoreCppCliTest.runtimeconfig.dev.json
    //    RuntimeFrameworks
    //        Microsoft.NETCore.App
    //            FrameworkName = Microsoft.NETCore.App
    //            Version = 3.1.0-preview3.19553.2
    //    RollForward = LatestMinor
    //    UserRuntimeConfig = C:\work\NETCoreCppCliTest\NETCoreCppCliTest/runtimeconfig.template.json
    //    AdditionalProbingPaths = C:\Users\wul\.dotnet\store\|arch|\|tfm|
    //Errors
    //    C:\work\sdk\artifacts\bin\Debug\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.Sdk.targets(281,5): error NETSDK1063: The path to the project assets file was not set.Run a NuGet package restore to generate this file. [C:\work\NETCoreCppCliTest\NETCoreCppCliTest\NETCoreCppCliTest.vcxproj]

        [Fact]
        public void ItCanGenerateWithoutAssetFile()
        {
            string testTempDir = Path.Combine(Path.GetTempPath(), "dotnetSdkTests");
            var runtimeConfigPath = Path.Combine(testTempDir, nameof(ItCanGenerateWithoutAssetFile) + "runtimeconfig.json");
            var runtimeConfigDevPath = Path.Combine(testTempDir, nameof(ItCanGenerateWithoutAssetFile) + "runtimeconfig.dev.json");
            if (File.Exists(runtimeConfigPath))
            {
                File.Delete(runtimeConfigPath);
            }

            if (File.Exists(runtimeConfigDevPath))
            {
                File.Delete(runtimeConfigDevPath);
            }

            var task = new GenerateRuntimeConfigurationFiles()
            {
                BuildEngine = new MockBuildEngine(),
                TargetFrameworkMoniker = ".NETCoreApp,Version=v3.1",
                TargetFramework = "netcoreapp3.1",
                RuntimeConfigPath = runtimeConfigPath,
                RuntimeConfigDevPath = runtimeConfigDevPath,
                RuntimeFrameworks = new MockTaskItem[] { 
                    new MockTaskItem(
                        itemSpec: "Microsoft.NETCore.App",
                        metadata: new Dictionary<string, string>
                        {
                            { "FrameworkName", "Microsoft.NETCore.App" },
                            { "Version", "3.1.0-preview3.19553.2" },
                        }
                    )},
                RollForward = "LatestMinor"
            };
            task.Execute().Should().BeTrue();
        }
    }
}
