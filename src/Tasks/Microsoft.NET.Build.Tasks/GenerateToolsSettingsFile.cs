﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Linq;

namespace Microsoft.NET.Build.Tasks
{
    public class GenerateToolsSettingsFile : TaskBase
    {
        [Required]
        public string EntryPointRelativePath{ get; set; }

        [Required]
        public string CommandName{ get; set; }


        //  LeftKey and RightKey: The metadata to join on.  If not set, then use the ItemSpec
        public string LeftKey { get; set; }

        public string RightKey { get; set; }


        //  LeftMetadata and RightMetadata: The metadata names to include in the result.  Specify "*" to include all metadata
        public string[] LeftMetadata { get; set; }

        public string[] RightMetadata { get; set; }


        [Output]
        public ITaskItem[] JoinResult
        {
            get; private set;
        }

        protected override void ExecuteCore()
        {
            bool useAllLeftMetadata = LeftMetadata != null && LeftMetadata.Length == 1 && LeftMetadata[0] == "*";
            bool useAllRightMetadata = RightMetadata != null && RightMetadata.Length == 1 && RightMetadata[0] == "*";

            JoinResult = Left.Join(Right,
                item => GetKeyValue(LeftKey, item),
                item => GetKeyValue(RightKey, item),
                (left, right) =>
                {
                    //  If including all metadata from left items and none from right items, just return left items directly
                    if (useAllLeftMetadata &&
                        string.IsNullOrEmpty(LeftKey) &&
                        (RightMetadata == null || RightMetadata.Length == 0))
                    {
                        return left;
                    }

                    //  If including all metadata from right items and none from left items, just return the right items directly
                    if (useAllRightMetadata &&
                        string.IsNullOrEmpty(RightKey) &&
                        (LeftMetadata == null || LeftMetadata.Length == 0))
                    {
                        return right;
                    }

                    var ret = new TaskItem(GetKeyValue(LeftKey, left));

                    //  Weird ordering here is to prefer left metadata in all cases, as CopyToMetadata doesn't overwrite any existing metadata
                    if (useAllLeftMetadata)
                    {
                        //  CopyMetadata adds an OriginalItemSpec, which we don't want.  So we subsequently remove it
                        left.CopyMetadataTo(ret);
                        ret.RemoveMetadata(MetadataKeys.OriginalItemSpec);
                    }

                    if (!useAllRightMetadata && RightMetadata != null)
                    {
                        foreach (string name in RightMetadata)
                        {
                            ret.SetMetadata(name, right.GetMetadata(name));
                        }
                    }

                    if (!useAllLeftMetadata && LeftMetadata != null)
                    {
                        foreach (string name in LeftMetadata)
                        {
                            ret.SetMetadata(name, left.GetMetadata(name));
                        }
                    }

                    if (useAllRightMetadata)
                    {
                        //  CopyMetadata adds an OriginalItemSpec, which we don't want.  So we subsequently remove it
                        right.CopyMetadataTo(ret);
                        ret.RemoveMetadata(MetadataKeys.OriginalItemSpec);
                    }

                    return (ITaskItem)ret;
                },
                StringComparer.OrdinalIgnoreCase).ToArray();            
        }

        static string GetKeyValue(string key, ITaskItem item)
        {
            if (string.IsNullOrEmpty(key))
            {
                return item.ItemSpec;
            }
            else
            {
                return item.GetMetadata(key);
            }
        }
    }
}
