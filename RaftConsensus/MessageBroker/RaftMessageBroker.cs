using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.MessageBroker
{
    public class RaftMessageBroker : IRaftMessageBroker
    {
        public FlaggedQueue<IRaftMessage> SendQueue { get; }
        public FlaggedQueue<IRaftMessage> ReceiveQueue { get; }

        public RaftMessageBroker()
        {
            SendQueue = new FlaggedQueue<IRaftMessage>();
            ReceiveQueue = new FlaggedQueue<IRaftMessage>();
        }
    }
}
