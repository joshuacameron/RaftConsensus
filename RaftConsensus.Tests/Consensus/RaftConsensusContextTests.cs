using Autofac;
using RaftConsensus.Consensus.Interfaces;
using Xunit;

namespace RaftConsensus.Tests.Consensus
{
    public class RaftConsensusContextTests : TestBase
    {
        private IRaftConsensus _node;

        public RaftConsensusContextTests()
        {
            _node = Container.Resolve<IRaftConsensus>();
        }
        
        [Fact]
        public void TestRaftConsensusInstantiates()
        {

        }
    }
}