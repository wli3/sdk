// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using NuGet.Common;
using NuGet.ProjectModel;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Xunit;
using static Microsoft.NET.Build.Tasks.UnitTests.LockFileSnippets;

namespace Microsoft.NET.Build.Tasks.UnitTests
{
    public class GivenAGenerateToolsSettingsFile
    {
        [Fact]
        public void It_can_put_command_name_in_correct_place_of_the_file()
        {

            var result = GenerateToolsSettingsFile.GenerateDocument("tool.dll", "mytool");
            var serializer = new XmlSerializer(typeof(DotNetCliTool));
            DotNetCliTool dotNetCliTool = null;

            using (TextReader sr = new StringReader(result.ToString()))
            {
                dotNetCliTool = (DotNetCliTool)serializer.Deserialize(sr);

            }

            dotNetCliTool.Commands.Single().Name.Should().Be("mytool");
        }

        [XmlRoot(Namespace = "", IsNullable = false)]
        public class DotNetCliTool
        {
            [XmlArrayItem("Command", IsNullable = false)]
            public DotNetCliToolCommand[] Commands { get; set; }
        }

        [Serializable]
        [XmlType(AnonymousType = true)]
        public class DotNetCliToolCommand
        {
            [XmlAttribute]
            public string Name { get; set; }

            [XmlAttribute]
            public string EntryPoint { get; set; }

            [XmlAttribute]
            public string Runner { get; set; }
        }
    }
}
