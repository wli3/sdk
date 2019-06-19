﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

using RuntimeEnvironment = Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment;

namespace Reporting
{
    public class Reporter
    {
        private Run run;
        private Os os;
        private Build build;
        private List<Test> tests = new List<Test>();
        protected IEnvironment environment;

        private Reporter() { }

        public void AddTest(Test test)
        {
            if (tests.Any(t => t.Name.Equals(test.Name)))
                throw new Exception($"Duplicate test name, {test.Name}");
            tests.Add(test);
        }

        /// <summary>
        /// Get a Reporter. Relies on environment variables.
        /// </summary>
        /// <param name="environment">Optional environment variable provider</param>
        /// <returns>A Reporter instance or null if the environment is incorrect.</returns>
        public static Reporter CreateReporter(IEnvironment environment = null)
        {
            var ret = new Reporter();
            ret.environment = environment == null ? new EnvironmentProvider() : environment;
           
            ret.Init();
            return ret;
        }

        private void Init()
        {
            run = new Run
            {
                CorrelationId = environment.GetEnvironmentVariable("HELIX_CORRELATION_ID"),
                PerfRepoHash = "place holder", // sdk does not use perf repo
                Name = null, // no use for now.
                Queue = environment.GetEnvironmentVariable("HelixTargetQueues"),
            };
            run.Hidden = false;
            run.Configurations.Add("Configuration", environment.GetEnvironmentVariable("configuration"));
            run.Configurations.Add("TestFullMSBuild", environment.GetEnvironmentVariableAsBool("TestFullMSBuild", false).ToString());

            os = new Os()
            {
                Name = $"{RuntimeEnvironment.OperatingSystem} {RuntimeEnvironment.OperatingSystemVersion}",
                Architecture = RuntimeInformation.OSArchitecture.ToString(),
                Locale = CultureInfo.CurrentUICulture.ToString()
            };

            string gitHash = environment.GetEnvironmentVariable("GIT_COMMIT");
            build = new Build
            {
                Repo = "dotnet/sdk",
                Branch = environment.GetEnvironmentVariable("GIT_BRANCH"),
                Architecture = environment.GetEnvironmentVariable("architecture"),
                Locale = "en-us",
                GitHash = gitHash,
                BuildName = environment.GetEnvironmentVariable("BuildNumber"),
                TimeStamp = GetCommitTimestamp(gitHash, environment.GetEnvironmentVariable("HELIX_CORRELATION_PAYLOAD")),
            };
        }

        public static DateTime GetCommitTimestamp(string gitHash, string directoryUnderGit)
        {
            ProcessStartInfo gitInfo = new ProcessStartInfo();
            gitInfo.RedirectStandardError = true;
            gitInfo.RedirectStandardOutput = true;
            gitInfo.FileName = "git";
            gitInfo.Arguments = $"show -s --format=%cI {gitHash}"; // such as "fetch orign"
            gitInfo.WorkingDirectory = directoryUnderGit;

            using (Process process = Process.Start(gitInfo))
            {
                string stderr_str = process.StandardError.ReadToEnd();  // pick up STDERR
                string stdout_str = process.StandardOutput.ReadToEnd(); // pick up STDOUT

                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new PerformanceTestsResultGeneratorException(
                        $"Cannot get commit time stamp from git exitcode {process.ExitCode}, " +
                        $"StandardOutput {stdout_str}, " +
                        $"StandardError {stderr_str}.");
                }

                return DateTime.Parse(stdout_str.Trim());
            }
        }

        public string GetJson()
        {
            var jsonobj = new
            {
                build,
                os,
                run,
                tests
            };
            var settings = new JsonSerializerSettings();
            var resolver = new DefaultContractResolver();
            resolver.NamingStrategy = new CamelCaseNamingStrategy() { ProcessDictionaryKeys = false };
            settings.ContractResolver = resolver;
            return JsonConvert.SerializeObject(jsonobj, Formatting.Indented, settings);
        }
    }
}
