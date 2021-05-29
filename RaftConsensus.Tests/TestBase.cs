using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RaftConsensus.Autofac;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System.IO;

namespace RaftConsensus.Tests
{
    public class TestBase
    {
        protected readonly IContainer Container;

        protected TestBase()
        {
            var configuration = GetConfiguration();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(@"test-log.txt", LogEventLevel.Debug, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = new ContainerBuilder();

            builder.RegisterModule(new RaftConsensusModule(configuration));

            builder.Register(_ => new LoggerFactory(new ILoggerProvider[] { new SerilogLoggerProvider() }))
                .As<ILoggerFactory>()
                .SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();
            
            Container = builder.Build();
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
