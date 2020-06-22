using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Build.Framework;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests
{
    public class ProcessFrameworkReferencesTests
    {
        [Fact]
        public void It_resolves_FrameworkReferences()
        {
            var task = new ProcessFrameworkReferences();

            task.EnableTargetingPackDownload = true;
            task.TargetFrameworkIdentifier = ".NETCoreApp";
            task.TargetFrameworkVersion = "3.0";
            task.FrameworkReferences = new[]
            {
                new MockTaskItem("Microsoft.AspNetCore.App", new Dictionary<string, string>())
            };

            task.KnownFrameworkReferences = new[]
            {
                new MockTaskItem("Microsoft.AspNetCore.App",
                    new Dictionary<string, string>()
                    {
                        { "TargetFramework", "netcoreapp3.0" },
                        { "RuntimeFrameworkName", "Microsoft.AspNetCore.App" },
                        { "DefaultRuntimeFrameworkVersion", "1.9.5" },
                        { "LatestRuntimeFrameworkVersion", "1.9.6" },
                        { "TargetingPackName", "Microsoft.AspNetCore.App" },
                        { "TargetingPackVersion", "1.9.0" }
                    })
            };

            task.Execute().Should().BeTrue();

            task.PackagesToDownload.Length.Should().Be(1);

            task.RuntimeFrameworks.Length.Should().Be(1);
            task.RuntimeFrameworks[0].ItemSpec.Should().Be("Microsoft.AspNetCore.App");
            task.RuntimeFrameworks[0].GetMetadata(MetadataKeys.Version).Should().Be("1.9.5");
        }

        [Fact]
        public void It_does_not_resolve_FrameworkReferences_if_targetframework_doesnt_match()
        {
            var task = new ProcessFrameworkReferences();

            task.TargetFrameworkIdentifier = ".NETCoreApp";
            task.TargetFrameworkVersion = "2.0";
            task.FrameworkReferences = new[]
            {
                new MockTaskItem("Microsoft.AspNetCore.App", new Dictionary<string, string>())
            };

            task.KnownFrameworkReferences = new[]
            {
                new MockTaskItem("Microsoft.AspNetCore.App",
                    new Dictionary<string, string>()
                    {
                        { "TargetFramework", "netcoreapp3.0" },
                        { "RuntimeFrameworkName", "Microsoft.AspNetCore.App" },
                        { "DefaultRuntimeFrameworkVersion", "1.9.5" },
                        { "LatestRuntimeFrameworkVersion", "1.9.6" },
                        { "TargetingPackName", "Microsoft.AspNetCore.App" },
                        { "TargetingPackVersion", "1.9.0" }
                    })
            };

            task.Execute().Should().BeTrue();

            task.PackagesToDownload.Should().BeNull();
            task.RuntimeFrameworks.Should().BeNull();
        }

        [Fact]
        public void Given_KnownFrameworkReferences_with_RuntimeCopyLocal_It_resolves_FrameworkReferences()
        {
            var task = new ProcessFrameworkReferences
            {
                BuildEngine = new MockNeverCacheBuildEngine4(),
                EnableTargetingPackDownload = true,
                TargetFrameworkIdentifier = ".NETCoreApp",
                TargetFrameworkVersion = "5.0",
                RuntimeGraphPath =
                    @"C:\work\sdk\artifacts\bin\redist\Debug\dotnet\sdk\5.0.100-dev\RuntimeIdentifierGraph.json",
                FrameworkReferences =
                    new[] {new MockTaskItem("Microsoft.Windows.Ref", new Dictionary<string, string>())},
                KnownFrameworkReferences = new[]
                {
                    new MockTaskItem("Microsoft.Windows.Ref",
                        new Dictionary<string, string>()
                        {
                            {"TargetFramework", "netcoreapp5.0"},
                            {"RuntimeFrameworkName", "Microsoft.Windows.Ref"},
                            {"DefaultRuntimeFrameworkVersion", "5.0.0-preview1"},
                            {"LatestRuntimeFrameworkVersion", "5.0.0-preview1"},
                            {"TargetingPackName", "Microsoft.Windows.Ref"},
                            {"TargetingPackVersion", "5.0.0-preview1"},
                            {"RuntimePackNamePatterns", "Microsoft.Windows.Ref"},
                            {"RuntimePackRuntimeIdentifiers", "any"},
                            {MetadataKeys.RuntimeCopyLocal, "true"},
                            {"IsWindowsOnly", "true"},
                        })
                }
            };

            // TODO wul not hard code it


            task.Execute().Should().BeTrue();

            task.PackagesToDownload.Length.Should().Be(1);

            task.RuntimeFrameworks.Should().BeNullOrEmpty(
                "Should not contain RuntimeCopyLocal framework, or it will be put into runtimeconfig.json");

            task.TargetingPacks.Length.Should().Be(1);
            task.TargetingPacks[0].ItemSpec.Should().Be("Microsoft.Windows.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.NuGetPackageId).Should().Be("Microsoft.Windows.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.NuGetPackageVersion).Should().Be("5.0.0-preview1");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.PackageConflictPreferredPackages).Should()
                .Be("Microsoft.Windows.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.RuntimeFrameworkName).Should()
                .Be("Microsoft.Windows.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.RuntimeIdentifier).Should().Be("");

            task.RuntimePacks.Length.Should().Be(1);
            task.RuntimePacks[0].ItemSpec.Should().Be("Microsoft.Windows.Ref");
            task.RuntimePacks[0].GetMetadata(MetadataKeys.FrameworkName).Should().Be("Microsoft.Windows.Ref");
            task.RuntimePacks[0].GetMetadata(MetadataKeys.NuGetPackageId).Should().Be("5.0.0-preview1");
            task.RuntimePacks[0].GetMetadata(MetadataKeys.RuntimeCopyLocal).Should().Be("true");
        }
    }
}
