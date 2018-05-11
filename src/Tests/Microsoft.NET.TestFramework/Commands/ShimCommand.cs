// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit.Abstractions;

namespace Microsoft.NET.TestFramework.Commands
{
    public class ShimCommand : TestCommand
    {
        private readonly string _commandName;

        public ShimCommand(ITestOutputHelper log, string commandName, params string[] args) : base(log)
        {
            _commandName = commandName;
            Arguments.AddRange(args);
        }

        protected override SdkCommandSpec CreateCommand(string[] args)
        {
            var sdkCommandSpec = new SdkCommandSpec()
            {
                FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? _commandName + ".exe" : _commandName,
                Arguments = args.ToList(),
                WorkingDirectory = WorkingDirectory
            };


            if (Environment.Is64BitProcess)
            {
                sdkCommandSpec.Environment.Add("DOTNET_ROOT", TestContext.Current.ToolsetUnderTest.DotNetHostPath);
            }
            else
            {
                sdkCommandSpec.Environment.Add("DOTNET_ROOT(x86)", TestContext.Current.ToolsetUnderTest.DotNetHostPath);
            }

            TestContext.Current.AddTestEnvironmentVariables(sdkCommandSpec);
            return sdkCommandSpec;
        }
    }
}
