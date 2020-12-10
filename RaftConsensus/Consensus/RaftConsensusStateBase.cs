using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;

namespace RaftConsensus.Consensus
{
    public abstract class RaftConsensusStateBase : IRaftConsensusState
    {
        protected readonly IRaftConsensus Context;

        protected RaftConsensusStateBase(IRaftConsensus context)
        {
            Context = context;
        }

        public virtual void ProcessMessage(IRaftMessage raftMessage)
        {
            //TODO: Check we're tracking this person

            //TODO: Check if the term is higher
        }

        public abstract void Dispose();
    }
}
