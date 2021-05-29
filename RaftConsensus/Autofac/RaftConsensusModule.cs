using Autofac;
using Microsoft.Extensions.Configuration;
using RaftConsensus.Common.Consensus.Interfaces;
using RaftConsensus.Common.PeerManagement.Interfaces;
using RaftConsensus.Common.Settings;
using RaftConsensus.Consensus;
using RaftConsensus.PeerManagement;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace RaftConsensus.Autofac
{
    public class RaftConsensusModule : Module
    {
        private readonly IConfiguration _configuration;

        public RaftConsensusModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Any())
                .AsImplementedInterfaces();

            builder.RegisterInstance(_configuration.GetSection("RaftConsensusStateSettings").Get<RaftConsensusStateSettings>());

            builder.RegisterType<RaftPeerManagement>().As<IRaftPeerManagement>();
            builder.RegisterType<RaftConsensusContext>().As<IRaftConsensus>();
        }
    }
}
