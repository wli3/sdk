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
    public sealed class GenerateShims : TaskBase
    {
        private NuGetPackageResolver _packageResolver;

        /// <summary>
        /// Path to assets.json.
        /// </summary>
        public string ProjectAssetsFile { get; set; }

        /// <summary>
        /// The file name of Apphost asset.
        /// </summary>
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

        /// <summary>
        /// PackageId of the dotnet tool NuGet Package.
        /// </summary>
        [Required]
        public string PackageId { get; set; }

        /// <summary>
        /// Package Version of the dotnet tool NuGet Package.
        /// </summary>
        [Required]
        public string PackageVersion { get; set; }

        /// <summary>
        /// The command name of the dotnet tool.
        /// </summary>
        [Required]
        public string ToolCommandName { get; set; }

        /// <summary>
        /// The entry point of the dotnet tool which will be run by Apphost
        /// </summary>
        [Required]
        public string ToolEntryPoint { get; set; }

        /// <summary>
        /// The output Directroy path of generated shims.
        /// </summary>
        [Required]
        public string PackagedShimOutputDirectory { get; set; }

        /// <summary>
        /// The RuntimeIdentifiers that shims will be generated for.
        /// </summary>
        [Required]
        public ITaskItem[] PackageToolShimRuntimeIdentifiers { get; set; }

        /// <summary>
        /// Path of generated shims. metadata "ShimRuntimeIdentifier" is used to map back to PackageToolShimRuntimeIdentifiers.
        /// </summary>
        [Output]
        public ITaskItem[] EmbeddedApphostPaths { get; private set; }

        protected override void ExecuteCore()
        {
            NuGet.Frameworks.NuGetFramework targetFramework = NuGetUtils.ParseFrameworkName(TargetFrameworkMoniker);
            LockFile lockFile = new LockFileCache(this).GetLockFile(ProjectAssetsFile);
            _packageResolver = NuGetPackageResolver.CreateResolver(lockFile, ProjectPath);
            LockFileTarget compileTimeTarget = lockFile.GetTargetAndThrowIfNotFound(targetFramework, runtime: null);

            var embeddedApphostPaths = new List<ITaskItem>();
            foreach (string runtimeIdentifier in PackageToolShimRuntimeIdentifiers.Select(r => r.ItemSpec))
            {
                string resolvedApphostAssetPath = GetApphostAsset(targetFramework, lockFile, runtimeIdentifier);

                var PackagedShimOutputDirectoryAndRid = Path.Combine(PackagedShimOutputDirectory, runtimeIdentifier);
                string appHostDestinationFilePath =
                    Path.Combine(PackagedShimOutputDirectoryAndRid, $"{ToolCommandName}{Path.GetExtension(resolvedApphostAssetPath)}");
                string appBinaryFilePath = $".store/{PackageId.ToLowerInvariant()}/{PackageVersion}/{PackageId.ToLowerInvariant()}/{PackageVersion}/tools/{targetFramework.GetShortFolderName()}/any/{ToolEntryPoint}";

                Directory.CreateDirectory(PackagedShimOutputDirectoryAndRid);
                if (File.Exists(appHostDestinationFilePath))
                {
                    File.Delete(appHostDestinationFilePath);
                }

                EmbedAppNameInHostUtil.EmbedAppHost(
                    resolvedApphostAssetPath,
                    appHostDestinationFilePath,
                    appBinaryFilePath
                    );

                TaskItem item = new TaskItem(appHostDestinationFilePath);
                item.SetMetadata("ShimRuntimeIdentifier", runtimeIdentifier);
                embeddedApphostPaths.Add(item);
            }

            EmbeddedApphostPaths = embeddedApphostPaths.ToArray();
        }

        private string GetApphostAsset(NuGet.Frameworks.NuGetFramework targetFramework, LockFile lockFile, string runtimeIdentifier)
        {
            var apphostName = DotNetAppHostExecutableNameWithoutExtension;

            if (runtimeIdentifier.StartsWith("win"))
            {
                apphostName += ".exe";
            }
            LockFileTarget runtimeTarget = lockFile.GetTargetAndThrowIfNotFound(targetFramework, runtimeIdentifier);

            return FindApphostInRuntimeTarget(apphostName, runtimeTarget);
        }

        private string FindApphostInRuntimeTarget(string apphostName, LockFileTarget runtimeTarget)
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
