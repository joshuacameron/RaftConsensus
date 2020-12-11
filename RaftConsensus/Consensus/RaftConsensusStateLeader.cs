using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;

namespace RaftConsensus.Consensus
{
    internal class RaftConsensusStateLeader : RaftConsensusStateBase
    {
        public RaftConsensusStateLeader(IRaftConsensus context)
            : base(context)
        {

        }

        public override void ProcessMessage(IRaftMessage raftMessage)
        {
            base.ProcessMessage(raftMessage);
        }

        protected override void TimeoutAction()
        {
            
        }
    }
}
