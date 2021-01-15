// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DotNet.ToolPackage.Tests
{
    public class DownloadProcess
    {
        public long? TotalByte { get; }
        public int ByteSoFar { get; }

        public DownloadProcess(long? totalByte, int byteSoFar)
        {
            TotalByte = totalByte;
            ByteSoFar = byteSoFar;
        }
    }
}
