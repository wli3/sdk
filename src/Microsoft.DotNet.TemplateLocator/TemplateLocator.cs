// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.MSBuildSdkResolver;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Versioning;
using Microsoft.NET.Sdk.WorkloadResolver;

namespace Microsoft.DotNet.TemplateLocator
{
    public sealed class TemplateLocator
    {
        private DirectoryInfo _dotnetSdkTemplatesLocation;

        public TemplateLocator()
        {
            string mockTemplateLocation = Environment.GetEnvironmentVariable("MOCKDOTNETSDKTEMPLATESLOCATION");
            if (!string.IsNullOrWhiteSpace(mockTemplateLocation))
            {
                _dotnetSdkTemplatesLocation = new DirectoryInfo(mockTemplateLocation);
            }
        }

        public IReadOnlyCollection<IOptionalSdkTemplatePackageInfo> GetDotnetSdkTemplatePackages(string sdkVersion,
            string dotnetRootPath)
        {
            var directory = new DirectoryInfo(dotnetRootPath);

            var sdkVersionParsed = NuGetVersion.Parse(sdkVersion);
            var bondDirectory = directory.GetDirectories().Where(d =>
            {
                var directoryVersion = NuGetVersion.Parse(d.Name.Split('-')[0]);
                return directoryVersion.Major == sdkVersionParsed.Major &&
                       directoryVersion.Minor == sdkVersionParsed.Minor &&
                       (directoryVersion.Patch / 100) == (sdkVersionParsed.Patch / 100);
            }).OrderByDescending(d => NuGetVersion.Parse(d.Name.Split('-')[0])).First();

            var manifestFolder = bondDirectory.GetDirectories().Single().FullName;
            var manifestContent = WorkloadManifest.LoadFromFolder(manifestFolder);
            var result = new List<IOptionalSdkTemplatePackageInfo>();
            foreach (var pack in manifestContent.SdkPackDetail)
            {
                if (pack.Value.kind.Equals("Template", StringComparison.OrdinalIgnoreCase))
                {
                    var optionalSdkTemplatePackageInfo = new OptionalSdkTemplatePackageInfo(pack.Key, pack.Value.version,
                        Path.Combine(dotnetRootPath, "template-packs", pack.Key.ToLower() + "." + pack.Value.version.ToLower() + ".nupkg"));
                    result.Add(optionalSdkTemplatePackageInfo);
                }
                
            }

            return result;
        }

        public bool TryGetDotnetSdkVersionUsedInVs(string vsVersion, out string sdkVersion)
        {
            var resolver = new NETCoreSdkResolver();
            string dotnetExeDir = resolver.GetDotnetExeDirectory();

            if (!Version.TryParse(vsVersion, out var parsedVsVersion))
            {
                throw new ArgumentException(vsVersion + " is not a valid version");
            }

            var msbuildMajorMinorVersion = new Version(parsedVsVersion.Major, parsedVsVersion.Minor, 0);

            var resolverResult =
                resolver.ResolveNETCoreSdkDirectory(null, msbuildMajorMinorVersion, true, dotnetExeDir);

            if (resolverResult.ResolvedSdkDirectory == null)
            {
                sdkVersion = null;
                return false;
            }
            else
            {
                sdkVersion = new DirectoryInfo(resolverResult.ResolvedSdkDirectory).Name;
                return true;
            }
        }

        private class OptionalSdkTemplatePackageInfo : IOptionalSdkTemplatePackageInfo
        {
            public OptionalSdkTemplatePackageInfo(string templatePackageId, string templateVersion, string path)
            {
                TemplatePackageId = templatePackageId ?? throw new ArgumentNullException(nameof(templatePackageId));
                TemplateVersion = templateVersion ?? throw new ArgumentNullException(nameof(templateVersion));
                Path = path ?? throw new ArgumentNullException(nameof(path));
            }

            public string TemplatePackageId { get; }

            public string TemplateVersion { get; }

            public string Path { get; }
        }
    }
}
