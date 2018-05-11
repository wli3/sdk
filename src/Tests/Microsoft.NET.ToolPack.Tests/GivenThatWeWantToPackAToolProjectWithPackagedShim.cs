// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using NuGet.Packaging;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.DotNet.Cli.Utils;

namespace Microsoft.NET.ToolPack.Tests
{
    public class GivenThatWeWantToPackAToolProjectWithPackagedShim : SdkTest
    {
        private string _testRoot;
        private string _packageId;
        private readonly string _packageVersion = "1.0.0";
        private const string _customToolCommandName = "customToolCommandName";
        private ITestOutputHelper _log;

        public GivenThatWeWantToPackAToolProjectWithPackagedShim(ITestOutputHelper log) : base(log)
        {
            _log = log;
        }

        private string SetupNuGetPackage(bool multiTarget, [CallerMemberName] string callingMethod = "")
        {
            TestAsset helloWorldAsset = _testAssetsManager
                .CopyTestAsset("PortableTool", callingMethod + multiTarget)
                .WithSource()
                .WithProjectChanges(project =>
                {
                    XNamespace ns = project.Root.Name.Namespace;
                    XElement propertyGroup = project.Root.Elements(ns + "PropertyGroup").First();
                    propertyGroup.Add(new XElement(ns + "PackageToolShimRuntimeIdentifiers", "win-x64;ubuntu-x64"));
                    propertyGroup.Add(new XElement(ns + "ToolCommandName", _customToolCommandName));
                    propertyGroup.Add(new XElement(ns + "PackageId", _customToolCommandName));

                    if (multiTarget)
                    {
                        propertyGroup.Element(ns + "TargetFramework").Remove();
                        propertyGroup.Add(new XElement(ns + "TargetFrameworks", "netcoreapp2.1"));
                    }
                })
                .Restore(Log);

            _testRoot = helloWorldAsset.TestRoot;

            var packCommand = new PackCommand(Log, helloWorldAsset.TestRoot);

            packCommand.Execute();
            _packageId = Path.GetFileNameWithoutExtension(packCommand.ProjectFile);

            return packCommand.GetNuGetPackage();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void It_packs_successfully(bool multiTarget)
        {
            var nugetPackage = SetupNuGetPackage(multiTarget);
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                nupkgReader
                    .GetToolItems()
                    .Should().NotBeEmpty();
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void It_contains_dependencies_dll(bool multiTarget)
        {
            var nugetPackage = SetupNuGetPackage(multiTarget);
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                IEnumerable<NuGet.Frameworks.NuGetFramework> supportedFrameworks = nupkgReader.GetSupportedFrameworks();
                supportedFrameworks.Should().NotBeEmpty();

                foreach (NuGet.Frameworks.NuGetFramework framework in supportedFrameworks)
                {
                    var allItems = nupkgReader.GetToolItems().SelectMany(i => i.Items).ToList();
                    allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/any/Newtonsoft.Json.dll");
                }
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void It_contains_shim(bool multiTarget)
        {
            var nugetPackage = SetupNuGetPackage(multiTarget);
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                IEnumerable<NuGet.Frameworks.NuGetFramework> supportedFrameworks = nupkgReader.GetSupportedFrameworks();
                supportedFrameworks.Should().NotBeEmpty();

                foreach (NuGet.Frameworks.NuGetFramework framework in supportedFrameworks)
                {
                    var allItems = nupkgReader.GetToolItems().SelectMany(i => i.Items).ToList();
                    allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/any/shims/win-x64/{_customToolCommandName}.exe",
                        "Name should be the same as the command name even customized");
                    allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/any/shims/ubuntu-x64/{_customToolCommandName}",
                        "RID should be the excat match of the property, even Apphost only has explicitly win, osx and linux");
                }
            }
        }

        [WindowsOnlyTheory]
        [InlineData(true)]
        [InlineData(false)]
        public void It_produce_valid_shims(bool multiTarget)
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                // only sample test on win-x64 since shims are RID specific
                return;
            }
            var nugetPackage = SetupNuGetPackage(multiTarget);
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                IEnumerable<NuGet.Frameworks.NuGetFramework> supportedFrameworks = nupkgReader.GetSupportedFrameworks();
                supportedFrameworks.Should().NotBeEmpty();
                var tmpfilePathRoot = Path.Combine(_testRoot, "temp", Path.GetRandomFileName());

                foreach (NuGet.Frameworks.NuGetFramework framework in supportedFrameworks)
                {
                    CopyPackageAssetToToolLayout("consoledemo.runtimeconfig.json", nupkgReader, tmpfilePathRoot, framework);
                    CopyPackageAssetToToolLayout("consoledemo.deps.json", nupkgReader, tmpfilePathRoot, framework);
                    CopyPackageAssetToToolLayout("try-toolrid.dll", nupkgReader, tmpfilePathRoot, framework);

                    string shimPath = Path.Combine(tmpfilePathRoot, $"{_customToolCommandName}.exe");
                    nupkgReader.ExtractFile(
                        $"tools/{framework.GetShortFolderName()}/any/shims/win-x64/{_customToolCommandName}.exe",
                        shimPath,
                        null);

                    var command = new ShimCommand(_log, _customToolCommandName);

                    command.Execute().Should()
                      .Pass()
                      .And
                      .HaveStdOutContaining("Hello World!");
                }
            }


            //tring copiedFile = nupkgReader.ExtractFile($"tools/{anyTfm}/any/DotnetToolSettings.xml", tmpfilePath, null);
        }

        private void CopyPackageAssetToToolLayout(
            string nupkgAssetName,
            PackageArchiveReader nupkgReader,
            string tmpfilePathRoot, 
            NuGet.Frameworks.NuGetFramework framework)
        {
            var toolLayoutDirectory = 
                Path.Combine(tmpfilePathRoot, ".store", _packageId, _packageVersion, _packageId, _packageVersion, "tools", framework.GetShortFolderName(), "any");
            var destinationFilePath = 
                Path.Combine(toolLayoutDirectory, nupkgAssetName);
            var copiedFile = nupkgReader.ExtractFile($"tools/{framework.GetShortFolderName()}/any/{nupkgAssetName}", destinationFilePath, null);
        }
    }
}
