// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.MSBuildSdkResolver;
using NuGet.Common;
using NuGet.Protocol;

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

        public IReadOnlyCollection<IOptionalSdkTemplatePackageInfo> GetDotnetSdkTemplatePackages(string sdkVersion)
        {
            if (_dotnetSdkTemplatesLocation == null)
            {
                return Array.Empty<IOptionalSdkTemplatePackageInfo>();
            }

            IEnumerable<LocalPackageInfo> packages = LocalFolderUtility
                            .GetPackagesV2(_dotnetSdkTemplatesLocation.FullName, new NullLogger());

            if (packages == null)
            {
                return Array.Empty<IOptionalSdkTemplatePackageInfo>();
            }
            else
            {
                return packages
                    .Select(l => new OptionalSdkTemplatePackageInfo(l)).ToArray();
            }
        }

        public bool TryGetDotnetSdkVersionUsedInVs(Version msbuildVersion, out string sdkVersion)
        {
            var resolver = new NETCoreSdkResolver();
            string dotnetExeDir = resolver.GetDotnetExeDirectory();
            var resolverResult = resolver.ResolveNETCoreSdkDirectory(null, msbuildVersion, true, dotnetExeDir);

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
            public OptionalSdkTemplatePackageInfo(LocalPackageInfo localPackageInfo)
            {
                TemplatePackageId = localPackageInfo.Identity.Id;
                TemplateVersion = localPackageInfo.Identity.Version.ToNormalizedString();
                Path = localPackageInfo.Path;
            }

            public string TemplatePackageId { get; }

            public string TemplateVersion { get; }

            public string Path { get; }
        }
    }
}
