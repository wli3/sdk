// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadManifest;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.Extensions.EnvironmentAbstractions;
using NuGet.Frameworks;

namespace Microsoft.DotNet.Workloads.Workload.Common
{
    internal static class WorkloadManifestFinderExtensions
    {
        public static (FilePath? filePath, string warningMessage) ExplicitManifestOrFindManifestContainPackageId(
            this IWorkloadManifestFinder workloadManifestFinder,
            string explicitManifestFile,
            PackageId packageId)
        {
            if (!string.IsNullOrWhiteSpace(explicitManifestFile))
            {
                return (new FilePath(explicitManifestFile), null);
            }

            IReadOnlyList<FilePath> manifestFilesContainPackageId;
            try
            {
                manifestFilesContainPackageId
                 = workloadManifestFinder.FindByPackageId(packageId);
            }
            catch (WorkloadManifestCannotBeFoundException e)
            {
                throw new GracefulException(new[]
                    {
                        e.Message,
                        LocalizableStrings.NoManifestGuide
                    },
                    verboseMessages: new[] { e.VerboseMessage },
                    isUserError: false);
            }

            if (manifestFilesContainPackageId.Any())
            {
                string warning = null;
                if (manifestFilesContainPackageId.Count > 1)
                {
                    warning =
                        string.Format(
                            LocalizableStrings.SamePackageIdInOtherManifestFile,
                            string.Join(
                                Environment.NewLine,
                                manifestFilesContainPackageId.Skip(1).Select(m => $"\t{m}")));
                }

                return (manifestFilesContainPackageId.First(), warning);
            }

            return (null, null);
        }
    }
}
