using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.Messages.Interfaces;
using System;

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

        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
