using RaftConsensus.Messages.Identification;

namespace RaftConsensus.Messages.Interfaces
{
    public interface IRaftMessage
    {
        IPeerIdentification To { get; }
        IPeerIdentification From { get; }
        IMessageTypeIdentification Type { get; }
        IMessageVersionIdentification Version { get; }
        int Term { get; }
    }
}
