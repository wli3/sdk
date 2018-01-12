// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Runtime.InteropServices;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using Xunit;
using System.Xml.Linq;
using Xunit.Abstractions;

namespace Microsoft.NET.Publish.Tests
{
    public class GivenThatWeWantToExcludeAPackageFromPublish : SdkTest
    {
        public GivenThatWeWantToExcludeAPackageFromPublish(ITestOutputHelper log) : base(log)
        {
        }

        [Fact]
        public void It_does_not_publish_a_PackageReference_with_PrivateAssets_All()
        {
            var helloWorldAsset = _testAssetsManager
                .CopyTestAsset("HelloWorld", "PublishExcludePackage")
                .WithSource()
                .WithProjectChanges(project =>
                {
                    AddPackageThatDependesOnOtherPackage(project);
                })
                .Restore(Log);

            var publishCommand = new PublishCommand(Log, helloWorldAsset.TestRoot);
            var publishResult = publishCommand.Execute();

            publishResult.Should().Pass();

            var publishDirectory = publishCommand.GetOutputDirectory();

            publishDirectory.Should().OnlyHaveFiles(new[] {
                "HelloWorld.dll",
                "HelloWorld.pdb",
                "HelloWorld.deps.json",
                "HelloWorld.runtimeconfig.json"
            });
        }

        private static void AddPackageThatDependesOnOtherPackage(XDocument project)
        {
            var ns = project.Root.Name.Namespace;

            var itemGroup = new XElement(ns + "ItemGroup");
            project.Root.Add(itemGroup);

            itemGroup.Add(new XElement(ns + "PackageReference", new XAttribute("Include", "System.Data.SqlClient"),
                                                                new XAttribute("Version", "4.3.0")));
        }

        [Fact]
        public void It_does_not_publish_a_PackageReference_with_Publish_false()
        {
            var helloWorldAsset = _testAssetsManager
                .CopyTestAsset("HelloWorld", "PublishPackagePublishFalse")
                .WithSource()
                .WithProjectChanges(project =>
                {
                    var ns = project.Root.Name.Namespace;

                    var itemGroup = new XElement(ns + "ItemGroup");
                    project.Root.Add(itemGroup);

                    itemGroup.Add(new XElement(ns + "PackageReference", new XAttribute("Include", "Newtonsoft.Json"),
                                                                        new XAttribute("Version", "9.0.1"),
                                                                        new XAttribute("Publish", "false")));
                })
                .Restore(Log);

            var publishCommand = new PublishCommand(Log, helloWorldAsset.TestRoot);
            var publishResult = publishCommand.Execute();

            publishResult.Should().Pass();

            var publishDirectory = publishCommand.GetOutputDirectory();

            publishDirectory.Should().OnlyHaveFiles(new[] {
                "HelloWorld.dll",
                "HelloWorld.pdb",
                "HelloWorld.deps.json",
                "HelloWorld.runtimeconfig.json"
            });
        }

        [Fact]
        public void It_publishes_a_PackageReference_with_PrivateAssets_All_and_Publish_true()
        {
            var helloWorldAsset = _testAssetsManager
                .CopyTestAsset("HelloWorld", "PublishPrivateAssets")
                .WithSource()
                .WithProjectChanges(project =>
                {
                    var ns = project.Root.Name.Namespace;

                    var itemGroup = new XElement(ns + "ItemGroup");
                    project.Root.Add(itemGroup);

                    itemGroup.Add(new XElement(ns + "PackageReference", new XAttribute("Include", "Newtonsoft.Json"),
                                                                        new XAttribute("Version", "9.0.1"),
                                                                        new XAttribute("PrivateAssets", "All"),
                                                                        new XAttribute("Publish", "true")));
                })
                .Restore(Log);

            var publishCommand = new PublishCommand(Log, helloWorldAsset.TestRoot);
            var publishResult = publishCommand.Execute();

            publishResult.Should().Pass();

            var publishDirectory = publishCommand.GetOutputDirectory();

            publishDirectory.Should().OnlyHaveFiles(new[] {
                "HelloWorld.dll",
                "HelloWorld.pdb",
                "HelloWorld.deps.json",
                "HelloWorld.runtimeconfig.json",
                "Newtonsoft.Json.dll",
                "System.Runtime.Serialization.Primitives.dll"
            });
        }
    }
}
