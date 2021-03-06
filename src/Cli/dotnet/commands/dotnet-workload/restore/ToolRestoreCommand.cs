// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.WorkloadManifest;
using Microsoft.DotNet.WorkloadPackage;
using Microsoft.Extensions.EnvironmentAbstractions;
using NuGet.Frameworks;
using NuGet.Versioning;

namespace Microsoft.DotNet.Workloads.Workload.Restore
{
    internal class WorkloadRestoreCommand : CommandBase
    {
        private readonly string _configFilePath;
        private readonly IReporter _errorReporter;
        private readonly ILocalWorkloadsResolverCache _localWorkloadsResolverCache;
        private readonly IWorkloadManifestFinder _workloadManifestFinder;
        private readonly IFileSystem _fileSystem;
        private readonly IReporter _reporter;
        private readonly string[] _sources;
        private readonly IWorkloadPackageInstaller _workloadPackageInstaller;
        private readonly string _verbosity;

        public WorkloadRestoreCommand(
            ParseResult result,
            IWorkloadPackageInstaller workloadPackageInstaller = null,
            IWorkloadManifestFinder workloadManifestFinder = null,
            ILocalWorkloadsResolverCache localWorkloadsResolverCache = null,
            IFileSystem fileSystem = null,
            IReporter reporter = null)
            : base(result)
        {
            if (workloadPackageInstaller == null)
            {
                (IWorkloadPackageStore,
                    IWorkloadPackageStoreQuery,
                    IWorkloadPackageInstaller installer) workloadPackageStoresAndInstaller
                        = WorkloadPackageFactory.CreateWorkloadPackageStoresAndInstaller(
                            additionalRestoreArguments: result.OptionValuesToBeForwarded(WorkloadRestoreCommandParser.GetCommand()));
                _workloadPackageInstaller = workloadPackageStoresAndInstaller.installer;
            }
            else
            {
                _workloadPackageInstaller = workloadPackageInstaller;
            }

            _workloadManifestFinder
                = workloadManifestFinder
                  ?? new WorkloadManifestFinder(new DirectoryPath(Directory.GetCurrentDirectory()));

            _localWorkloadsResolverCache = localWorkloadsResolverCache ?? new LocalWorkloadsResolverCache();
            _fileSystem = fileSystem ?? new FileSystemWrapper();

            _reporter = reporter ?? Reporter.Output;
            _errorReporter = reporter ?? Reporter.Error;

            _configFilePath = result.ValueForOption<string>(WorkloadRestoreCommandParser.ConfigOption);
            _sources = result.ValueForOption<string[]>(WorkloadRestoreCommandParser.AddSourceOption);
            _verbosity = Enum.GetName(result.ValueForOption<VerbosityOptions>(WorkloadRestoreCommandParser.VerbosityOption));
        }

        public override int Execute()
        {
            FilePath? customManifestFileLocation = GetCustomManifestFileLocation();

            FilePath? configFile = null;
            if (!string.IsNullOrEmpty(_configFilePath))
            {
                configFile = new FilePath(_configFilePath);
            }

            IReadOnlyCollection<WorkloadManifestPackage> packagesFromManifest;
            try
            {
                packagesFromManifest = _workloadManifestFinder.Find(customManifestFileLocation);
            }
            catch (WorkloadManifestCannotBeFoundException e)
            {
                if (CommandContext.IsVerbose())
                {
                    _reporter.WriteLine(string.Join(Environment.NewLine, e.VerboseMessage).Yellow());
                }

                _reporter.WriteLine(e.Message.Yellow());
                _reporter.WriteLine(LocalizableStrings.NoWorkloadsWereRestored.Yellow());
                return 0;
            }

            WorkloadRestoreResult[] workloadRestoreResults =
                packagesFromManifest
                    .AsParallel()
                    .Select(package => InstallPackages(package, configFile))
                    .ToArray();

            Dictionary<RestoredCommandIdentifier, RestoredCommand> downloaded =
                workloadRestoreResults.SelectMany(result => result.SaveToCache)
                    .ToDictionary(pair => pair.Item1, pair => pair.Item2);

            EnsureNoCommandNameCollision(downloaded);

            _localWorkloadsResolverCache.Save(downloaded);

            return PrintConclusionAndReturn(workloadRestoreResults);
        }

        private WorkloadRestoreResult InstallPackages(
            WorkloadManifestPackage package,
            FilePath? configFile)
        {
            string targetFramework = BundledTargetFramework.GetTargetFrameworkMoniker();

            if (PackageHasBeenRestored(package, targetFramework))
            {
                return WorkloadRestoreResult.Success(
                    saveToCache: Array.Empty<(RestoredCommandIdentifier, RestoredCommand)>(),
                    message: string.Format(
                        LocalizableStrings.RestoreSuccessful, package.PackageId,
                        package.Version.ToNormalizedString(), string.Join(", ", package.CommandNames)));
            }

            try
            {
                IWorkloadPackage workloadPackage =
                    _workloadPackageInstaller.InstallPackageToExternalManagedLocation(
                        new PackageLocation(
                            nugetConfig: configFile,
                            additionalFeeds: _sources,
                            rootConfigDirectory: package.FirstEffectDirectory),
                        package.PackageId, ToVersionRangeWithOnlyOneVersion(package.Version), targetFramework,
                        verbosity: _verbosity);

                if (!ManifestCommandMatchesActualInPackage(package.CommandNames, workloadPackage.Commands))
                {
                    return WorkloadRestoreResult.Failure(
                        string.Format(LocalizableStrings.CommandsMismatch,
                            JoinBySpaceWithQuote(package.CommandNames.Select(c => c.Value.ToString())),
                            package.PackageId,
                            JoinBySpaceWithQuote(workloadPackage.Commands.Select(c => c.Name.ToString()))));
                }

                return WorkloadRestoreResult.Success(
                    saveToCache: workloadPackage.Commands.Select(command => (
                        new RestoredCommandIdentifier(
                            workloadPackage.Id,
                            workloadPackage.Version,
                            NuGetFramework.Parse(targetFramework),
                            Constants.AnyRid,
                            command.Name),
                        command)).ToArray(),
                    message: string.Format(
                        LocalizableStrings.RestoreSuccessful,
                        package.PackageId,
                        package.Version.ToNormalizedString(),
                        string.Join(" ", package.CommandNames)));
            }
            catch (WorkloadPackageException e)
            {
                return WorkloadRestoreResult.Failure(package.PackageId, e);
            }
        }

        private int PrintConclusionAndReturn(WorkloadRestoreResult[] workloadRestoreResults)
        {
            if (workloadRestoreResults.Any(r => !r.IsSuccess))
            {
                _reporter.WriteLine();
                _errorReporter.WriteLine(string.Join(
                    Environment.NewLine,
                    workloadRestoreResults.Where(r => !r.IsSuccess).Select(r => r.Message)).Red());

                var successMessage = workloadRestoreResults.Where(r => r.IsSuccess).Select(r => r.Message);
                if (successMessage.Any())
                {
                    _reporter.WriteLine();
                    _reporter.WriteLine(string.Join(Environment.NewLine, successMessage));

                }

                _errorReporter.WriteLine(Environment.NewLine +
                                         (workloadRestoreResults.Any(r => r.IsSuccess)
                                             ? LocalizableStrings.RestorePartiallyFailed
                                             : LocalizableStrings.RestoreFailed).Red());

                return 1;
            }
            else
            {
                _reporter.WriteLine(string.Join(Environment.NewLine,
                    workloadRestoreResults.Where(r => r.IsSuccess).Select(r => r.Message)));
                _reporter.WriteLine();
                _reporter.WriteLine(LocalizableStrings.LocalWorkloadsRestoreWasSuccessful.Green());

                return 0;
            }
        }

        private static bool ManifestCommandMatchesActualInPackage(
            WorkloadCommandName[] commandsFromManifest,
            IReadOnlyList<RestoredCommand> workloadPackageCommands)
        {
            WorkloadCommandName[] commandsFromPackage = workloadPackageCommands.Select(t => t.Name).ToArray();
            foreach (var command in commandsFromManifest)
            {
                if (!commandsFromPackage.Contains(command))
                {
                    return false;
                }
            }

            foreach (var command in commandsFromPackage)
            {
                if (!commandsFromManifest.Contains(command))
                {
                    return false;
                }
            }

            return true;
        }

        private bool PackageHasBeenRestored(
            WorkloadManifestPackage package,
            string targetFramework)
        {
            var sampleRestoredCommandIdentifierOfThePackage = new RestoredCommandIdentifier(
                package.PackageId,
                package.Version,
                NuGetFramework.Parse(targetFramework),
                Constants.AnyRid,
                package.CommandNames.First());

            return _localWorkloadsResolverCache.TryLoad(
                       sampleRestoredCommandIdentifierOfThePackage,
                       out var restoredCommand)
                   && _fileSystem.File.Exists(restoredCommand.Executable.Value);
        }

        private FilePath? GetCustomManifestFileLocation()
        {
            string customFile = _parseResult.ValueForOption<string>(WorkloadRestoreCommandParser.WorkloadManifestOption);
            FilePath? customManifestFileLocation;
            if (!string.IsNullOrEmpty(customFile))
            {
                customManifestFileLocation = new FilePath(customFile);
            }
            else
            {
                customManifestFileLocation = null;
            }

            return customManifestFileLocation;
        }

        private void EnsureNoCommandNameCollision(Dictionary<RestoredCommandIdentifier, RestoredCommand> dictionary)
        {
            string[] errors = dictionary
                .Select(pair => (PackageId: pair.Key.PackageId, CommandName: pair.Key.CommandName))
                .GroupBy(packageIdAndCommandName => packageIdAndCommandName.CommandName)
                .Where(grouped => grouped.Count() > 1)
                .Select(nonUniquePackageIdAndCommandNames =>
                    string.Format(LocalizableStrings.PackagesCommandNameCollisionConclusion,
                        string.Join(Environment.NewLine,
                            nonUniquePackageIdAndCommandNames.Select(
                                p => "\t" + string.Format(
                                    LocalizableStrings.PackagesCommandNameCollisionForOnePackage,
                                    p.CommandName.Value,
                                    p.PackageId.ToString())))))
                .ToArray();

            if (errors.Any())
            {
                throw new WorkloadPackageException(string.Join(Environment.NewLine, errors));
            }
        }

        private static string JoinBySpaceWithQuote(IEnumerable<object> objects)
        {
            return string.Join(" ", objects.Select(o => $"\"{o.ToString()}\""));
        }

        private static VersionRange ToVersionRangeWithOnlyOneVersion(NuGetVersion version)
        {
            return new VersionRange(
                version,
                includeMinVersion: true,
                maxVersion: version,
                includeMaxVersion: true);
        }

        private struct WorkloadRestoreResult
        {
            public (RestoredCommandIdentifier, RestoredCommand)[] SaveToCache { get; }
            public bool IsSuccess { get; }
            public string Message { get; }

            private WorkloadRestoreResult(
                (RestoredCommandIdentifier, RestoredCommand)[] saveToCache,
                bool isSuccess, string message)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentException("message", nameof(message));
                }

                SaveToCache = saveToCache ?? Array.Empty<(RestoredCommandIdentifier, RestoredCommand)>();
                IsSuccess = isSuccess;
                Message = message;
            }

            public static WorkloadRestoreResult Success(
                (RestoredCommandIdentifier, RestoredCommand)[] saveToCache,
                string message)
            {
                return new WorkloadRestoreResult(saveToCache, true, message);
            }

            public static WorkloadRestoreResult Failure(string message)
            {
                return new WorkloadRestoreResult(null, false, message);
            }

            public static WorkloadRestoreResult Failure(
                PackageId packageId,
                WorkloadPackageException workloadPackageException)
            {
                return new WorkloadRestoreResult(null, false,
                    string.Format(LocalizableStrings.PackageFailedToRestore,
                        packageId.ToString(), workloadPackageException.ToString()));
            }
        }
    }
}
