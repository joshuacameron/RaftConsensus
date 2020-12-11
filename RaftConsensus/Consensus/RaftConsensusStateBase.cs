using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;
using System;
using System.Threading;

namespace RaftConsensus.Consensus
{
    public abstract class RaftConsensusStateBase : IRaftConsensusState, IDisposable
    {
        protected readonly IRaftConsensus Context;
        private readonly Thread _backgroundThread;
        private readonly CancellationTokenSource _cancellationTokenSource;

        protected RaftConsensusStateBase(IRaftConsensus context)
        {
            Context = context;

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

        protected abstract void BackgroundThread(CancellationToken token);

        public virtual void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _backgroundThread.Join();
            _cancellationTokenSource.Dispose();
        }
    }
}
