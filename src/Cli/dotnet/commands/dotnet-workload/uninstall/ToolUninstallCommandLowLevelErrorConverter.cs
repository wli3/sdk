// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.DotNet.ShellShim;
using Microsoft.DotNet.WorkloadPackage;

namespace Microsoft.DotNet.Workloads.Workload.Uninstall
{
    internal static class WorkloadUninstallCommandLowLevelErrorConverter
    {
        public static IEnumerable<string> GetUserFacingMessages(Exception ex, PackageId packageId)
        {
            string[] userFacingMessages = null;
            if (ex is WorkloadPackageException)
            {
                userFacingMessages = new[]
                {
                    String.Format(
                        CommonLocalizableStrings.FailedToUninstallWorkloadPackage,
                        packageId,
                        ex.Message),
                };
            }
            else if (ex is WorkloadConfigurationException || ex is ShellShimException)
            {
                userFacingMessages = new[]
                {
                    string.Format(
                        LocalizableStrings.FailedToUninstallWorkload,
                        packageId,
                        ex.Message)
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
