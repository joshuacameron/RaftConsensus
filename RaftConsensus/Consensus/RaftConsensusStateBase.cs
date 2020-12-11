using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RaftConsensus.Consensus
{
    internal abstract class RaftConsensusStateBase : IRaftConsensusState
    {
        protected readonly IRaftConsensus Context;
        private readonly int _actionTimeoutMilliseconds;
        private readonly Thread _backgroundThread;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ManualResetEvent _resetTimeoutEvent;

        protected RaftConsensusStateBase(IRaftConsensus context, int actionTimeoutMilliseconds)
        {
            Context = context;
            _actionTimeoutMilliseconds = actionTimeoutMilliseconds;
            _resetTimeoutEvent = new ManualResetEvent(false);

            _cancellationTokenSource = new CancellationTokenSource();

            _backgroundThread = new Thread(() => BackgroundThread(_cancellationTokenSource.Token))
            {
                IsBackground = true
            };

            _backgroundThread.Start();
        }
        
        public virtual void ProcessMessage(IRaftMessage raftMessage)
        {
            //TODO: Check we're tracking this person

            //TODO: Check if the term is higher
        }

        private void BackgroundThread(CancellationToken cancellationToken)
        {
            while (true)
            {
                const int resetTimeoutEventIndex = 0;
                const int cancellationTimeoutEventIndex = 1;

                int signaledIndex = WaitHandle.WaitAny(new[] { _resetTimeoutEvent, cancellationToken.WaitHandle}, _actionTimeoutMilliseconds);

                switch (signaledIndex)
                {
                    case resetTimeoutEventIndex:
                        break;
                    case cancellationTimeoutEventIndex:
                        return;
                    case WaitHandle.WaitTimeout:
                        TimeoutAction();
                        break;
                }
            }
        }

        protected void ResetTimeout()
        {
            _resetTimeoutEvent.Set();
        }

        protected abstract void TimeoutAction();

        public virtual void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _backgroundThread.Join();
            _cancellationTokenSource.Dispose();
        }
    }
}
