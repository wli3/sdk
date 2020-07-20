using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests
{
    public class CheckForUnsupportedWindowsTargetPlatformVersionTests
    {
        private readonly MockTaskItem[] _knownFrameworkReferences = new[]
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
                    {MetadataKeys.RuntimePackAlwaysCopyLocal, "true"},
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
                    {MetadataKeys.RuntimePackAlwaysCopyLocal, "true"},
                    {"IsWindowsOnly", "true"},
                }),
        };
        
        [Fact]
        public void Given_matching_TargetPlatformVersion_it_should_pass()
        {
            var task = new CheckForUnsupportedWindowsTargetPlatformVersion
            {
                BuildEngine = new MockNeverCacheBuildEngine4(),
                TargetPlatformVersion = "10.0.18362",
                KnownFrameworkReferences = _knownFrameworkReferences
            };

            task.Execute().Should().BeTrue();
        }
        
        [Fact]
        public void Given_no_matching_TargetPlatformVersion_it_should_error()
        {
            var task = new CheckForUnsupportedWindowsTargetPlatformVersion
            {
                BuildEngine = new MockNeverCacheBuildEngine4(),
                TargetPlatformVersion = "10.0.9999",
                KnownFrameworkReferences = _knownFrameworkReferences
            };

            task.Execute().Should().BeFalse();
        }
    }
}
