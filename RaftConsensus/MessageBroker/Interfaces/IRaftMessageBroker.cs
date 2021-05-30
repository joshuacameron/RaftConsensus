using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.MessageBroker.Interfaces
{
    public interface IRaftMessageBroker
    {
        FlaggedQueue<IRaftMessage> SendQueue { get; }
        FlaggedQueue<IRaftMessage> ReceiveQueue { get; }
    }
}
