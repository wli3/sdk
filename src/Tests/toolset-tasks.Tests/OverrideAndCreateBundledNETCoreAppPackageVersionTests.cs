// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.DotNet.Build.Tasks;
using Xunit;

namespace Microsoft.DotNet.Cli.Build.Tests
{
    public class OverrideAndCreateBundledNETCoreAppPackageVersionTests
    {
        [Fact]
        public void AppleSauce()
        {
            string result = OverrideAndCreateBundledNETCoreAppPackageVersion
                            .ExecuteInternal(File.ReadAllText("Microsoft.NETCoreSdk.BundledVersions.props.input"), "5.0.0-rc.1.20410.10");
            result.Should().Be(File.ReadAllText("Microsoft.NETCoreSdk.BundledVersions.props.expected"));
        }
    }
}
