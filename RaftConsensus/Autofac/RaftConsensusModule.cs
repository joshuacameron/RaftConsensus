using Autofac;
using Microsoft.Extensions.Configuration;
using RaftConsensus.Consensus.Interfaces;
using RaftConsensus.Consensus.States;
using RaftConsensus.Consensus.States.Interfaces;
using RaftConsensus.Helpers;
using RaftConsensus.Helpers.Interfaces;
using RaftConsensus.MessageBroker;
using RaftConsensus.MessageBroker.Interfaces;
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

            builder.RegisterType<Waiter>().As<IWaiter>();
            builder.RegisterType<RaftConsensusStateFactory>().As<IRaftConsensusStateFactory>();
            builder.RegisterType<RaftMessageQueues>().As<IRaftMessageQueues>();
            builder.RegisterType<RaftConsensusContext>().As<IRaftConsensus>();
        }
    }
}
