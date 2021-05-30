using System;
using Autofac;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Messages.Interfaces;
using Xunit;

namespace RaftConsensus.Tests.Consensus
{
    public class RaftConsensusContextTests : TestBase
    {
        private readonly IRaftConsensus _node;

        public RaftConsensusContextTests()
        {
            _node = Container.Resolve<IRaftConsensus>();
        }
        
        [Fact]
        public void TestRaftConsensusInstantiates()
        {
            var a =_node.State;
            //Create Follower
            //Trigger timeout, become candidate
            //Send in two messages saying agree
            //Now leader
            //Timeout, send heartbeat
            //Reply to heartbeat
            //Send message out to commit
            //Get responses
            //Send out when happy everyone
            //Everyone reply
        }

        private void TriggerTimeout()
        {

        }

        private IRaftMessage CreateAppendEntryReply()
        {
            throw new NotImplementedException();
        }

        private IRaftMessage CreateRequestVoteReply()
        {
            throw new NotImplementedException();
        }
    }
}