// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using NuGet.Common;
using NuGet.Protocol;
using Xunit.Abstractions;

namespace Microsoft.DotNet.ToolPackage.Tests
{
    internal sealed class ConsolePrintDownloadProcessReporter : IObserver<DownloadProcess>
    {
        private readonly ILogger _log;
        private IDisposable _unsubscriber;

        public ConsolePrintDownloadProcessReporter(ILogger log) =>
            _log = log ?? throw new ArgumentNullException(nameof(log));

        public void OnCompleted() => _log.LogInformation("Done");

        public void OnError(Exception error)
        {
            // Do nothing.
        }

        public void OnNext(DownloadProcess value) =>
            _log.LogInformation(value.TotalByte == null
                ? $"Finished {value.ByteSoFar} byte of total unknown%"
                : $"Finished {value.ByteSoFar * 100 / value.TotalByte}%");

        public void Subscribe(IObservable<DownloadProcess> provider) =>
            _unsubscriber = provider.Subscribe(this);

        public void Unsubscribe() => _unsubscriber.Dispose();
    }
}
