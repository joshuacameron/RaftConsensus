using Autofac;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
using RaftConsensus.Common.Settings;
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
        }
    }
}
