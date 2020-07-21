﻿using System;
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

        [Required]
        public string WinRTApisPackageName { get; set; }

        protected override void ExecuteCore()
        {
            if (!TargetPlatformIdentifier.Equals("Windows", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            IEnumerable<KnownFrameworkReference> winRtApisKnownFrameworkReferences =
                KnownFrameworkReferences
                    .Select(item => new KnownFrameworkReference(item))
                    .Where(i => i.Name.Equals(WinRTApisPackageName, StringComparison.OrdinalIgnoreCase));

            var knownFrameworkReferencesForTargetFramework =
                winRtApisKnownFrameworkReferences
                    .Where(kfr => kfr.KnownFrameworkReferenceAppliesToTargetFramework(
                        TargetFrameworkIdentifier,
                        TargetFrameworkVersion,
                        TargetPlatformVersion));

            if (!knownFrameworkReferencesForTargetFramework.Any())
            {
                var availableVersions = winRtApisKnownFrameworkReferences
                    .Select(item => item.TargetFramework.PlatformVersion);

                Log.LogError(string.Format(Strings.InvalidTargetPlatformVersion, TargetPlatformVersion,
                    TargetPlatformIdentifier, string.Join(" ", availableVersions)));
            }
        }
    }
}
