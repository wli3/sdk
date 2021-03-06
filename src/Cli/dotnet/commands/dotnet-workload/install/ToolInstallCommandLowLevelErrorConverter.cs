// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.DotNet.ShellShim;
using Microsoft.DotNet.WorkloadPackage;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal static class InstallWorkloadCommandLowLevelErrorConverter
    {
        public static IEnumerable<string> GetUserFacingMessages(Exception ex, PackageId packageId)
        {
            string[] userFacingMessages = null;
            if (ex is WorkloadPackageException)
            {
                userFacingMessages = new[]
                {
                    ex.Message,
                    string.Format(LocalizableStrings.WorkloadInstallationFailedWithRestoreGuidance, packageId),
                };
            }
            else if (ex is WorkloadConfigurationException)
            {
                userFacingMessages = new[]
                {
                    string.Format(
                        LocalizableStrings.InvalidWorkloadConfiguration,
                        ex.Message),
                    string.Format(LocalizableStrings.WorkloadInstallationFailedContactAuthor, packageId)
                };
            }
            else if (ex is ShellShimException)
            {
                userFacingMessages = new[]
                {
                    string.Format(
                        LocalizableStrings.FailedToCreateWorkloadShim,
                        packageId,
                        ex.Message),
                    string.Format(LocalizableStrings.WorkloadInstallationFailed, packageId)
                };
            }

            return userFacingMessages;
        }

        public static bool ShouldConvertToUserFacingError(Exception ex)
        {
            return ex is WorkloadPackageException
                   || ex is WorkloadConfigurationException
                   || ex is ShellShimException;
        }
    }
}
