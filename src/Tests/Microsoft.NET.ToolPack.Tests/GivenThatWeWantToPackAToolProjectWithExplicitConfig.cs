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
    public class GivenThatWeWantToPackAToolProjectWithExplicitConfig : SdkTest
    {

        public GivenThatWeWantToPackAToolProjectWithExplicitConfig(ITestOutputHelper log) : base(log)
        {

        }

        [Fact]
        public void And_explicit_entry_point_name_it_finds_the_entry_point_dll_and_put_in_setting_file()
        {
        }


        [Fact(Skip = "Pending")]
        public void And_explicit_commandName_it_finds_commandName_and_put_in_setting_file()
        {
            const string explicitCommandName = "explicit_command_name";
            TestAsset helloWorldAsset = _testAssetsManager
            .CopyTestAsset("PortableTool", "PackPortableTool" + Path.GetRandomFileName())
            .WithSource()
            .WithProjectChanges(project =>
            {
                var ns = project.Root.Name.Namespace;
                var propertyGroup = project.Root.Elements(ns + "PropertyGroup").First();
                propertyGroup.Add(new XElement("ToolCommandName", "explicit_command_name"));
            })
            .Restore(Log);

            var packCommand = new PackCommand(Log, helloWorldAsset.TestRoot);

            packCommand.Execute();

            var nugetPackage = packCommand.GetNuGetPackage();

            using (var nupkgReader = new PackageArchiveReader(nugetPackage))
            {
                var tmpfilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                string copiedFile = nupkgReader.ExtractFile($"tools/DotnetToolSettings.xml", tmpfilePath, null);
                File.ReadAllText(copiedFile)
                    .Should()
                    .Contain(explicitCommandName);
            }
        }
    }
}
