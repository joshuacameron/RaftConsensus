using RaftConsensus.Helpers.Interfaces;
using System.Threading;

namespace RaftConsensus.Helpers
{
    public class Waiter :IWaiter
    {
        public int Wait(WaitHandle[] waitHandles, int millisecondsTimeout)
        {
            return WaitHandle.WaitAny(waitHandles, millisecondsTimeout);
        }
    }
}
