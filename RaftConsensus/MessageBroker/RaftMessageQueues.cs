using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.MessageBroker
{
    public class RaftMessageQueues : IRaftMessageQueues
    {
        public FlaggedQueue<IRaftMessage> SendQueue { get; }
        public FlaggedQueue<IRaftMessage> BroadcastQueue { get; }
        public FlaggedQueue<IRaftMessage> ReceiveQueue { get; }

        public RaftMessageQueues()
        {
            SendQueue = new FlaggedQueue<IRaftMessage>();
            BroadcastQueue = new FlaggedQueue<IRaftMessage>();
            ReceiveQueue = new FlaggedQueue<IRaftMessage>();
        }
    }
}
