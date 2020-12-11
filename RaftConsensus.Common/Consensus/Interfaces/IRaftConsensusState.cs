using RaftConsensus.Common.Messages.Interfaces;

namespace RaftConsensus.Common.Consensus.Interfaces
{
    public interface IRaftConsensusState
    {
        void ProcessMessage(IRaftMessage raftMessage);
    }
}
