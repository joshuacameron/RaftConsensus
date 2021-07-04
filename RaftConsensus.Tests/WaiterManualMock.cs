using System;
using RaftConsensus.Helpers.Interfaces;
using System.Threading;

namespace RaftConsensus.Tests
{
    internal class WaiterManualMock : IWaiter, IDisposable
    {
        private readonly ManualResetEvent _timeoutTrigger;

        public WaiterManualMock()
        {
            _timeoutTrigger = new ManualResetEvent(false);
        }

        public int Wait(WaitHandle[] waitHandles, int millisecondsTimeout)
        {
            var waitHandlesWithTimeout = new WaitHandle[waitHandles.Length + 1];
            Array.Copy(waitHandles, 0, waitHandlesWithTimeout, 0, waitHandles.Length);
            waitHandlesWithTimeout[^1] = _timeoutTrigger;

            var result = WaitHandle.WaitAny(waitHandlesWithTimeout);

            _timeoutTrigger.Reset();

            return result == waitHandlesWithTimeout.Length - 1 ? WaitHandle.WaitTimeout : result;
        }

        public void TriggerTimeout()
        {
            _timeoutTrigger.Set();
        }

        public void Dispose()
        {
            _timeoutTrigger?.Dispose();
        }
    }
}
