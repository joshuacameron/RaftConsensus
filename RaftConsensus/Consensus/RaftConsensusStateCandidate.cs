using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Messages.Interfaces;
using System;

namespace RaftConsensus.Consensus
{
    internal class RaftConsensusStateCandidate : RaftConsensusStateBase
    {
        public RaftConsensusStateCandidate(IRaftConsensus context)
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
