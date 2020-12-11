using RaftConsensus.Common.Consensus.Enums;
using RaftConsensus.Common.Log.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;
using RaftConsensus.Common.PeerManagement.Interfaces;
using RaftConsensus.Common.Settings;

namespace RaftConsensus.Common.Consensus.Interfaces
{
    public interface IRaftConsensus
    {
        void ProcessMessage(IRaftMessage raftMessage);
        RaftConsensusState State { get; set; }
        IRaftLog RaftLog { get; }
        IPeerManagement PeerManagement { get; }
        RaftConsensusStateSettings Settings { get; }
    }
}
