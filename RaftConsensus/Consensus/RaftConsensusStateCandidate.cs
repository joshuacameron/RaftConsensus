using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;

namespace RaftConsensus.Consensus
{
    internal class RaftConsensusStateCandidate : RaftConsensusStateBase
    {
        public RaftConsensusStateCandidate(IRaftConsensus context)
            : base(context, context.Settings.CandidateTimeoutSeconds)
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
