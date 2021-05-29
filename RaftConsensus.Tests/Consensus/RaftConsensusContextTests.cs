using Autofac;
using RaftConsensus.Common.Consensus.Interfaces;
using Serilog;
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
