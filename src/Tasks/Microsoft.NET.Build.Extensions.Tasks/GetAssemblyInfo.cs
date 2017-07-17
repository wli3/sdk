// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Build.Framework;
using System.Diagnostics;
using System.IO;

namespace Microsoft.NET.Build.Tasks
{
    public class GetAssemblyInfo : TaskBase
    {
        [Required]
        public string RelativeFilePath { get; set; }

        [Output]
        public string CompanyName { get; private set; }
        [Output]
        public string LegalCopyright { get; private set; }
        [Output]
        public string Comments { get; private set; }
        [Output]
        public string FileVersion { get; private set; }
        [Output]
        public string ProductVersion { get; private set; }
        [Output]
        public string ProductName { get; private set; }
        [Output]
        public string FileDescription { get; private set; }
        [Output]
        public string AssemblyVersion { get; private set; }


        protected override void ExecuteCore()
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(Path.GetFullPath(RelativeFilePath));
            CompanyName = fileVersionInfo.CompanyName;
            LegalCopyright = fileVersionInfo.LegalCopyright;
            Comments = fileVersionInfo.Comments;
            FileVersion = fileVersionInfo.FileVersion;
            ProductVersion = fileVersionInfo.ProductVersion;
            ProductName = fileVersionInfo.ProductName;
            FileDescription = fileVersionInfo.FileDescription;

            var TryGetAssemblyVersion = FileUtilities.TryGetAssemblyVersion(Path.GetFullPath(RelativeFilePath));
            if (TryGetAssemblyVersion != null)
            {
                AssemblyVersion = TryGetAssemblyVersion.ToString();
            }
        }
    }
}
