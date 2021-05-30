using RaftConsensus.Consensus.Enums;
using RaftConsensus.MessageBroker.Interfaces;
using RaftConsensus.PeerManagement.Interfaces;
using RaftConsensus.Settings;

namespace RaftConsensus.Consensus.Interfaces
{
    public interface IRaftConsensus
    {
        RaftConsensusState State { get; set; }
        IRaftPeerManagement PeerManagement { get; }
        IRaftMessageBroker MessageBroker { get; }
        RaftConsensusStateSettings Settings { get; }
    }
}
