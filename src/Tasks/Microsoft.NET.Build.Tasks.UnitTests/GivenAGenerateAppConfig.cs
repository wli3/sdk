// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests
{
    public class GivenAGenerateAppConfig
    {
        [Fact]
        public void It_creates_startup_and_supportedRuntime_nod_when_there_is_not_any()
        {

            var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("configuration"));

            GenerateAppConfig.AddSupportedRuntimeToAppconfigFile(doc, "net452");

            doc.Element("configuration")
                .Elements("startup")
                .Single().Elements()
                .Should().Contain(e => e.Name.LocalName == "supportedRuntime");
        }

        [Fact]
        public void It_creates_supportedRuntime_nod_when_there_is_startup()
        {

            var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("configuration", new XElement("startup")));

            GenerateAppConfig.AddSupportedRuntimeToAppconfigFile(doc, "net452");

            doc.Element("configuration")
                .Elements("startup")
                .Single().Elements()
                .Should().Contain(e => e.Name.LocalName == "supportedRuntime");
        }

        [Fact]
        public void It_does_not_change_supportedRuntime_nod_when_there_is_supportedRuntime()
        {

            var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("configuration",
                    new XElement("startup",
                    new XElement("supportedRuntime",
                    new XAttribute("version", "v4.0"),
                    new XAttribute("sku", ".NETFramework,Version=v4.7.2")))));

            GenerateAppConfig.AddSupportedRuntimeToAppconfigFile(doc, "net461");

            XElement supportedRuntime = doc.Element("configuration")
                .Elements("startup")
                .Single().Elements("supportedRuntime")
                .Single();

            supportedRuntime.Should().HaveAttribute("version", "v4.0");
            supportedRuntime.Should().HaveAttribute("sku", ".NETFramework,Version=v4.7.2");
        }

        [Theory]
        [InlineData("net11", "v1.1.4322")]
        [InlineData("net20", "v2.0.50727")]
        [InlineData("net35", "v2.0.50727")]
        public void It_Generate_correct_version_and_sku_for_below40(string targetframework, string expectedVersion)
        {
            // intersection of https://docs.microsoft.com/en-us/nuget/reference/target-frameworks
            // and https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/startup/supportedruntime-element#version
            var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("configuration"));

            GenerateAppConfig.AddSupportedRuntimeToAppconfigFile(doc, targetframework);

            XElement supportedRuntime = doc.Element("configuration")
                .Elements("startup")
                .Single().Elements("supportedRuntime")
                .Single();

            supportedRuntime.Should().HaveAttribute("version", expectedVersion);
            supportedRuntime.Attribute("sku").Should().BeNull();
        }

        [Theory]
        [InlineData("net45", "v4.0", ".NETFramework,Version=v4.5")]
        [InlineData("net451", "v4.0", ".NETFramework,Version=v4.5.1")]
        [InlineData("net452", "v4.0", ".NETFramework,Version=v4.5.2")]
        [InlineData("net46", "v4.0", ".NETFramework,Version=v4.6")]
        [InlineData("net461", "v4.0", ".NETFramework,Version=v4.6.1")]
        [InlineData("net462", "v4.0", ".NETFramework,Version=v4.6.2")]
        [InlineData("net47", "v4.0", ".NETFramework,Version=v4.7")]
        [InlineData("net471", "v4.0", ".NETFramework,Version=v4.7.1")]
        [InlineData("net472", "v4.0", ".NETFramework,Version=v4.7.2")]
        public void It_Generate_correct_version_and_sku_for_above40(string targetframework, string expectedVersion, string expectedSku)
        {
            var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("configuration"));

            GenerateAppConfig.AddSupportedRuntimeToAppconfigFile(doc, targetframework);

            XElement supportedRuntime = doc.Element("configuration")
                .Elements("startup")
                .Single().Elements("supportedRuntime")
                .Single();

            supportedRuntime.Should().HaveAttribute("version", expectedVersion);
            supportedRuntime.Should().HaveAttribute("sku", expectedSku);
        }
    }
}
