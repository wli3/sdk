// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Build.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests
{
    public class GivenAGenerateDepsFile
    {
        public GivenAGenerateDepsFile()
        {
        }

        [Fact]
        public void ItCanGenerateWithoutAssetFile()
        {
            string testTempDir = Path.Combine(Path.GetTempPath(), "dotnetSdkTests");
            Directory.CreateDirectory(testTempDir);
            var depsFilePath = Path.Combine(testTempDir, nameof(ItCanGenerateWithoutAssetFile) + "runtimeconfig.json");
            if (File.Exists(depsFilePath))
            {
                File.Delete(depsFilePath);
            }


            var task = new TestableGenerateDepsFile()
            {
                BuildEngine = new MockBuildEngine(),
                ProjectPath = @"C:\work\NETCoreCppCliTest\NETCoreCppCliTest\NETCoreCppCliTest.vcxproj",
                DepsFilePath = depsFilePath,
                TargetFramework = ".NETCoreApp,Version=v3.1",
                AssemblyName = "NETCoreCppCliTest",
                AssemblyExtension = ".dll",
                AssemblyVersion = "1.0.0",
                IncludeMainProject = true,
                RuntimeFrameworks = new MockTaskItem[]
                {
                    new MockTaskItem(
                        itemSpec: "Microsoft.NETCore.App",
                        metadata: new Dictionary<string, string>
                        {
                            {"FrameworkName", "Microsoft.NETCore.App"},
                            {"Version", "Version = 3.1.0-preview3.19553.2"},
                        }
                    )
                },
                UserRuntimeAssemblies =
                    new string[]
                    {
                        @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug\NETCoreCppCliTestB.dll",
                        @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug\NETCoreCppCliTestC.dll"
                    },
                CompileReferences = Array.Empty<MockTaskItem>(),
                ResolvedNuGetFiles = Array.Empty<MockTaskItem>(),
                ResolvedRuntimeTargetsFiles = Array.Empty<MockTaskItem>(),
                ReferencePaths = new MockTaskItem[]
                {
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.CSharp.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.CSharp.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.Core.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "10.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.VisualBasic.Core, Version=10.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.Core.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "10.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.Win32.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.Win32.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.Win32.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\mscorlib.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\mscorlib.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec: @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug\NETCoreCppCliTestB.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"BuildReference", "true"},
                            {"CopyLocal", "true"},
                            {"FusionName", "NETCoreCppCliTestB, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"},
                            {"ImageRuntime", "v4.0.30319"},
                            {"IncludeRuntimeDependency", "true"},
                            {"LinkLibraryDependencies", "true"},
                            {
                                "MSBuildSourceProjectFile",
                                @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\NETCoreCppCliTestB.vcxproj"
                            },
                            {"MSBuildSourceTargetName", "Build"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug\NETCoreCppCliTestB.dll"
                            },
                            {"OriginalProjectReferenceItemSpec", @"..\NETCoreCppCliTestB\NETCoreCppCliTestB.vcxproj"},
                            {"OutputItemType", "_ResolvedNativeProjectReferencePaths"},
                            {"Private", "true"},
                            {"Project", "{c8d22d0f-0c2d-4b4a-bf28-14a2d001102b}"},
                            {"ProjectReferenceOriginalItemSpec", @"..\NETCoreCppCliTestB\NETCoreCppCliTestB.vcxproj"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ProjectReference"},
                            {
                                "ResolvedFrom",
                                @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug\NETCoreCppCliTestB.dll"
                            },
                            {"SkipGetTargetFrameworkProperties", "true"},
                            {"Targets", ""},
                            {"UseLibraryDependencyInputs", "false"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\netstandard.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "2.1.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\netstandard.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.AppContext.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.AppContext, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.AppContext.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Buffers.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Buffers, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Buffers.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Concurrent.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.15.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.Concurrent, Version=4.0.15.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Concurrent.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Immutable.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "1.2.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.Immutable, Version=1.2.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Immutable.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.NonGeneric.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.NonGeneric, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.NonGeneric.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Specialized.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.Specialized, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Specialized.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Annotations.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.3.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.Annotations, Version=4.3.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Annotations.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.DataAnnotations.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.DataAnnotations, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.DataAnnotations.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "31bf3856ad364e35"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.EventBasedAsync.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.EventBasedAsync, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.EventBasedAsync.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.Primitives, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.TypeConverter.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.TypeConverter, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.TypeConverter.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Configuration.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Configuration.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Console.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Console, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Console.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Core.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Core.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.Common.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Data.Common, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.Common.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.DataSetExtensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Data.DataSetExtensions, Version=4.0.1.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.DataSetExtensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Contracts.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Contracts, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Contracts.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Debug.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Debug, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Debug.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.DiagnosticSource.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.DiagnosticSource, Version=4.0.5.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.DiagnosticSource.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.FileVersionInfo.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.FileVersionInfo, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.FileVersionInfo.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Process.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Process, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Process.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.StackTrace.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.StackTrace, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.StackTrace.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TextWriterTraceListener.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.TextWriterTraceListener, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TextWriterTraceListener.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tools.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Tools, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tools.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TraceSource.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.TraceSource, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TraceSource.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tracing.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Tracing, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tracing.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {"FusionName", "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"},
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Drawing.Primitives, Version=4.2.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Dynamic.Runtime.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Dynamic.Runtime, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Dynamic.Runtime.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Calendars.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Globalization.Calendars, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Calendars.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Globalization, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Globalization.Extensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.Brotli.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression.Brotli, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.Brotli.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.FileSystem.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression.FileSystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.FileSystem.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.ZipFile.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression.ZipFile, Version=4.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.ZipFile.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.DriveInfo.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem.DriveInfo, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.DriveInfo.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Watcher.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem.Watcher, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Watcher.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.IsolatedStorage.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.IsolatedStorage, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.IsolatedStorage.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.MemoryMappedFiles.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.MemoryMappedFiles, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.MemoryMappedFiles.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Pipes.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Pipes, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Pipes.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.UnmanagedMemoryStream.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.UnmanagedMemoryStream, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.UnmanagedMemoryStream.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Expressions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq.Expressions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Expressions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Parallel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq.Parallel, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Parallel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Queryable.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq.Queryable, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Queryable.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Memory.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Memory, Version=4.2.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Memory.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Http.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Http, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Http.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.HttpListener.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.HttpListener, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.HttpListener.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Mail.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Mail, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Mail.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NameResolution.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.NameResolution, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NameResolution.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NetworkInformation.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.NetworkInformation, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NetworkInformation.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Ping.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Ping, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Ping.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Requests.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Requests, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Requests.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Security.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Security, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Security.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.ServicePoint.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.ServicePoint, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.ServicePoint.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Sockets.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Sockets, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Sockets.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebClient.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebClient, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebClient.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebHeaderCollection.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebHeaderCollection, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebHeaderCollection.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebProxy.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebProxy, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebProxy.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.Client.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebSockets.Client, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.Client.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebSockets, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.Vectors.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.6.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Numerics.Vectors, Version=4.1.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.Vectors.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ObjectModel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ObjectModel, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ObjectModel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.DispatchProxy.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.6.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.DispatchProxy, Version=4.0.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.DispatchProxy.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Emit, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.ILGeneration.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Emit.ILGeneration, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.ILGeneration.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.Lightweight.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Emit.Lightweight, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.Lightweight.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Extensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Metadata.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "1.4.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Metadata, Version=1.4.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Metadata.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.TypeExtensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.TypeExtensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.TypeExtensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Reader.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Resources.Reader, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Reader.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.ResourceManager.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Resources.ResourceManager, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.ResourceManager.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Writer.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Resources.Writer, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Writer.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.Unsafe.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.6.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.CompilerServices.Unsafe, Version=4.0.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.Unsafe.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.VisualC.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.CompilerServices.VisualC, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.VisualC.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Extensions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Handles.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Handles, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Handles.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.InteropServices, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.RuntimeInformation.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.InteropServices.RuntimeInformation, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.RuntimeInformation.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.WindowsRuntime.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.InteropServices.WindowsRuntime, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.WindowsRuntime.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Intrinsics.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Intrinsics, Version=4.0.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Intrinsics.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Loader.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Loader, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Loader.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Numerics.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Numerics, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Numerics.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Formatters.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Formatters, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Formatters.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Json.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Json, Version=4.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Json.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Primitives, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Xml.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Xml, Version=4.1.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Xml.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Claims.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Claims, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Claims.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Algorithms.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.3.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Algorithms, Version=4.3.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Algorithms.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Csp.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Csp, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Csp.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Encoding.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Encoding, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Encoding.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.X509Certificates.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.X509Certificates, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.X509Certificates.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Principal.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Principal, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Principal.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.SecureString.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.SecureString, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.SecureString.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceModel.Web.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ServiceModel.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceModel.Web.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "31bf3856ad364e35"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceProcess.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceProcess.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.CodePages.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.3.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encoding.CodePages, Version=4.1.3.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.CodePages.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encoding, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encoding.Extensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encodings.Web.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encodings.Web, Version=4.0.5.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encodings.Web.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Json.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Json, Version=4.0.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Json.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.RegularExpressions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.RegularExpressions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.RegularExpressions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Channels.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Channels, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Channels.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Overlapped.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Overlapped, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Overlapped.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Dataflow.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.6.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks.Dataflow, Version=4.6.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Dataflow.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.3.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks.Extensions, Version=4.3.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Parallel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks.Parallel, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Parallel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Thread.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Thread, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Thread.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.ThreadPool.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.ThreadPool, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.ThreadPool.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Timer.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Timer, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Timer.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Transactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.Local.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Transactions.Local, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.Local.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ValueTuple.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.3.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ValueTuple, Version=4.0.3.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ValueTuple.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.HttpUtility.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Web.HttpUtility, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.HttpUtility.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Windows.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Windows, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Windows.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Linq.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Linq.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.ReaderWriter.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.ReaderWriter, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.ReaderWriter.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Serialization.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Serialization.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XDocument.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XDocument, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XDocument.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlDocument.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XmlDocument, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlDocument.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlSerializer.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XmlSerializer, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlSerializer.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XPath, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.XDocument.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XPath.XDocument, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.XDocument.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\WindowsBase.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\WindowsBase.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "31bf3856ad364e35"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        })
                },
                ReferenceDependencyPaths = new MockTaskItem[]
                {
                    new MockTaskItem(
                        itemSpec: @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug\NETCoreCppCliTestC.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"BuildReference", "true"},
                            {"CopyLocal", "true"},
                            {"FusionName", "NETCoreCppCliTestC, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"},
                            {"ImageRuntime", "v4.0.30319"},
                            {"IncludeRuntimeDependency", "true"},
                            {"LinkLibraryDependencies", "true"},
                            {
                                "MSBuildSourceProjectFile",
                                @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\NETCoreCppCliTestB.vcxproj"
                            },
                            {"MSBuildSourceTargetName", "Build"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug\NETCoreCppCliTestB.dll"
                            },
                            {"OriginalProjectReferenceItemSpec", @"..\NETCoreCppCliTestB\NETCoreCppCliTestB.vcxproj"},
                            {"OutputItemType", "_ResolvedNativeProjectReferencePaths"},
                            {"Private", "true"},
                            {"Project", "{c8d22d0f-0c2d-4b4a-bf28-14a2d001102b}"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ProjectReference"},
                            {"ResolvedFrom", @"C:\work\NETCoreCppCliTest\NETCoreCppCliTestB\Debug"},
                            {"SkipGetTargetFrameworkProperties", "true"},
                            {"Targets", ""},
                            {"UseLibraryDependencyInputs", "false"},
                            {"Version", ""},
                        })
                },
                ReferenceAssemblies = new MockTaskItem[]
                {
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.CSharp.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.CSharp.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.Core.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "10.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.VisualBasic.Core, Version=10.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.Core.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "10.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.VisualBasic.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.Win32.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "Microsoft.Win32.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\Microsoft.Win32.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\mscorlib.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\mscorlib.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\netstandard.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "2.1.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\netstandard.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.AppContext.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.AppContext, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.AppContext.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Buffers.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Buffers, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Buffers.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Concurrent.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.15.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.Concurrent, Version=4.0.15.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Concurrent.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Immutable.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "1.2.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.Immutable, Version=1.2.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Immutable.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.NonGeneric.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.NonGeneric, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.NonGeneric.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Specialized.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Collections.Specialized, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Collections.Specialized.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Annotations.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.3.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.Annotations, Version=4.3.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Annotations.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.DataAnnotations.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.DataAnnotations, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.DataAnnotations.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "31bf3856ad364e35"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.EventBasedAsync.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.EventBasedAsync, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.EventBasedAsync.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.Primitives, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.TypeConverter.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ComponentModel.TypeConverter, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ComponentModel.TypeConverter.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Configuration.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Configuration.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Console.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Console, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Console.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Core.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Core.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.Common.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Data.Common, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.Common.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.DataSetExtensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Data.DataSetExtensions, Version=4.0.1.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.DataSetExtensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Data.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Contracts.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Contracts, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Contracts.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Debug.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Debug, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Debug.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.DiagnosticSource.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.DiagnosticSource, Version=4.0.5.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.DiagnosticSource.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.FileVersionInfo.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.FileVersionInfo, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.FileVersionInfo.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Process.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Process, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Process.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.StackTrace.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.StackTrace, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.StackTrace.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TextWriterTraceListener.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.TextWriterTraceListener, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TextWriterTraceListener.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tools.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Tools, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tools.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TraceSource.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.TraceSource, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.TraceSource.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tracing.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Diagnostics.Tracing, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Diagnostics.Tracing.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {"FusionName", "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"},
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Drawing.Primitives, Version=4.2.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Drawing.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Dynamic.Runtime.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Dynamic.Runtime, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Dynamic.Runtime.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Calendars.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Globalization.Calendars, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Calendars.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Globalization, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Globalization.Extensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Globalization.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.Brotli.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression.Brotli, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.Brotli.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.FileSystem.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression.FileSystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.FileSystem.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.ZipFile.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Compression.ZipFile, Version=4.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Compression.ZipFile.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.DriveInfo.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem.DriveInfo, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.DriveInfo.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Watcher.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.FileSystem.Watcher, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.FileSystem.Watcher.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.IsolatedStorage.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.IsolatedStorage, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.IsolatedStorage.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.MemoryMappedFiles.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.MemoryMappedFiles, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.MemoryMappedFiles.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Pipes.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.Pipes, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.Pipes.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.UnmanagedMemoryStream.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.IO.UnmanagedMemoryStream, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.IO.UnmanagedMemoryStream.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Expressions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq.Expressions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Expressions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Parallel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq.Parallel, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Parallel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Queryable.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Linq.Queryable, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Linq.Queryable.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Memory.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Memory, Version=4.2.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Memory.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Http.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Http, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Http.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.HttpListener.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.HttpListener, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.HttpListener.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Mail.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Mail, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Mail.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NameResolution.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.NameResolution, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NameResolution.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NetworkInformation.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.NetworkInformation, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.NetworkInformation.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Ping.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Ping, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Ping.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Requests.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Requests, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Requests.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Security.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Security, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Security.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.ServicePoint.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.ServicePoint, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.ServicePoint.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Sockets.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.Sockets, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.Sockets.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebClient.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebClient, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebClient.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebHeaderCollection.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebHeaderCollection, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebHeaderCollection.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebProxy.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebProxy, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebProxy.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.Client.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebSockets.Client, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.Client.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Net.WebSockets, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Net.WebSockets.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.Vectors.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.6.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Numerics.Vectors, Version=4.1.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Numerics.Vectors.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ObjectModel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ObjectModel, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ObjectModel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.DispatchProxy.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.6.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.DispatchProxy, Version=4.0.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.DispatchProxy.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Emit, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.ILGeneration.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Emit.ILGeneration, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.ILGeneration.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.Lightweight.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Emit.Lightweight, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Emit.Lightweight.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Extensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Metadata.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "1.4.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Metadata, Version=1.4.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Metadata.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.TypeExtensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Reflection.TypeExtensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Reflection.TypeExtensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Reader.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Resources.Reader, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Reader.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.ResourceManager.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Resources.ResourceManager, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.ResourceManager.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Writer.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Resources.Writer, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Resources.Writer.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.Unsafe.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.6.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.CompilerServices.Unsafe, Version=4.0.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.Unsafe.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.VisualC.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.CompilerServices.VisualC, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.CompilerServices.VisualC.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Extensions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Handles.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Handles, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Handles.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.InteropServices, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.RuntimeInformation.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.InteropServices.RuntimeInformation, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.RuntimeInformation.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.WindowsRuntime.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.InteropServices.WindowsRuntime, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.InteropServices.WindowsRuntime.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Intrinsics.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Intrinsics, Version=4.0.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Intrinsics.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Loader.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Loader, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Loader.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Numerics.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Numerics, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Numerics.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Formatters.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Formatters, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Formatters.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Json.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Json, Version=4.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Json.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Primitives, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Xml.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Runtime.Serialization.Xml, Version=4.1.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Runtime.Serialization.Xml.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Claims.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Claims, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Claims.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Algorithms.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.3.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Algorithms, Version=4.3.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Algorithms.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Csp.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Csp, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Csp.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Encoding.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Encoding, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Encoding.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Primitives.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.Primitives, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.Primitives.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.X509Certificates.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Cryptography.X509Certificates, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Cryptography.X509Certificates.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Principal.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.Principal, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.Principal.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.SecureString.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Security.SecureString, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Security.SecureString.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceModel.Web.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ServiceModel.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceModel.Web.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "31bf3856ad364e35"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceProcess.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ServiceProcess.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.CodePages.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.3.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encoding.CodePages, Version=4.1.3.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.CodePages.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encoding, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encoding.Extensions, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encoding.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encodings.Web.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Encodings.Web, Version=4.0.5.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Encodings.Web.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Json.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.Json, Version=4.0.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.Json.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.RegularExpressions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Text.RegularExpressions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Text.RegularExpressions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Channels.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Channels, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Channels.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Overlapped.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Overlapped, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Overlapped.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Dataflow.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.6.5.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks.Dataflow, Version=4.6.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Dataflow.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Extensions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.3.1.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks.Extensions, Version=4.3.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Extensions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Parallel.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.4.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Tasks.Parallel, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Tasks.Parallel.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Thread.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Thread, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Thread.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.ThreadPool.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.ThreadPool, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.ThreadPool.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Timer.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Threading.Timer, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Threading.Timer.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Transactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.Local.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Transactions.Local, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Transactions.Local.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ValueTuple.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.3.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.ValueTuple, Version=4.0.3.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.ValueTuple.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.HttpUtility.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Web.HttpUtility, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Web.HttpUtility.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "cc7b13ffcd2ddd51"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Windows.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Windows, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Windows.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Linq.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Linq.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.ReaderWriter.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.2.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.ReaderWriter, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.ReaderWriter.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Serialization.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.Serialization.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b77a5c561934e089"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XDocument.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XDocument, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XDocument.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlDocument.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XmlDocument, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlDocument.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlSerializer.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XmlSerializer, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XmlSerializer.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XPath, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.XDocument.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.1.2.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "System.Xml.XPath.XDocument, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\System.Xml.XPath.XDocument.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "b03f5f7f11d50a3a"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        }),
                    new MockTaskItem(
                        itemSpec:
                        @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\WindowsBase.dll",
                        metadata: new Dictionary<string, string>
                        {
                            {"AssemblyVersion", "4.0.0.0"},
                            {"CopyLocal", "false"},
                            {"CopyLocalSatelliteAssemblies", "true"},
                            {"ExternallyResolved", "true"},
                            {"FileVersion", "4.700.19.55104"},
                            {"FrameworkReferenceName", "Microsoft.NETCore.App"},
                            {"FrameworkReferenceVersion", "3.1.0-preview3.19553.2"},
                            {
                                "FusionName",
                                "WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
                            },
                            {"ImageRuntime", "v4.0.30319"},
                            {"NuGetPackageId", "Microsoft.NETCore.App.Ref"},
                            {"NuGetPackageVersion", "3.1.0-preview3.19553.2"},
                            {
                                "OriginalItemSpec",
                                @"C:\work\sdk\.dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0-preview3.19553.2\ref\netcoreapp3.1\WindowsBase.dll"
                            },
                            {"Private", "false"},
                            {"PublicKeyToken", "31bf3856ad364e35"},
                            {"ReferenceOutputAssembly", "true"},
                            {"ReferenceSourceTarget", "ResolveAssemblyReference"},
                            {"ResolvedFrom", "{RawFileName}"},
                            {"Version", ""},
                        })
                }
            };

            Action a = () => task.PublicExecuteCore();
            a.ShouldNotThrow();

            File.ReadAllText(depsFilePath).Should()
                .Be(
                    @"");
        }

        private class TestableGenerateDepsFile : GenerateDepsFile
        {
            public void PublicExecuteCore()
            {
                base.ExecuteCore();
            }
        }

        private class MockBuildEngine4 : MockBuildEngine, IBuildEngine4
        {
            public bool IsRunningMultipleNodes => throw new System.NotImplementedException();

            public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalProperties,
                IDictionary targetOutputs, string toolsVersion)
            {
                throw new System.NotImplementedException();
            }

            public BuildEngineResult BuildProjectFilesInParallel(string[] projectFileNames, string[] targetNames,
                IDictionary[] globalProperties, IList<string>[] removeGlobalProperties, string[] toolsVersion,
                bool returnTargetOutputs)
            {
                throw new System.NotImplementedException();
            }

            public bool BuildProjectFilesInParallel(string[] projectFileNames, string[] targetNames,
                IDictionary[] globalProperties, IDictionary[] targetOutputsPerProject, string[] toolsVersion,
                bool useResultsCache, bool unloadProjectsOnCompletion)
            {
                throw new System.NotImplementedException();
            }

            public object GetRegisteredTaskObject(object key, RegisteredTaskObjectLifetime lifetime)
            {
                return null;
            }

            public void Reacquire()
            {
                throw new System.NotImplementedException();
            }

            public void RegisterTaskObject(object key, object obj, RegisteredTaskObjectLifetime lifetime,
                bool allowEarlyCollection)
            {
                return;
            }

            public object UnregisterTaskObject(object key, RegisteredTaskObjectLifetime lifetime)
            {
                throw new System.NotImplementedException();
            }

            public void Yield()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
