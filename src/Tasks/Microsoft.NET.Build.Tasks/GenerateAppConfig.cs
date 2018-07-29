// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.Frameworks;
using NuGet.ProjectModel;

namespace Microsoft.NET.Build.Tasks
{
    public sealed class GenerateAppConfig : TaskBase
    {

        [Required]
        public string AppConfigFile { get; set; }

        [Required]
        public string OutputAppConfigFile { get; set; }

        protected override void ExecuteCore()
        {
            
           
        }


    }
}
