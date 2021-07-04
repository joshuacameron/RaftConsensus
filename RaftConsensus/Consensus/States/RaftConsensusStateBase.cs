using RaftConsensus.Consensus.Interfaces;
using System;
using System.Threading;
using RaftConsensus.Consensus.Enums;
using RaftConsensus.Helpers.Interfaces;
using RaftConsensus.Messages.Enums;
using RaftConsensus.Messages.Interfaces;

/* TODO: Add logging
 *
 *
 */

namespace RaftConsensus.Consensus.States
{
    public abstract class RaftConsensusStateBase : IDisposable
    {
        private readonly IRaftConsensus _context;
        private readonly IWaiter _waiter;
        private readonly int _actionTimeoutMilliseconds;
        private readonly Thread _backgroundThread;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _changeStateRequested;

        protected RaftConsensusStateBase(IRaftConsensus context, IWaiter waiter, int actionTimeoutMilliseconds)
        {
            _context = context;
            _waiter = waiter;
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
            var raftMessage = _context.MessageQueues.ReceiveQueue.Dequeue();

            if (raftMessage == null)
            {
                return;
            }

            //TODO: Check not null

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

        //TODO: these IRaftMessage types should be less abstract
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
                const int cancellationEventIndex = 1;

                var signaledIndex = _waiter.Wait(new[] { _context.MessageQueues.ReceiveQueue.GetWaitHandle(), cancellationToken.WaitHandle}, _actionTimeoutMilliseconds);

                switch (signaledIndex)
                {
                    case messageReceivedEventIndex:
                        ProcessMessage();
                        break;
                    case cancellationEventIndex:
                        return;
                    case WaitHandle.WaitTimeout:
                        TimeoutAction();
                        break;
                }

                if (_changeStateRequested)
                {
                    return;
                }
            }
        }

        protected void ChangeState(RaftConsensusState state)
        {
            _changeStateRequested = true;
            _context.SetState(state);
        }

        //TODO: Suppress finalize?
        public virtual void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _backgroundThread.Join();
            _cancellationTokenSource.Dispose();
        }
    }
}
