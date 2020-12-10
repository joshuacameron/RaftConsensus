using RaftConsensus.Messages.Enums;

namespace RaftConsensus.Messages.Interfaces
{
    public interface IRaftMessage
    {
        RaftMessageType GetMessageType();
    }
}
