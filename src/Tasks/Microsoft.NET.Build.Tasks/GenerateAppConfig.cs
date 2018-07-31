// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using NuGet.Frameworks;

namespace Microsoft.NET.Build.Tasks
{
    public sealed class GenerateAppConfig : TaskBase
    {
        /// <summary>
        /// Path to the app.config source file.
        /// </summary>
        public ITaskItem AppConfigFile { get; set; }

        /// <summary>
        /// Name of the output application config file: $(TargetFileName).config
        /// </summary>
        public string TargetName { get; set; }

        public string TargetFramework { get; set; }

        /// <summary>
        /// Path to an intermediate file where we can write the input app.config plus the generated startup supportedRuntime
        /// </summary>
        [Output]
        public ITaskItem OutputAppConfigFile { get; set; }

        protected override void ExecuteCore()
        {
            XDocument doc = LoadAppConfig(AppConfigFile);

            AddSupportedRuntimeToAppconfigFile(doc, TargetFramework);

            if (File.Exists(OutputAppConfigFile.ItemSpec))
            {
                File.Delete(OutputAppConfigFile.ItemSpec);
            }

            if (AppConfigFile != null)
            {
                AppConfigFile.CopyMetadataTo(OutputAppConfigFile);
            }
            else
            {
                OutputAppConfigFile.SetMetadata(MetadataKeys.TargetPath, TargetName);
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

        public static void AddSupportedRuntimeToAppconfigFile(XDocument doc, string targetframework)
        {
            XElement startupNode = doc.Root
                                      .Nodes()
                                      .OfType<XElement>()
                                      .FirstOrDefault(e => e.Name.LocalName == "startup");

            string runtimeVersion = string.Empty;
            if (!HasExistingSupportedRuntime(startupNode))
            {
                if (TryGetSupportRuntimeNode(
                    targetframework,
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
        private static bool TryGetSupportRuntimeNode(string targetframework, string runtimeVersion, out XElement supportedRuntime)
        {
            supportedRuntime = null;
            var targetFrameworkParsed = NuGetFramework.Parse(targetframework);
            if (targetFrameworkParsed.Framework == ".NETFramework")
            {
                if (targetFrameworkParsed.Version.Major < 4)
                {
                    if (_targetFrameworkBelow40RuntimeVersionMap.TryGetValue(targetFrameworkParsed.GetShortFolderName(), out string value))
                    {
                        runtimeVersion = value;
                        supportedRuntime = new XElement(
                           "supportedRuntime",
                           new XAttribute("version", value));

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (targetFrameworkParsed.Version.Major == 4)
                {
                    supportedRuntime = new XElement(
                           "supportedRuntime",
                           new XAttribute("version", "v4.0"),
                           new XAttribute("sku", targetFrameworkParsed.DotNetFrameworkName));
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
                document = new XDocument(
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

        private static readonly Dictionary<string, string> _targetFrameworkBelow40RuntimeVersionMap = new Dictionary<string, string>()
        {
            ["net11"] = "v1.1.4322",
            ["net20"] = "v2.0.50727",
            ["net35"] = "v2.0.50727",
        };
    }
}
