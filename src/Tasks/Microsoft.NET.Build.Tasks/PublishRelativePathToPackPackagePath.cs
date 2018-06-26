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

        
        [Output]
        public ITaskItem[] ResolvedFileToPublishWithPackagePath { get; private set; }

        protected override void ExecuteCore()
        {
            var result = new TaskItem();
            foreach (var r in ResolvedFileToPublish)
            {
                result.Add(Item);
            }
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
                return= publishRelativePath.Substring(0, index);
            }

        }



    }
}
