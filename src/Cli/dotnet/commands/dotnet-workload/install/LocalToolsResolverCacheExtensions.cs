// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadPackage;
using NuGet.Frameworks;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal static class LocalWorkloadsResolverCacheExtensions
    {
        public static void SaveWorkloadPackage(
            this ILocalWorkloadsResolverCache localWorkloadsResolverCache,
            IWorkloadPackage workloadDownloadedPackage,
            string targetFrameworkToInstall)
        {
            if (localWorkloadsResolverCache == null)
            {
                throw new ArgumentNullException(nameof(localWorkloadsResolverCache));
            }

            if (workloadDownloadedPackage == null)
            {
                throw new ArgumentNullException(nameof(workloadDownloadedPackage));
            }

            if (string.IsNullOrWhiteSpace(targetFrameworkToInstall))
            {
                throw new ArgumentException("targetFrameworkToInstall cannot be null or whitespace",
                    nameof(targetFrameworkToInstall));
            }

            foreach (var restoredCommand in workloadDownloadedPackage.Commands)
            {
                localWorkloadsResolverCache.Save(
                    new Dictionary<RestoredCommandIdentifier, RestoredCommand>
                    {
                        [new RestoredCommandIdentifier(
                                workloadDownloadedPackage.Id,
                                workloadDownloadedPackage.Version,
                                NuGetFramework.Parse(targetFrameworkToInstall),
                                Constants.AnyRid,
                                restoredCommand.Name)] =
                            restoredCommand
                    });
            }
        }
    }
}
