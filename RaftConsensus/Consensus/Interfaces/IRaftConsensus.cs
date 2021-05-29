using RaftConsensus.Consensus.Enums;
using RaftConsensus.Messages.Interfaces;
using RaftConsensus.PeerManagement.Interfaces;
using RaftConsensus.Settings;

namespace RaftConsensus.Consensus.Interfaces
{
    public interface IRaftConsensus
    {
        void ProcessMessage(IRaftMessage raftMessage);
        RaftConsensusState State { get; set; }
        IRaftPeerManagement PeerManagement { get; }
        RaftConsensusStateSettings Settings { get; }
    }
}
