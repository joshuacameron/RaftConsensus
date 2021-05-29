using Autofac;
using Microsoft.Extensions.Configuration;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Consensus.States;
using RaftConsensus.PeerManagement;
using RaftConsensus.PeerManagement.Interfaces;
using RaftConsensus.Settings;
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
