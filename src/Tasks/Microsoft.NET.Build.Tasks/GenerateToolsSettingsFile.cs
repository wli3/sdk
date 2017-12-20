﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Microsoft.NET.Build.Tasks
{
    public class GenerateToolsSettingsFile : TaskBase
    {
        [Required]
        public string EntryPointRelativePath { get; set; }

        [Required]
        public string CommandName { get; set; }

        [Output]
        public ITaskItem FileWritten
        {
            get; private set;
        }

        protected override void ExecuteCore()
        {

        }

        internal static XDocument GenerateDocument(string entryPointRelativePath, string commandName)
        {
            return new XDocument(
                new XDeclaration(version: "1.0", encoding: null, standalone: null),
                new XElement("DotNetCliTool",
                      new XElement("Commands",
                          new XElement("Command",
                          new XAttribute("Name", commandName),
                          new XAttribute("EntryPoint", entryPointRelativePath),
                          new XAttribute("Runner", "dotnet")))));
        }

    }
}
