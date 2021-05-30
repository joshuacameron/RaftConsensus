using RaftConsensus.MessageBroker.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace RaftConsensus.MessageBroker
{
    public class FlaggedQueue<T> : IFlaggedQueue<T>
    {
        private readonly Queue<T> _queue;
        private readonly ManualResetEvent _flag;

        public FlaggedQueue()
        {
            _queue = new Queue<T>();
            _flag = new ManualResetEvent(false);
        }

        public void Enqueue(T item)
        {
            lock (_queue)
            {
                _queue.Enqueue(item);
                _flag.Set();
            }
        }

        public T Dequeue()
        {
            lock (_queue)
            {
                if (_queue.Count == 1)
                {
                    _flag.Reset();
                }

                return _queue.Dequeue();
            }
        }

        public WaitHandle GetWaitHandle()
        {
            return _flag;
        }
    }
}
