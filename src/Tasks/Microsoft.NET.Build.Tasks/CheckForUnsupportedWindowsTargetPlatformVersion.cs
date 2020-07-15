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
        public ITaskItem[] KnownFrameworkReferences { get; set; } = Array.Empty<ITaskItem>();

        public string TargetPlatformVersion { get; set; }

        public CheckForUnsupportedWindowsTargetPlatformVersion()
        {
        }

        protected override void ExecuteCore()
        {
        }
    }

    internal struct KnownFrameworkReference
    {
        ITaskItem _item;
        public KnownFrameworkReference(ITaskItem item)
        {
            _item = item;
            TargetFramework = new NuGetFrameworkTemp(item.GetMetadata("TargetFramework"));
        }

        //  The name / itemspec of the FrameworkReference used in the project
        public string Name => _item.ItemSpec;

        //  The framework name to write to the runtimeconfig file (and the name of the folder under dotnet/shared)
        public string RuntimeFrameworkName => _item.GetMetadata(MetadataKeys.RuntimeFrameworkName);
        public string DefaultRuntimeFrameworkVersion => _item.GetMetadata("DefaultRuntimeFrameworkVersion");

        //  The ID of the targeting pack NuGet package to reference
        public string TargetingPackName => _item.GetMetadata("TargetingPackName");
        public string TargetingPackVersion => _item.GetMetadata("TargetingPackVersion");
        public string TargetingPackFormat => _item.GetMetadata("TargetingPackFormat");

        public string RuntimePackRuntimeIdentifiers => _item.GetMetadata(MetadataKeys.RuntimePackRuntimeIdentifiers);

        public bool IsWindowsOnly => _item.HasMetadataValue("IsWindowsOnly", "true");

        public bool RuntimePackAlwaysCopyLocal =>
            _item.HasMetadataValue(MetadataKeys.RuntimePackAlwaysCopyLocal, "true");

        public string Profile => _item.GetMetadata("Profile");

        public NuGetFrameworkTemp TargetFramework { get; }


        // TODO replace with the proper impl from nuget
        internal class NuGetFrameworkTemp
        {
            public NuGetFrameworkTemp(string targetFramework)
            {
                Match match = Regex.Match(targetFramework, "([^-]+)");
                var beforeDash = match.Value;

                var nuGetFramework = NuGetFramework.Parse(beforeDash);
                Framework = nuGetFramework.Framework;
                Version = nuGetFramework.Version;
                ShortFolderName = nuGetFramework.GetShortFolderName();

                if (beforeDash != targetFramework)
                {
                    var afterDash = targetFramework.Substring(match.Length + 1);
                    Match matchPlatform = Regex.Match(afterDash, "^[A-Za-z]+");
                    Platform = matchPlatform.Value;
                    PlatformVersion = afterDash.Substring(matchPlatform.Length);
                }
            }

            public string Framework { get; }
            public Version Version { get; }
            public string Platform { get; }

            public string PlatformVersion { get; }

            private string ShortFolderName { get; }

            public string GetShortFolderName()
            {
                return ShortFolderName;
            }
        }
    }
}
