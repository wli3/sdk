// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.Frameworks;
using NuGet.ProjectModel;

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

        public static void AddSupportedRuntimeToAppconfigFile(XDocument doc, string targetFramework)
        {
            XElement startupNode = doc.Root
                                      .Nodes()
                                      .OfType<XElement>()
                                      .FirstOrDefault(e => e.Name.LocalName == "startup");

            if (startupNode == null)
            {
                startupNode = new XElement("startup");
                doc.Root.Add(startupNode);
            }

            if (!startupNode.Nodes().OfType<XElement>().Any(e => e.Name.LocalName == "supportedRuntime"))
            {
                var supportedRuntime = new XElement(
                    "supportedRuntime",
                    new XAttribute("version", "v4.0"),
                    new XAttribute("sku", ".NETFramework,Version=v4.6.1"));

                startupNode.Add(supportedRuntime);
            }
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
                    // TODO wul loc
                    throw new BuildErrorException("The application configuration file must have root configuration element.");
                }
            }

            return document;
        }
    }
}
