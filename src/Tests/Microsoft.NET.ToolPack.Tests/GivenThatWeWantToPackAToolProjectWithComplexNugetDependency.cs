// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using NuGet.Packaging;

namespace Microsoft.NET.ToolPack.Tests
{
    public class GivenThatWeWantToPackAToolProjectWithComplexNugetDependency : SdkTest
    {
        public GivenThatWeWantToPackAToolProjectWithComplexNugetDependency(ITestOutputHelper log) : base(log)
        {

        }

        [Fact]
        public void It_finds_the_entry_point_dll_and_put_in_setting_file()
        {
            TestAsset helloWorldAsset = _testAssetsManager
                                        .CopyTestAsset("PortableTool", "PackAToolGraphOfNuGetPackages")
                                        .WithSource()
                                        .WithProjectChanges(project =>
                                        {
                                            AddPackageThatDependesOnOtherPackage(project);
                                        })
                                        .Restore(Log);
            var packCommand = new PackCommand(Log, helloWorldAsset.TestRoot);
            packCommand.Execute();
            var nugetPackage = packCommand.GetNuGetPackage();

            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                IEnumerable<NuGet.Frameworks.NuGetFramework> supportedFrameworks = nupkgReader.GetSupportedFrameworks();
                supportedFrameworks.Should().NotBeEmpty();

                //var immediatePackageDepedencyDll = ""
                foreach (NuGet.Frameworks.NuGetFramework framework in supportedFrameworks)
                {
                    if (framework.GetShortFolderName() == "any")
                    {
                        continue;
                    }

                    var allItems = nupkgReader.GetToolItems().SelectMany(i => i.Items).ToList();
                    //something like this
                    //allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/runtimes/win7-x86/native.dll");
                    allItems.Should().Contain($"tools/{framework.GetShortFolderName()}/any/Newtonsoft.Json.dll"); //some runtime asset
                    //Also asset no package dependence 
                }
            }
        }

        private static void AddPackageThatDependesOnOtherPackage(XDocument project)
        {
            var ns = project.Root.Name.Namespace;

            var itemGroup = new XElement(ns + "ItemGroup");
            project.Root.Add(itemGroup);

            itemGroup.Add(new XElement(ns + "PackageReference", new XAttribute("Include", "System.Data.SqlClient"),
                                                                new XAttribute("Version", "4.3.0")));
        }
    }
}
