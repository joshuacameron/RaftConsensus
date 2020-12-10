using RaftConsensus.Consensus.Enums;
using RaftConsensus.Log.Interfaces;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.Consensus.Interfaces
{
    public interface IRaftConsensus
    {
        void ProcessMessage(IRaftMessage raftMessage);
        void SetState(RaftConsensusState state);
        IRaftLog GetRaftLog();
    }
}
