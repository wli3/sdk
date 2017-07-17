// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Build.Framework;
using System.IO;

namespace Microsoft.NET.Build.Tasks
{
    public class GetAssemblyInfo : TaskBase
    {
        [Required]
        public string RelativeFilePath { get; set; }

        [Output]
        public string FileVersion { get; set; }

        [Output]
        public string AssemblyVersion { get; set; }


        protected override void ExecuteCore()
        {
            GetAssemblyInfoFrom(Path.GetFullPath(RelativeFilePath));
        }

        private void GetAssemblyInfoFrom(string FilePath)
        {
            FileVersion = FileUtilities.GetFileVersion(FilePath).ToString();
            var TryGetAssemblyVersion = FileUtilities.TryGetAssemblyVersion(FilePath).ToString();
            if (TryGetAssemblyVersion != null)
            {
                AssemblyVersion = TryGetAssemblyVersion;
            }
        }
    }
}
