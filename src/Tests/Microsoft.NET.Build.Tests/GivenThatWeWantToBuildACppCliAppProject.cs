// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using Xunit.Abstractions;

namespace Microsoft.NET.Build.Tests
{
    public class GivenThatWeWantToBuildACppCliAppProject : SdkTest
    {
        public GivenThatWeWantToBuildACppCliAppProject(ITestOutputHelper log) : base(log)
        {
        }

        [FullMSBuildOnlyFact]
        public void It_builds_and_runs()
        {
            var testAsset = _testAssetsManager
                .CopyTestAsset("NETCoreCppClApp")
                .WithSource()
                .Restore(Log, "NETCoreCppCliTest.sln");

            new BuildCommand(Log, Path.Combine(testAsset.TestRoot, "NETCoreCppCliTest.sln"))
                .Execute("-p:Platform=x64")
                .Should()
                .Pass();

            // There is a bug in MSVC in CI's old VS image.
            // Once https://github.com/dotnet/core-eng/issues/7409/ is done
            // we should directly run the app to test.

            // TODO wul remove that before check in
            var exe = Path.Combine(
                testAsset.TestRoot,
                "x64",
                "Debug",
                "NETCoreCppCliTest.exe");

            new RunExeCommand(Log, exe).Execute()
                .Should()
                .Pass()
                .And
                .HaveStdOutContaining("Hello world from C++/CLI");
        }
    }
}
