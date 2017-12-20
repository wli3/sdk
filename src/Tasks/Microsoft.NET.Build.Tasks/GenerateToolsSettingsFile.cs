using System;
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

        [Output]
        public ITaskItem FileWritten
        {
            get; private set;
        }

        protected override void ExecuteCore()
        {
           
        }
    }
}
