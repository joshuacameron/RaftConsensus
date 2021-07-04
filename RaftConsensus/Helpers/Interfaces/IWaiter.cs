using System.Threading;

namespace RaftConsensus.Helpers.Interfaces
{
    public interface IWaiter
    {
        int Wait(WaitHandle[] waitHandles, int millisecondsTimeout);
    }
}
