using RaftConsensus.Consensus.Enums;

namespace RaftConsensus.Consensus.States.Interfaces
{
    public interface IRaftConsensusStateFactory
    {
        RaftConsensusStateBase CreateState(RaftConsensusState state, RaftConsensusContext context);
    }
}
