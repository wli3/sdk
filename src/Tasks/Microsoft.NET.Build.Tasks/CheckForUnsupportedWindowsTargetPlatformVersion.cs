using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.Frameworks;

namespace Microsoft.NET.Build.Tasks
{
    public class CheckForUnsupportedWindowsTargetPlatformVersion : TaskBase
    {
        [Required]
        public ITaskItem[] KnownFrameworkReferences { get; set; } = Array.Empty<ITaskItem>();

        [Required]
        public string TargetFrameworkIdentifier { get; set; }

        [Required]
        public string TargetFrameworkVersion { get; set; }

        public string TargetPlatformIdentifier { get; set; }

        public string TargetPlatformVersion { get; set; }

        public CheckForUnsupportedWindowsTargetPlatformVersion()
        {
        }

        protected override void ExecuteCore()
        {
            if (TargetPlatformIdentifier != "Windows")
            {
                return;
            }
            
            var knownFrameworkReferencesForTargetFramework =
                KnownFrameworkReferences
                    .Select(item => new KnownFrameworkReference(item))
                    .Where(kfr => kfr.KnownFrameworkReferenceAppliesToTargetFramework(
                        TargetFrameworkIdentifier,
                        TargetFrameworkVersion,
                        TargetPlatformVersion));

            if (!knownFrameworkReferencesForTargetFramework.Any())
            {
                var availableVersions = KnownFrameworkReferences
                    .Select(item => new KnownFrameworkReference(item).TargetFramework.PlatformVersion);
                Log.LogError(string.Format(Strings.InvalidTargetPlatformVersion, TargetPlatformVersion, TargetPlatformIdentifier, string.Join(" ", availableVersions)));
            }
        }
    }
}
