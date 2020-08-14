// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Microsoft.NET.TestFramework;
using System.Linq;
using NuGet.Common;
using NuGet.Protocol;

namespace Microsoft.DotNet.TemplateLocator.Tests
{
    public class GivenAnTemplateLocator : SdkTest
    {
        public GivenAnTemplateLocator(ITestOutputHelper logger) : base(logger)
        {
        }

        [Fact]
        public void ItShouldReturnListOfTemplates()
        {
            var resolver = new TemplateLocator();
            var fakeDotnetRootDirectory =
                Path.Combine(TestContext.Current.TestExecutionDirectory, Path.GetRandomFileName());
            var manifestDirectory = Path.Combine(fakeDotnetRootDirectory, "5.0.100-manifests", "5.0.111");
            Directory.CreateDirectory(manifestDirectory);
            File.WriteAllText(Path.Combine(manifestDirectory, "WorkloadManifest.xml"), fakeManifest);
            var result = resolver.GetDotnetSdkTemplatePackages("5.0.102", fakeDotnetRootDirectory);

            result.Should().NotBeEmpty();
        }

        private static string fakeManifest = @"
<WorkloadManifest>
  <Workloads>
    <Workload Name=""Xamarin.Android workload"">
      <RequiredPack Name=""Xamarin.Android.Workload""/>
      <RequiredPack Name=""Xamarin.Android.Templates""/>
    </Workload>
  </Workloads>
  <WorkloadPacks>
    <Pack Name=""Xamarin.Android.Workload""
          Version=""1.0.1""
          Kind=""sdk"" />
    <Pack Name=""Xamarin.Android.Templates""
          Version=""2.0.1""
          Kind=""Template"" />
  </WorkloadPacks>
</WorkloadManifest>
";
    }
}
