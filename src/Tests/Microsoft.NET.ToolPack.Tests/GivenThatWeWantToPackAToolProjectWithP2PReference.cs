// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Xml.Linq;
using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using System.Runtime.CompilerServices;
using NuGet.Packaging;
using System.IO;

namespace Microsoft.NET.ToolPack.Tests
{
    public class GivenThatWeWantToPackAToolProjectWithP2PReference : SdkTest
    {

        public GivenThatWeWantToPackAToolProjectWithP2PReference(ITestOutputHelper log) : base(log)
        {

        }

        private string SetupNuGetPackage([CallerMemberName] string callingMethod = "")
        {
            TestAsset testAsset = _testAssetsManager
                .CopyTestAsset("PortableToolWithP2P", callingMethod)
                .WithSource()
                .WithProjectChanges(project =>
                {
                    XNamespace ns = project.Root.Name.Namespace;
                    XElement propertyGroup = project.Root.Elements(ns + "PropertyGroup").First();
                })
                .Restore(Log, "App");

            var appProjectDirectory = Path.Combine(testAsset.TestRoot, "App");
            var packCommand = new PackCommand(Log, appProjectDirectory);

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
    }
}
