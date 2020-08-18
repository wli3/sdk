// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
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
            File.WriteAllText(OutputPath, ExecuteInternal(File.ReadAllText(Stage0MicrosoftNETCoreAppRefPackageVersionPath), MicrosoftNETCoreAppRefPackageVersion));
            return true;
        }

        public static string ExecuteInternal(string stage0MicrosoftNETCoreAppRefPackageVersionContent, string microsoftNETCoreAppRefPackageVersion)
        {
            var projectXml = XDocument.Parse(stage0MicrosoftNETCoreAppRefPackageVersionContent);

            var ns = projectXml.Root.Name.Namespace;

            var propertyGroup = projectXml.Root.Elements(ns + "PropertyGroup").First();

            var originalBundledNETCoreAppPackageVersion = propertyGroup.Element(ns + "BundledNETCoreAppPackageVersion").Value;

            //propertyGroup.Element(ns + "BundledNETCoreAppPackageVersion").Value = microsoftNETCoreAppRefPackageVersion;

            return projectXml.ToString();
        }
    }
}
