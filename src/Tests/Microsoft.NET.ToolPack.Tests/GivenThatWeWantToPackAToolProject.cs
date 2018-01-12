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

namespace Microsoft.NET.ToolPack.Tests
{
    public class GivenThatWeWantToPackAToolProject : SdkTest
    {

        public GivenThatWeWantToPackAToolProject(ITestOutputHelper log) : base(log)
        {
        }

        private string SetupNuGetPackage()
        {
            TestAsset helloWorldAsset = _testAssetsManager
                .CopyTestAsset("PortableTool")
                //.WithSource( /* (multi targeted vs non ) */ )
                .Restore(Log);

            var packCommand = new PackCommand(Log, helloWorldAsset.TestRoot);

            packCommand.Execute();

            return packCommand.GetNuGetPackage();
        }

        [Fact]
        public void It_packs_successfully()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                nupkgReader
                    .GetToolItems()
                    .Should().NotBeEmpty();
            }
        }

        [Fact]
        public void It_finds_the_entry_point_dll_and_command_name_and_put_in_setting_file()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                var anyTfm = nupkgReader.GetSupportedFrameworks().First().GetShortFolderName();
                var tmpfilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                string copiedFile = nupkgReader.ExtractFile($"tools/{anyTfm}/any/DotnetToolSettings.xml", tmpfilePath, null);
                XElement command = XDocument.Load(copiedFile)
                                      .Element("DotNetCliTool")
                                      .Element("Commands")
                                      .Element("Command");

                command.Attribute("Name")
                        .Value
                        .Should().Be("consoledemo", "it should contain command name that is same as the msbuild well known properties $(TargetName)");

                command.Attribute("EntryPoint")
                        .Value
                        .Should().Be("consoledemo.dll", "it should contain entry point dll that is same as the msbuild well known properties $(TargetFileName)");

            }
        }

        [Fact]
        public void It_adds_platform_package_to_dependency_and_remove_other_package_dependency()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                nupkgReader
                    .GetPackageDependencies().First().Packages
                    .Should().OnlyContain(p => p.Id == "Microsoft.NETCore.Platforms");
            }
        }

        [Fact]
        public void It_contains_runtimeconfig_for_each_tfm()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                IEnumerable<NuGet.Frameworks.NuGetFramework> supportedFrameworks = nupkgReader.GetSupportedFrameworks();
                supportedFrameworks.Should().NotBeEmpty();

                foreach (NuGet.Frameworks.NuGetFramework framework in supportedFrameworks)
                {
                    if (framework.GetShortFolderName() == "any")
                    {
                        continue;
                    }

                    var allItems = nupkgReader.GetToolItems().SelectMany(i => i.Items).ToList();
                    allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/any/consoledemo.runtimeconfig.json");
                }
            }
        }

        [Fact]
        public void It_contains_DotnetToolSettingsXml_for_each_tfm()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                IEnumerable<NuGet.Frameworks.NuGetFramework> supportedFrameworks = nupkgReader.GetSupportedFrameworks();
                supportedFrameworks.Should().NotBeEmpty();

                foreach (NuGet.Frameworks.NuGetFramework framework in supportedFrameworks)
                {
                    if (framework.GetShortFolderName() == "any")
                    {
                        continue;
                    }

                    var allItems = nupkgReader.GetToolItems().SelectMany(i => i.Items).ToList();
                    allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/any/DotnetToolSettings.xml");
                }
            }
        }

        [Fact]
        public void It_does_not_contain_lib()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                nupkgReader.GetLibItems().Should().BeEmpty();
            }
        }

        [Fact]
        public void It_contains_folder_structure_tfm_any()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                nupkgReader
                    .GetToolItems()
                    .Should().Contain(
                        f => f.Items.
                            Contains($"tools/{f.TargetFramework.GetShortFolderName()}/any/consoledemo.dll"));
            }
        }

        [Fact]
        public void It_contains_packagetype_dotnettool()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                nupkgReader
                    .GetPackageTypes().Should().ContainSingle(t => t.Name == "DotnetTool");
            }
        }

        [Fact]
        public void It_contains_dependencies_dll()
        {
            var nugetPackage = SetupNuGetPackage();
            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                IEnumerable<NuGet.Frameworks.NuGetFramework> supportedFrameworks = nupkgReader.GetSupportedFrameworks();
                supportedFrameworks.Should().NotBeEmpty();

                foreach (NuGet.Frameworks.NuGetFramework framework in supportedFrameworks)
                {
                    if (framework.GetShortFolderName() == "any")
                    {
                        continue;
                    }

                    var allItems = nupkgReader.GetToolItems().SelectMany(i => i.Items).ToList();
                    allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/any/Newtonsoft.Json.dll");
                }
            }
        }
    }
}
