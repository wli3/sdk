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
        /// Path to an intermediate file where we can write the input app.config plus the generated startup supportedRuntime
        /// </summary>
        [Output]
        public ITaskItem OutputAppConfigFile { get; set; }

        protected override void ExecuteCore()
        {
            var doc = LoadAppConfig(AppConfigFile);

            XElement runtimeNode = doc.Root
                                      .Nodes()
                                      .OfType<XElement>()
                                      .FirstOrDefault(e => e.Name.LocalName == "runtime");

            if (runtimeNode == null)
            {
                runtimeNode = new XElement("runtime");
                doc.Root.Add(runtimeNode);
            }

            if (File.Exists(OutputAppConfigFile.ItemSpec))
            {
                File.Delete(OutputAppConfigFile.ItemSpec);
            }

            if (AppConfigFile != null)
            {
                AppConfigFile.CopyMetadataTo(OutputAppConfigFile);
            }

            var fileStream = new FileStream(OutputAppConfigFile.ItemSpec, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan);
            using (var stream = new StreamWriter(fileStream))
            {
                doc.Save(stream);
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
                    // TODO loc
                    throw new BuildErrorException("The application configuration file must have root configuration element.");
                }
            }

            return document;
        }
    }
}
