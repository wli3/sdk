// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Build.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests
{
    public class GivenAGenerateRuntimeConfigurationFiles
    {
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
            Directory.CreateDirectory(testTempDir);
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

            var task = new TestableGenerateRuntimeConfigurationFiles()
            {
                BuildEngine = new MockBuildEngine4(),
                TargetFrameworkMoniker = ".NETCoreApp,Version=v3.0",
                TargetFramework = "netcoreapp3.0",
                RuntimeConfigPath = runtimeConfigPath,
                RuntimeConfigDevPath = runtimeConfigDevPath,
                RuntimeFrameworks = new MockTaskItem[] { 
                    new MockTaskItem(
                        itemSpec: "Microsoft.NETCore.App",
                        metadata: new Dictionary<string, string>
                        {
                            { "FrameworkName", "Microsoft.NETCore.App" },
                            { "Version", "3.0.0-preview1.100" },
                        }
                    )},
                RollForward = "LatestMinor"
            };

            Action a = () => task.PublicExecuteCore();
            a.ShouldNotThrow();

            File.ReadAllText(runtimeConfigPath).Should()
                .Be(
@"{
  ""runtimeOptions"": {
    ""tfm"": ""netcoreapp3.0"",
    ""rollForward"": ""LatestMinor"",
    ""framework"": {
      ""name"": ""Microsoft.NETCore.App"",
      ""version"": ""3.0.0-preview1.100""
    }
  }
}");
            File.Exists(runtimeConfigDevPath).Should().BeFalse("No nuget involved, so no extra probing path"); // TODO wul need discussion
        }

        [Fact(Skip = "Pending")]
        public void ItShouldErrorAndCallForRestoreWhenAssetFileDoesNotExist()
        {
            // When Microsoft.PackageDependencyResolution.targets is not imported, this is not set. So does not expect it
        }

        private class TestableGenerateRuntimeConfigurationFiles : GenerateRuntimeConfigurationFiles
        {
            public void PublicExecuteCore()
            {
                base.ExecuteCore();
            }
        }

        private class MockBuildEngine4 : MockBuildEngine, IBuildEngine4
        {
            public bool IsRunningMultipleNodes => throw new System.NotImplementedException();

            public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalProperties, IDictionary targetOutputs, string toolsVersion)
            {
                throw new System.NotImplementedException();
            }

            public BuildEngineResult BuildProjectFilesInParallel(string[] projectFileNames, string[] targetNames, IDictionary[] globalProperties, IList<string>[] removeGlobalProperties, string[] toolsVersion, bool returnTargetOutputs)
            {
                throw new System.NotImplementedException();
            }

            public bool BuildProjectFilesInParallel(string[] projectFileNames, string[] targetNames, IDictionary[] globalProperties, IDictionary[] targetOutputsPerProject, string[] toolsVersion, bool useResultsCache, bool unloadProjectsOnCompletion)
            {
                throw new System.NotImplementedException();
            }

            public object GetRegisteredTaskObject(object key, RegisteredTaskObjectLifetime lifetime)
            {
                return null;
            }

            public void Reacquire()
            {
                throw new System.NotImplementedException();
            }

            public void RegisterTaskObject(object key, object obj, RegisteredTaskObjectLifetime lifetime, bool allowEarlyCollection)
            {
                return;
            }

            public object UnregisterTaskObject(object key, RegisteredTaskObjectLifetime lifetime)
            {
                throw new System.NotImplementedException();
            }

            public void Yield()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
