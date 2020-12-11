using Autofac;
using Microsoft.Extensions.Configuration;
using RaftConsensus.Autofac;
using System.IO;

namespace RaftConsensus.Tests
{
    public class TestBase
    {
        protected readonly IContainer Container;

        protected TestBase()
        {
            var configuration = GetConfiguration();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new RaftConsensusModule(configuration));

            Container = containerBuilder.Build();
        }

        private static IConfiguration GetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            var currentDirectory = Directory.GetCurrentDirectory();

            return configurationBuilder.SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }
    }
}
