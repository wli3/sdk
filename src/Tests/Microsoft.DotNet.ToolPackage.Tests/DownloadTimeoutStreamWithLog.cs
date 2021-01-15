// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Protocol;

namespace Microsoft.DotNet.ToolPackage.Tests
{
    public class DownloadTimeoutStreamWithLog : DownloadTimeoutStream
    {
        private readonly DownloadProcessMonitor _downloadProcessMonitor;
        private readonly ILogger _logger;
        private readonly long? _total;

        private int _totalReadCount;

        public DownloadTimeoutStreamWithLog(long? total, DownloadProcessMonitor downloadProcessMonitor, ILogger logger,
            string downloadName,
            Stream networkStream,
            TimeSpan timeout) : base(downloadName, networkStream, timeout)
        {
            _total = total;
            _downloadProcessMonitor = downloadProcessMonitor;
            _logger = logger;
        }

        public override async Task<int> ReadAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken)
        {
            int result = await base.ReadAsync(buffer, offset, count, cancellationToken);

            _totalReadCount += result;
            _downloadProcessMonitor.SendTotalSoFar(_total, _totalReadCount);

            return result;
        }

        public override ValueTask DisposeAsync()
        {
            _downloadProcessMonitor.Clear();
            return base.DisposeAsync();
        }
    }
}
