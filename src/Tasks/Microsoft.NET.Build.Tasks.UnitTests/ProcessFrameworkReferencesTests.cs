﻿using System.Collections.Generic;
using System.IO;
using FluentAssertions;
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
        public void Given_targetPlatform_and_targetPlatform_version_It_resolves_FrameworkReferences_()
        {
            var task = new ProcessFrameworkReferences();

            task.EnableTargetingPackDownload = true;
            task.TargetFrameworkIdentifier = ".NETCoreApp";
            task.TargetFrameworkVersion = "3.0";
            task.TargetPlatformIdentifier = "Windows";
            task.TargetPlatformVersion = "10.0.18362";
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
            const string minimalRuntimeGraphPathContent =
                "{\"runtimes\":{\"any\":{\"#import\":[\"base\"]},\"base\":{\"#import\":[]}}}";
            var runtimeGraphPathPath = Path.GetTempFileName();
            File.WriteAllText(runtimeGraphPathPath, minimalRuntimeGraphPathContent);

            var task = new ProcessFrameworkReferences
            {
                BuildEngine = new MockNeverCacheBuildEngine4(),
                EnableTargetingPackDownload = true,
                TargetFrameworkIdentifier = ".NETCoreApp",
                TargetFrameworkVersion = "5.0",
                TargetPlatformIdentifier = "Windows",
                TargetPlatformVersion = "10.0.18362",
                RuntimeGraphPath =
                    runtimeGraphPathPath,
                FrameworkReferences =
                    new[] {new MockTaskItem("Microsoft.Windows.SDK.NET.Ref", new Dictionary<string, string>())},
                KnownFrameworkReferences = new[]
                {
                    new MockTaskItem("Microsoft.Windows.SDK.NET.Ref",
                        new Dictionary<string, string>
                        {
                            {"TargetFramework", "net5.0-windows10.0.17760"},
                            {"RuntimeFrameworkName", "Microsoft.Windows.SDK.NET.Ref"},
                            {"DefaultRuntimeFrameworkVersion", "10.0.17760.1-preview"},
                            {"LatestRuntimeFrameworkVersion", "10.0.17760.1-preview"},
                            {"TargetingPackName", "Microsoft.Windows.SDK.NET.Ref"},
                            {"TargetingPackVersion", "10.0.17760.1-preview"},
                            {"RuntimePackNamePatterns", "Microsoft.Windows.SDK.NET.Ref"},
                            {"RuntimePackRuntimeIdentifiers", "any"},
                            {MetadataKeys.RuntimeCopyLocal, "true"},
                            {"IsWindowsOnly", "true"},
                        }),
                    new MockTaskItem("Microsoft.Windows.SDK.NET.Ref",
                        new Dictionary<string, string>
                        {
                            {"TargetFramework", "net5.0-windows10.0.18362"},
                            {"RuntimeFrameworkName", "Microsoft.Windows.SDK.NET.Ref"},
                            {"DefaultRuntimeFrameworkVersion", "10.0.18362.1-preview"},
                            {"LatestRuntimeFrameworkVersion", "10.0.18362.1-preview"},
                            {"TargetingPackName", "Microsoft.Windows.SDK.NET.Ref"},
                            {"TargetingPackVersion", "10.0.18362.1-preview"},
                            {"RuntimePackNamePatterns", "Microsoft.Windows.SDK.NET.Ref"},
                            {"RuntimePackRuntimeIdentifiers", "any"},
                            {MetadataKeys.RuntimeCopyLocal, "true"},
                            {"IsWindowsOnly", "true"},
                        }),
                }
            };

            task.Execute().Should().BeTrue();

            task.PackagesToDownload.Length.Should().Be(1);

            task.RuntimeFrameworks.Should().BeNullOrEmpty(
                "Should not contain RuntimeCopyLocal framework, or it will be put into runtimeconfig.json");

            task.TargetingPacks.Length.Should().Be(1);
            task.TargetingPacks[0].ItemSpec.Should().Be("Microsoft.Windows.SDK.NET.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.NuGetPackageId).Should().Be("Microsoft.Windows.SDK.NET.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.NuGetPackageVersion).Should().Be("10.0.18362.1-preview");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.PackageConflictPreferredPackages).Should()
                .Be("Microsoft.Windows.SDK.NET.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.RuntimeFrameworkName).Should()
                .Be("Microsoft.Windows.SDK.NET.Ref");
            task.TargetingPacks[0].GetMetadata(MetadataKeys.RuntimeIdentifier).Should().Be("");

            task.RuntimePacks.Length.Should().Be(1);
            task.RuntimePacks[0].ItemSpec.Should().Be("Microsoft.Windows.SDK.NET.Ref");
            task.RuntimePacks[0].GetMetadata(MetadataKeys.FrameworkName).Should().Be("Microsoft.Windows.SDK.NET.Ref");
            task.RuntimePacks[0].GetMetadata(MetadataKeys.NuGetPackageVersion).Should().Be("10.0.18362.1-preview");
            task.RuntimePacks[0].GetMetadata(MetadataKeys.RuntimeCopyLocal).Should().Be("true");
        }
    }
}
