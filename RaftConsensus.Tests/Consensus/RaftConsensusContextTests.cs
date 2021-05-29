using Autofac;
using RaftConsensus.Consensus.Interfaces;
using Xunit;

namespace RaftConsensus.Tests.Consensus
{
    public class RaftConsensusContextTests : TestBase
    {
        [Fact]
        public void TestRaftConsensusInstantiates()
        {
            var raf = Container.Resolve<IRaftConsensus>();
        }
    }
}
