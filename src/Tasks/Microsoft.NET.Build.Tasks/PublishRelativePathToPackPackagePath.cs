// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Microsoft.NET.Build.Tasks
{
    public sealed class PublishRelativePathToPackPackagePath : TaskBase
    {
        [Required]
        public ITaskItem[] ResolvedFileToPublish { get; set; }

        [Required]
        public string PublishDir { get; set; }

        [Required]
        public string TargetFramework { get; set; }


        [Output]
        public ITaskItem[] ResolvedFileToPublishWithPackagePath { get; private set; }

        protected override void ExecuteCore()
        {
            var result = new List<TaskItem>();
            foreach (var r in ResolvedFileToPublish)
            {
                string relativePath = r.GetMetadata("RelativePath");
                var fullpath = Path.GetFullPath(
                    Path.Combine(PublishDir,
                    relativePath));
                var i = new TaskItem(fullpath);
                i.SetMetadata("PackagePath",$"tools/{TargetFramework}/any/{AppleSauce(relativePath)}/");
                result.Add(i);
            }

            ResolvedFileToPublishWithPackagePath = result.ToArray();
        }

        private string AppleSauce(string publishRelativePath)
        {
            var index = publishRelativePath.LastIndexOf('/');
            if (index == -1)
            {
                return string.Empty;
            }
            else
            {
                return publishRelativePath.Substring(0, index);
            }

        }



    }
}
