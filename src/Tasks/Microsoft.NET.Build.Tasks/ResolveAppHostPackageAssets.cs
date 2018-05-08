// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Microsoft.NET.Build.Tasks
{
    public sealed class ResolveAppHostPackageAssets : TaskBase
    {
        /// <summary>
        /// Path to assets.json.
        /// </summary>
        public string ProjectAssetsFile { get; set; }

        /// <summary>
        /// Path to assets.cache file.
        /// </summary>
        [Required]
        public string ProjectAssetsCacheFile { get; set; }

        /// <summary>
        /// Path to project file (.csproj|.vbproj|.fsproj)
        /// </summary>
        [Required]
        public string ProjectPath { get; set; }

        /// <summary>
        /// TFM to use for compile-time assets.
        /// </summary>
        [Required]
        public string TargetFrameworkMoniker { get; set; }

        [Required]
        public ITaskItem[] RuntimeIdentifier { get; set; }

        /// <summary>
        /// Do not write package assets cache to disk nor attempt to read previous cache from disk.
        /// </summary>
        public bool DisablePackageAssetsCache { get; set; }

        /// <summary>
        /// Do not generate transitive project references.
        /// </summary>
        public bool DisableTransitiveProjectReferences { get; set; }

        /// <summary>
        /// Do not add references to framework assemblies as specified by packages.
        /// </summary>
        public bool DisableFrameworkAssemblies { get; set; }

        /// <summary>
        /// Log messages from assets log to build error/warning/message.
        /// </summary>
        public bool EmitAssetsLogMessages { get; set; }

        /// <summary>
        /// Set ExternallyResolved=true metadata on reference items to indicate to MSBuild ResolveAssemblyReferences
        /// that these are resolved by an external system (in this case nuget) and therefore several steps can be
        /// skipped as an optimization.
        /// </summary>
        public bool MarkPackageReferencesAsExternallyResolved { get; set; }

        /// <summary>
        /// Project language ($(ProjectLanguage) in common targets -"VB" or "C#" or "F#" ).
        /// Impacts applicability of analyzer assets.
        /// </summary>
        public string ProjectLanguage { get; set; }

        /// <summary>
        /// Check that there is at least one package dependency in the RID graph that is not in the RID-agnostic graph.
        /// Used as a heuristic to detect invalid RIDs.
        /// </summary>
        public bool EnsureRuntimePackageDependencies { get; set; }

        /// <summary>
        /// Specifies whether to validate that the version of the implicit platform packages in the assets
        /// file matches the version specified by <see cref="ExpectedPlatformPackages"/>
        /// </summary>
        public bool VerifyMatchingImplicitPackageVersion { get; set; }

        /// <summary>
        /// Implicitly referenced platform packages.  If set, then an error will be generated if the
        /// version of the specified packages from the assets file does not match the expected versions.
        /// </summary>
        public ITaskItem[] ExpectedPlatformPackages { get; set; }

        [Output]
        public ITaskItem[] RuntimeIdentifierApphost { get; private set; }

        protected override void ExecuteCore()
        {
            var resultPackages = new List<ITaskItem>();

            foreach (ITaskItem rid in RuntimeIdentifier)
            {
                var apphostName = "apphost";
                var resolvePackageAssets = new ResolvePackageAssets
                {
                    ProjectAssetsFile = Path.GetFullPath(ProjectAssetsFile),
                    ProjectAssetsCacheFile = Path.GetFullPath(ProjectAssetsCacheFile),
                    ProjectPath = Path.GetFullPath(ProjectPath),
                    TargetFrameworkMoniker = TargetFrameworkMoniker,
                    RuntimeIdentifier = rid.ItemSpec,
                    DisablePackageAssetsCache = DisablePackageAssetsCache,
                    DisableTransitiveProjectReferences = DisableTransitiveProjectReferences,
                    DisableFrameworkAssemblies = DisableFrameworkAssemblies,
                    EmitAssetsLogMessages = EmitAssetsLogMessages,
                    MarkPackageReferencesAsExternallyResolved = MarkPackageReferencesAsExternallyResolved,
                    ProjectLanguage = ProjectLanguage,
                    EnsureRuntimePackageDependencies = EnsureRuntimePackageDependencies,
                    VerifyMatchingImplicitPackageVersion = VerifyMatchingImplicitPackageVersion,
                    ExpectedPlatformPackages = ExpectedPlatformPackages,
                    BuildEngine = BuildEngine
                };

                resolvePackageAssets.Execute();
                ITaskItem[] nativeLibraries = resolvePackageAssets.NativeLibraries;
                ITaskItem apphostItem = nativeLibraries.Where(t => t.GetMetadata("FileName") == apphostName).Single(); // TODO handling error

                TaskItem item = new TaskItem(rid);
                item.SetMetadata("AppHostFilePath", apphostItem.ItemSpec);

                resultPackages.Add(item);
            }
            RuntimeIdentifierApphost = resultPackages.ToArray();
        }

    }
}
