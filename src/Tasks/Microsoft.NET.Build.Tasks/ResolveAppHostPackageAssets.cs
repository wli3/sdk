﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.ProjectModel;

namespace Microsoft.NET.Build.Tasks
{
    public sealed class ResolveAppHostPackageAssets : TaskBase
    {
        private NuGetPackageResolver _packageResolver;

        /// <summary>
        /// Path to assets.json.
        /// </summary>
        public string ProjectAssetsFile { get; set; }

        [Required]
        public string DotNetAppHostExecutableNameWithoutExtension { get; set; }

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
        public string PackagedShimOutputDirectory { get; set; }

        [Required]
        public ITaskItem[] PackageToolShimRuntimeIdentifiers { get; set; }

        [Output]
        public ITaskItem[] RuntimeIdentifierApphost { get; private set; }

        protected override void ExecuteCore()
        {
            var resultPackages = new List<ITaskItem>();
            NuGet.Frameworks.NuGetFramework targetFramework = NuGetUtils.ParseFrameworkName(TargetFrameworkMoniker);
            LockFile lockFile = new LockFileCache(this).GetLockFile(ProjectAssetsFile);
            _packageResolver = NuGetPackageResolver.CreateResolver(lockFile, ProjectPath);
            LockFileTarget compileTimeTarget = lockFile.GetTargetAndThrowIfNotFound(targetFramework, runtime: null);

            foreach (string rid in PackageToolShimRuntimeIdentifiers.Select(r => r.ItemSpec))
            {
                var apphostName = DotNetAppHostExecutableNameWithoutExtension;

                if (rid.StartsWith("win"))
                {
                    apphostName += ".exe";
                }
                LockFileTarget runtimeTarget = lockFile.GetTargetAndThrowIfNotFound(targetFramework, rid);
                TaskItem item = new TaskItem(rid);
                string resolvedPackageAssetPath = FindApphost(apphostName, runtimeTarget);
                item.SetMetadata("AppHostFilePath", resolvedPackageAssetPath);

                resultPackages.Add(item);
                var PackagedShimOutputDirectoryAndRid = Path.Combine(PackagedShimOutputDirectory, rid);
                Directory.CreateDirectory(PackagedShimOutputDirectoryAndRid);
                File.Copy(resolvedPackageAssetPath,
                    Path.Combine(PackagedShimOutputDirectoryAndRid, Path.GetFileName(resolvedPackageAssetPath)),
                    overwrite: true);
            }

            RuntimeIdentifierApphost = resultPackages.ToArray();
        }

        private string FindApphost(string apphostName, LockFileTarget runtimeTarget)
        {
            foreach (LockFileTargetLibrary library in runtimeTarget.Libraries)
            {
                if (!library.IsPackage())
                {
                    continue;
                }

                foreach (LockFileItem asset in library.NativeLibraries)
                {
                    if (asset.IsPlaceholderFile())
                    {
                        continue;
                    }

                    string resolvedPackageAssetPath = ResolvePackageAssetPath(library, asset.Path);
                    if (Path.GetFileName(resolvedPackageAssetPath) == apphostName)
                    {
                        return resolvedPackageAssetPath;
                    }
                }
            }

            throw new BuildErrorException($"Cannot find apphost for {runtimeTarget.RuntimeIdentifier}"); // TODO no check in loc wul
        }

        private string ResolvePackageAssetPath(LockFileTargetLibrary package, string relativePath)
        {
            string packagePath = _packageResolver.GetPackageDirectory(package.Name, package.Version);
            return Path.Combine(packagePath, NormalizeRelativePath(relativePath));
        }

        private static string NormalizeRelativePath(string relativePath)
                => relativePath.Replace('/', Path.DirectorySeparatorChar);

    }
}
