using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Messages.Interfaces;
using System;

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

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
