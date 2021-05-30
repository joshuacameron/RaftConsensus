using System;
using Microsoft.Extensions.Logging;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Messages.Interfaces;

namespace RaftConsensus.Consensus.States
{
    internal class RaftConsensusStateLeader : RaftConsensusStateBase
    {
        private readonly ILogger<RaftConsensusStateLeader> _logger;

        public RaftConsensusStateLeader(ILogger<RaftConsensusStateLeader> logger, IRaftConsensus context)
            : base(context, context.Settings.LeaderTimeoutMilliseconds)
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
            
        }
    }
}
