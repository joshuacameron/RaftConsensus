using System;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.Consensus.States
{
    internal class RaftConsensusStateFollower : RaftConsensusStateBase
    {
        public RaftConsensusStateFollower(IRaftConsensus context)
            : base(context, context.Settings.FollowerTimeoutMilliseconds)
        {

        }

        protected override void ProcessAppendEntryRequest(IRaftMessage raftMessage)
        {
            throw new NotImplementedException();
        }

        protected override void ProcessAppendEntryReply(IRaftMessage raftMessage)
        {
            throw new NotImplementedException();
        }

        protected override void ProcessRequestVoteRequest(IRaftMessage raftMessage)
        {
            throw new NotImplementedException();
        }

        protected override void ProcessRequestVoteReply(IRaftMessage raftMessage)
        {
            throw new NotImplementedException();
        }

        protected override void TimeoutAction()
        {

        }
    }
}