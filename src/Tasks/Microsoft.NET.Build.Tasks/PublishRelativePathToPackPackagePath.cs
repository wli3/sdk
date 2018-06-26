// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            foreach (ITaskItem r in ResolvedFileToPublish)
            {
                string relativePath = r.GetMetadata("RelativePath");
                var fullpath = Path.GetFullPath(
                    Path.Combine(PublishDir,
                    relativePath));
                var i = new TaskItem(fullpath);
                i.SetMetadata("PackagePath", $"tools/{TargetFramework}/any/{GetDirectoryPathInRelativePath(relativePath)}");
                result.Add(i);
            }

            ResolvedFileToPublishWithPackagePath = result.ToArray();
        }

        internal static string GetDirectoryPathInRelativePath(string publishRelativePath)
        {
            publishRelativePath = NormalizeDirectorySeparators(publishRelativePath);
            var index = publishRelativePath.LastIndexOf(AltDirectorySeparatorChar);
            if (index == -1)
            {
                return string.Empty;
            }
            else
            {
                return publishRelativePath.Substring(0, index);
            }
        }

        // https://github.com/dotnet/corefx/issues/4208
        // Basic copy paste from corefx. But Normalize to "/" instead of \
        private static string NormalizeDirectorySeparators(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            char current;

            StringBuilder builder = new StringBuilder(path.Length);

            int start = 0;
            if (IsDirectorySeparator(path[start]))
            {
                start++;
                builder.Append(AltDirectorySeparatorChar);
            }

            for (int i = start; i < path.Length; i++)
            {
                current = path[i];

                // If we have a separator
                if (IsDirectorySeparator(current))
                {
                    // If the next is a separator, skip adding this
                    if (i + 1 < path.Length && IsDirectorySeparator(path[i + 1]))
                    {
                        continue;
                    }

                    // Ensure it is the primary separator
                    current = AltDirectorySeparatorChar;
                }

                builder.Append(current);
            }

            return builder.ToString();
        }

        private static bool IsDirectorySeparator(char c)
        {
            return c == DirectorySeparatorChar || c == AltDirectorySeparatorChar;
        }

        private const char DirectorySeparatorChar = '\\';
        private const char AltDirectorySeparatorChar = '/';
    }
}
