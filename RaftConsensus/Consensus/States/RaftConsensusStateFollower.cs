using System;
using Microsoft.Extensions.Logging;
using RaftConsensus.Consensus.Enums;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Helpers.Interfaces;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.Consensus.States
{
    internal class RaftConsensusStateFollower : RaftConsensusStateBase
    {
        private readonly ILogger<RaftConsensusStateFollower> _logger;

        public RaftConsensusStateFollower(ILogger<RaftConsensusStateFollower> logger, IRaftConsensus context, IWaiter waiter)
            : base(context, waiter, context.Settings.FollowerTimeoutMilliseconds)
        {
            _logger = logger;
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
            ChangeState(RaftConsensusState.Candidate);
        }
    }
}