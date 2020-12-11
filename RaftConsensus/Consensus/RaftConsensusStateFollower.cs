using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;

namespace RaftConsensus.Consensus
{
    internal class RaftConsensusStateFollower : RaftConsensusStateBase
    {
        public RaftConsensusStateFollower(IRaftConsensus context)
            : base(context, context.Settings.FollowerTimeoutSeconds)
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
