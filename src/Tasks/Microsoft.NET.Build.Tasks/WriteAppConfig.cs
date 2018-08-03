// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using NuGet.Frameworks;
using NuGet.Versioning;

namespace Microsoft.NET.Build.Tasks
{
    public sealed class WriteAppConfig : TaskBase
    {
        /// <summary>
        /// Path to the app.config source file.
        /// </summary>
        public ITaskItem AppConfigFile { get; set; }

        [Required]
        public string TargetFrameworkIdentifier { get; set; }

        [Required]
        public string TargetFrameworkVersion { get; set; }

        public string TargetFrameworkProfile { get; set; }

        /// <summary>
        /// Path to an intermediate file where we can write the input app.config plus the generated startup supportedRuntime
        /// </summary>
        [Required]
        public ITaskItem OutputAppConfigFile { get; set; }

        protected override void ExecuteCore()
        {
            XDocument doc = LoadAppConfig(AppConfigFile);

            AddSupportedRuntimeToAppconfigFile(doc, TargetFrameworkIdentifier, TargetFrameworkVersion, TargetFrameworkProfile);

            if (File.Exists(OutputAppConfigFile.ItemSpec))
            {
                File.Delete(OutputAppConfigFile.ItemSpec);
            }

            var fileStream = new FileStream(
                OutputAppConfigFile.ItemSpec,
                FileMode.Create,
                FileAccess.Write,
                FileShare.Read);
            using (var stream = new StreamWriter(fileStream))
            {
                doc.Save(stream);
            }
        }

        public static void AddSupportedRuntimeToAppconfigFile(
            XDocument doc,
            string targetFrameworkIdentifier,
            string targetFrameworkVersion,
            string targetFrameworkProfile = null)
        {
            XElement startupNode = doc.Root
                                      .Nodes()
                                      .OfType<XElement>()
                                      .FirstOrDefault(e => e.Name.LocalName == "startup");

            string runtimeVersion = string.Empty;
            if (!HasExistingSupportedRuntime(startupNode))
            {
                if (TryGetSupportRuntimeNode(
                    targetFrameworkIdentifier,
                    targetFrameworkVersion,
                    targetFrameworkProfile,
                    runtimeVersion,
                    out XElement supportedRuntime))
                {
                    if (startupNode == null)
                    {
                        startupNode = new XElement("startup");
                        doc.Root.Add(startupNode);
                    }

                    startupNode.Add(supportedRuntime);
                }
            }
        }

        private static bool HasExistingSupportedRuntime(XElement startupNode)
        {
            return startupNode != null
                  && startupNode.Nodes().OfType<XElement>().Any(e => e.Name.LocalName == "supportedRuntime");
        }

        //https://github.com/dotnet/docs/blob/master/docs/framework/configure-apps/file-schema/startup/supportedruntime-element.md
        private static bool TryGetSupportRuntimeNode(
            string targetFrameworkIdentifier,
            string targetFrameworkVersion,
            string targetFrameworkProfile,
            string runtimeVersion,
            out XElement supportedRuntime)
        {
            supportedRuntime = null;

            if (targetFrameworkIdentifier == ".NETFramework"
                && NuGetVersion.TryParse(targetFrameworkVersion.TrimStart('v', 'V'), out NuGetVersion parsedVersion))
            {
                if (parsedVersion.Version.Major < 4)
                {
                    string supportedRuntimeVersion = null;

                    if (parsedVersion.Version.Major == 1 && parsedVersion.Version.Minor >= 0 && parsedVersion.Version.Minor > 1)
                    {
                        supportedRuntimeVersion = "v1.0.3705";
                    }
                    else if (parsedVersion.Version.Major == 1 && parsedVersion.Version.Minor >= 1)
                    {
                        supportedRuntimeVersion = "v1.1.4322";
                    }
                    else if (parsedVersion.Version.Major >= 2 && parsedVersion.Version.Major < 4)
                    {
                        supportedRuntimeVersion = "v2.0.50727";
                    }

                    if (supportedRuntimeVersion == null)
                    {
                        return false;
                    }

                    supportedRuntime =
                           new XElement(
                               "supportedRuntime",
                               new XAttribute("version", supportedRuntimeVersion));

                    return true;
                }
                else if (parsedVersion.Version.Major == 4)
                {
                    string profileInSku = targetFrameworkProfile != null ? $",Profile={targetFrameworkProfile}" : string.Empty;
                    supportedRuntime =
                        new XElement(
                            "supportedRuntime",
                            new XAttribute("version", "v4.0"),
                                new XAttribute("sku", $"{targetFrameworkIdentifier},Version={targetFrameworkVersion}{profileInSku}"));

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Load or create App.Config
        /// </summary>
        private XDocument LoadAppConfig(ITaskItem appConfigItem)
        {
            XDocument document;
            if (appConfigItem == null)
            {
                document =
                    new XDocument(
                        new XDeclaration("1.0", "utf-8", "true"),
                            new XElement("configuration"));
            }
            else
            {
                document = XDocument.Load(appConfigItem.ItemSpec);
                if (document.Root == null || document.Root.Name != "configuration")
                {
                    throw new BuildErrorException(Strings.AppConfigRequiresRootConfiguration);
                }
            }

            return document;
        }
    }
}
