// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Microsoft.DotNet.Build.Tasks
{
    /// <summary>
    /// Use the runtime in dotnet/sdk instead of in the stage 0 to avoid circualr dependency.
    /// If there is a change dependend on the latest runtime. Without override the runtime version in BundledNETCoreAppPackageVersion
    /// we would need to somehow get this change in without the test, and then insertion dotnet/installer
    /// and then update the stage 0 back.
    /// 
    /// Use a task to override since it was generated as a string literal replace anyway.
    /// And using C# can have better error when anything goes wrong.
    /// </summary>
    public sealed class OverrideAndCreateBundledNETCoreAppPackageVersion : Task
    {
        [Required]
        public string Stage0MicrosoftNETCoreAppRefPackageVersionPath { get; set; }

        [Required]
        public string MicrosoftNETCoreAppRefPackageVersion { get; set; }

        [Required]
        public string OutputPath { get; set; }

        public override bool Execute()
        {
            return ExecuteInternal(Stage0MicrosoftNETCoreAppRefPackageVersionPath, MicrosoftNETCoreAppRefPackageVersion, OutputPath);
        }

        public static bool ExecuteInternal(string stage0MicrosoftNETCoreAppRefPackageVersionPath, string microsoftNETCoreAppRefPackageVersion, string outputPath)
        {
            File.WriteAllText(outputPath, Path.GetFullPath(stage0MicrosoftNETCoreAppRefPackageVersionPath) + "  " + microsoftNETCoreAppRefPackageVersion);
            return true;
        }
    }
}
