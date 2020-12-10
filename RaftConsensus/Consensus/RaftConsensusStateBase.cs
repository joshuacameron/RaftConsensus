using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.Consensus
{
    public abstract class RaftConsensusStateBase :IRaftConsensusState
    {
        protected readonly IRaftConsensus Context;

        protected RaftConsensusStateBase(IRaftConsensus context)
        {
            Context = context;
        }

        public abstract void Dispose();

        public abstract void ProcessMessage(IRaftMessage raftMessage);
    }
}
