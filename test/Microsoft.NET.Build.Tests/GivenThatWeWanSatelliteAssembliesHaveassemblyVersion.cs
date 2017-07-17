// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using Xunit;
using Xunit.Abstractions;
using System.Diagnostics;
using FluentAssertions;
using System.Reflection;

namespace Microsoft.NET.Build.Tests
{
    public class GivenThatWeWanSatelliteAssembliesHaveassemblyVersion : SdkTest
    {
        private string _mainAssembliyPath;
        private string _satelliteAssembliyPath;
        public GivenThatWeWanSatelliteAssembliesHaveassemblyVersion(ITestOutputHelper log) : base(log)
        {
            if (UsingFullFrameworkMSBuild)
            {
                //  Disable this test on full framework, as generating strong named satellite assemblies with AL.exe requires Admin permissions
                //  See https://github.com/dotnet/sdk/issues/732
                return;
            }

            var testAsset = _testAssetsManager
              .CopyTestAsset("AllResourcesInSatelliteDisableVersionGenerate")
              .WithSource();

            testAsset = testAsset.Restore(Log);
            var buildCommand = new BuildCommand(Log, testAsset.TestRoot);
            buildCommand
                .Execute()
                .Should()
                .Pass();

            var outputDirectory = buildCommand.GetOutputDirectory("netcoreapp1.1");
            _mainAssembliyPath = Path.Combine(outputDirectory.FullName, "AllResourcesInSatellite.dll");
            _satelliteAssembliyPath = Path.Combine(outputDirectory.FullName, "en", "AllResourcesInSatellite.resources.dll");
        }

        [Fact]
        public void It_should_produce_same_SatelliteAssemblie_FileVersionInfo_as_main()
        {
            var mainAssembliyFileVersioninfo = FileVersionInfo.GetVersionInfo(_mainAssembliyPath);
            var satelliteAssembliyFileVersioninfo = FileVersionInfo.GetVersionInfo(_satelliteAssembliyPath);

            satelliteAssembliyFileVersioninfo.FileVersion.Should().Be(mainAssembliyFileVersioninfo.FileVersion);

            satelliteAssembliyFileVersioninfo.CompanyName.Should().Be(mainAssembliyFileVersioninfo.CompanyName);
            satelliteAssembliyFileVersioninfo.LegalCopyright.Should().Be(mainAssembliyFileVersioninfo.LegalCopyright);
            satelliteAssembliyFileVersioninfo.Comments.Should().Be(mainAssembliyFileVersioninfo.Comments);
            satelliteAssembliyFileVersioninfo.FileVersion.Should().Be(mainAssembliyFileVersioninfo.FileVersion);
            satelliteAssembliyFileVersioninfo.ProductVersion.Should().Be(mainAssembliyFileVersioninfo.ProductVersion);
            satelliteAssembliyFileVersioninfo.ProductName.Should().Be(mainAssembliyFileVersioninfo.ProductName);
            satelliteAssembliyFileVersioninfo.FileDescription.Should().Be(mainAssembliyFileVersioninfo.FileDescription);
        }

        [Fact]
        public void It_should_produce_same_SatelliteAssemblie_AssemblyVersions_as_main()
        {
            var mainAssembliy = AssemblyName.GetAssemblyName(_mainAssembliyPath);
            var satelliteAssembliy = AssemblyName.GetAssemblyName(_satelliteAssembliyPath);

            satelliteAssembliy.Version.Should().Be(mainAssembliy.Version);
        }
    }
}
