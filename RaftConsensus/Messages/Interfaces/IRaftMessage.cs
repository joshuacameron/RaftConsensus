using RaftConsensus.Messages.Enums;
using RaftConsensus.Messages.Identification;
using RaftConsensus.Messages.Identification.Interfaces;

namespace RaftConsensus.Messages.Interfaces
{
    public interface IRaftMessage
    {
        IPeerIdentification To { get; }
        IPeerIdentification From { get; }
        RaftMessageType Type { get; }
        IMessageVersionIdentification Version { get; }
        int Term { get; }
    }
}
