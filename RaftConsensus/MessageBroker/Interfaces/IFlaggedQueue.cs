using System.Threading;

namespace RaftConsensus.MessageBroker.Interfaces
{
    public interface IFlaggedQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        WaitHandle GetWaitHandle();
    }
}