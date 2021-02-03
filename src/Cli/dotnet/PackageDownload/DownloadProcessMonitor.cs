// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Microsoft.DotNet.Cli.PackageDownload
{
    public class DownloadProcessMonitor : IObservable<DownloadProcess>
    {
        private readonly List<IObserver<DownloadProcess>> _observers;

        public DownloadProcessMonitor() => _observers = new List<IObserver<DownloadProcess>>();

        public IDisposable Subscribe(IObserver<DownloadProcess> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(_observers, observer);
        }

        public void SendTotalSoFar(long? total, int totalSoFar)
        {
            DownloadProcess tempData = new(total, totalSoFar);
            foreach (IObserver<DownloadProcess> observer in _observers)
                observer.OnNext(tempData);
        }

        public void Clear()
        {
            foreach (IObserver<DownloadProcess> observer in _observers.ToArray())
            {
                observer?.OnCompleted();
            }

            _observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly IObserver<DownloadProcess> _observer;
            private readonly List<IObserver<DownloadProcess>> _observers;

            public Unsubscriber(List<IObserver<DownloadProcess>> observers, IObserver<DownloadProcess> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null) _observers.Remove(_observer);
            }
        }
    }
}
