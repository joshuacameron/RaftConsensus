using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.MessageBroker.Interfaces
{
    public interface IRaftMessageQueues
    {
        FlaggedQueue<IRaftMessage> SendQueue { get; }
        FlaggedQueue<IRaftMessage> BroadcastQueue { get; }
        FlaggedQueue<IRaftMessage> ReceiveQueue { get; }
    }
}
