using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;
using System.Threading;

namespace RaftConsensus.Consensus
{
    internal class RaftConsensusStateFollower : RaftConsensusStateBase
    {
        public RaftConsensusStateFollower(IRaftConsensus context)
            : base(context)
        {

        }

        public override void ProcessMessage(IRaftMessage raftMessage)
        {

        }

        protected override void BackgroundThread(CancellationToken token)
        {

        }
    }
}
