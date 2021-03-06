// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.CommandLine.Parsing;
using System.IO;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.Extensions.EnvironmentAbstractions;
using NuGet.Versioning;

namespace Microsoft.DotNet.Workloads.Workload.Install
{
    internal class WorkloadInstallLocalInstaller
    {
        public string TargetFrameworkToInstall { get; private set; }

        private readonly IWorkloadPackageInstaller _workloadPackageInstaller;
        private readonly PackageId _packageId;
        private readonly string _packageVersion;
        private readonly string _configFilePath;
        private readonly string[] _sources;
        private readonly string _verbosity;

        public WorkloadInstallLocalInstaller(
            ParseResult parseResult,
            IWorkloadPackageInstaller workloadPackageInstaller = null)
        {
            _packageId = new PackageId(parseResult.ValueForArgument<string>(WorkloadInstallCommandParser.PackageIdArgument));
            _packageVersion = parseResult.ValueForOption<string>(WorkloadInstallCommandParser.VersionOption);
            _configFilePath = parseResult.ValueForOption<string>(WorkloadInstallCommandParser.ConfigOption);
            _sources = parseResult.ValueForOption<string[]>(WorkloadInstallCommandParser.AddSourceOption);
            _verbosity = Enum.GetName(parseResult.ValueForOption<VerbosityOptions>(WorkloadInstallCommandParser.VerbosityOption));

            if (workloadPackageInstaller == null)
            {
                (IWorkloadPackageStore,
                    IWorkloadPackageStoreQuery,
                    IWorkloadPackageInstaller installer) workloadPackageStoresAndInstaller
                        = WorkloadPackageFactory.CreateWorkloadPackageStoresAndInstaller(
                            additionalRestoreArguments: parseResult.OptionValuesToBeForwarded(WorkloadInstallCommandParser.GetCommand()));
                _workloadPackageInstaller = workloadPackageStoresAndInstaller.installer;
            }
            else
            {
                _workloadPackageInstaller = workloadPackageInstaller;
            }

            TargetFrameworkToInstall = BundledTargetFramework.GetTargetFrameworkMoniker();
        }

        public IWorkloadPackage Install(FilePath manifestFile)
        {
            if (!string.IsNullOrEmpty(_configFilePath) && !File.Exists(_configFilePath))
            {
                throw new GracefulException(
                    string.Format(
                        LocalizableStrings.NuGetConfigurationFileDoesNotExist,
                        Path.GetFullPath(_configFilePath)));
            }

            VersionRange versionRange = null;
            if (!string.IsNullOrEmpty(_packageVersion) && !VersionRange.TryParse(_packageVersion, out versionRange))
            {
                throw new GracefulException(
                    string.Format(
                        LocalizableStrings.InvalidNuGetVersionRange,
                        _packageVersion));
            }

            FilePath? configFile = null;
            if (!string.IsNullOrEmpty(_configFilePath))
            {
                configFile = new FilePath(_configFilePath);
            }

            try
            {
                IWorkloadPackage workloadDownloadedPackage =
                    _workloadPackageInstaller.InstallPackageToExternalManagedLocation(
                        new PackageLocation(
                            nugetConfig: configFile,
                            additionalFeeds: _sources,
                            rootConfigDirectory: manifestFile.GetDirectoryPath()),
                        _packageId,
                        versionRange,
                        TargetFrameworkToInstall,
                        verbosity: _verbosity);

                return workloadDownloadedPackage;
            }
            catch (Exception ex) when (InstallWorkloadCommandLowLevelErrorConverter.ShouldConvertToUserFacingError(ex))
            {
                throw new GracefulException(
                    messages: InstallWorkloadCommandLowLevelErrorConverter.GetUserFacingMessages(ex, _packageId),
                    verboseMessages: new[] {ex.ToString()},
                    isUserError: false);
            }
        }
    }
}
