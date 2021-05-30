using RaftConsensus.Consensus.Interfaces;
using System;
using System.Threading;
using RaftConsensus.Messages.Enums;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.Consensus.States
{
    internal abstract class RaftConsensusStateBase : IDisposable
    {
        protected readonly IRaftConsensus Context;
        private readonly int _actionTimeoutMilliseconds;
        private readonly Thread _backgroundThread;
        private readonly CancellationTokenSource _cancellationTokenSource;

        protected RaftConsensusStateBase(IRaftConsensus context, int actionTimeoutMilliseconds)
        {
            Context = context;
            _actionTimeoutMilliseconds = actionTimeoutMilliseconds;

            _cancellationTokenSource = new CancellationTokenSource();

            _backgroundThread = new Thread(() => BackgroundThread(_cancellationTokenSource.Token))
            {
                IsBackground = true
            };

            _backgroundThread.Start();
        }
        
        private void ProcessMessage()
        {
            var raftMessage = Context.MessageBroker.ReceiveQueue.Dequeue();

            //TODO: Check message version is what we're compatible with

            //TODO: Check we're tracking this person

            //TODO: Check if the term is higher

            switch (raftMessage.Type)
            {
                case RaftMessageType.AppendEntryRequest:
                    ProcessAppendEntryRequest(raftMessage);
                    break;
                case RaftMessageType.AppendEntryReply:
                    ProcessAppendEntryReply(raftMessage);
                    break;
                case RaftMessageType.RequestVoteRequest:
                    ProcessRequestVoteRequest(raftMessage);
                    break;
                case RaftMessageType.RequestVoteReply:
                    ProcessRequestVoteReply(raftMessage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("raftMessage.Type","Message type not one of accepted RaftMessageType types");
            }
        }

        protected abstract void ProcessAppendEntryRequest(IRaftMessage raftMessage);
        protected abstract void ProcessAppendEntryReply(IRaftMessage raftMessage);
        protected abstract void ProcessRequestVoteRequest(IRaftMessage raftMessage);
        protected abstract void ProcessRequestVoteReply(IRaftMessage raftMessage);

        protected abstract void TimeoutAction();

        private void BackgroundThread(CancellationToken cancellationToken)
        {
            while (true)
            {
                const int messageReceivedEventIndex = 0;
                const int cancellationTimeoutEventIndex = 1;

                var signaledIndex = WaitHandle.WaitAny(new[] { Context.MessageBroker.ReceiveQueue.GetWaitHandle(), cancellationToken.WaitHandle}, _actionTimeoutMilliseconds);

                switch (signaledIndex)
                {
                    case messageReceivedEventIndex:
                        ProcessMessage();
                        break;
                    case cancellationTimeoutEventIndex:
                        return;
                    case WaitHandle.WaitTimeout:
                        TimeoutAction();
                        break;
                }
            }
        }

        public virtual void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _backgroundThread.Join();
            _cancellationTokenSource.Dispose();
        }
    }
}
