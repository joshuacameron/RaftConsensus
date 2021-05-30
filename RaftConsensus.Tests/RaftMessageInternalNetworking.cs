using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.Messages.Identification;
using RaftConsensus.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using RaftConsensus.Messages.Identification.Interfaces;

namespace RaftConsensus.Tests
{
    public class RaftMessageInternalNetworking : IDisposable
    {
        private readonly List<Tuple<IPeerIdentification, IRaftMessageQueues>> _peerQueues;

        private Thread _backgroundThread;
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isRunning;

        public RaftMessageInternalNetworking()
        {
            _peerQueues = new List<Tuple<IPeerIdentification, IRaftMessageQueues>>();

            _isRunning = false;
        }

        public void AddPeerQueues(IPeerIdentification peer, IRaftMessageQueues queues)
        {
            if (_isRunning)
            {
                throw new InvalidOperationException("Cannot add peer queues when InternalNetworking already running");
            }

            _peerQueues.Add(new Tuple<IPeerIdentification, IRaftMessageQueues>(peer, queues));
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _backgroundThread = new Thread(() => BackgroundThread(_cancellationTokenSource.Token))
            {
                IsBackground = true
            };

            _backgroundThread.Start();

            _isRunning = true;
        }

        private void BackgroundThread(CancellationToken cancellationToken)
        {
            const int cancellationTimeoutEventIndex = 0;

            var waitHandles = new List<WaitHandle> { cancellationToken.WaitHandle };

            foreach (var (_, item2) in _peerQueues)
            {
                waitHandles.Add(item2.SendQueue.GetWaitHandle());
                waitHandles.Add(item2.BroadcastQueue.GetWaitHandle());
            }

            var waitHandlesArray = waitHandles.ToArray();

            while (true)
            {
                var signaledIndex = WaitHandle.WaitAny(waitHandlesArray);

                if (signaledIndex == cancellationTimeoutEventIndex)
                {
                    return;
                }

                var peerQueueIndex = (int)Math.Round(signaledIndex / (double) 2) - 1;
                var peer = _peerQueues[peerQueueIndex];
                var isBroadcast = signaledIndex % 2 == 0;

                if (isBroadcast)
                {
                    var message = peer.Item2.BroadcastQueue.Dequeue();
                    BroadcastMessage(message);
                }
                else
                {
                    var message = peer.Item2.SendQueue.Dequeue();
                    SendMessage(message);
                }
            }
        }

        private void SendMessage(IRaftMessage message)
        {
            foreach (var peerQueue in _peerQueues)
            {
                if (!peerQueue.Item1.Equals(message.To)) continue;

                peerQueue.Item2.ReceiveQueue.Enqueue(message);
                return;
            }
        }

        private void BroadcastMessage(IRaftMessage message)
        {
            foreach (var peerQueue in _peerQueues)
            {
                if (!peerQueue.Item1.Equals(message.From))
                {
                    peerQueue.Item2.ReceiveQueue.Enqueue(message);
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _backgroundThread.Join();
            _cancellationTokenSource.Dispose();
        }
    }
}
