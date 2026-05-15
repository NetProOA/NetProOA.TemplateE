using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog.Web;
using NetProOA.Framework.ApolloConfiguration;
using NetProOA.Framework.Authorization.Interceptor;
using NetProOA.Framework.ConsulConfiguration;
using NetProOA.Framework.Ioc;
using NetProOA.Framework.NacosConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NetProOA.TemplateE.Api.Test.Infrastructure
{
    public class TestInitialize
    {
        [AssemblyInitialize]
        public static ILifetimeScope Initialize<T>(string environment) where T : class
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", environment);
            Environment.SetEnvironmentVariable("Environment", environment);
            Environment.SetEnvironmentVariable("NewLine", "\r\n");
            var builder = CreateWebHostBuilder<T>(new string[] { });
            var lifetimeScope = (builder.Build().Services as AutofacServiceProvider).LifetimeScope;
            return lifetimeScope;
        }
        private const NLoggerType N_LOGGER_TYPE = NLoggerType.Local;
        public static IHostBuilder CreateWebHostBuilder<T>(string[] args) where T : class
        {
            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                var configurationBuilder = config.SetBasePath(Directory.GetCurrentDirectory());
                configurationBuilder.AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Config/appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Config/nacos.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Config/apollo.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Config/consul.{environmentName}.json", optional: false, reloadOnChange: true);
                if (args != null)
                {
                    config.AddCommandLine(args);
                };
            });
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<T>();
            })
            .ConfigureAppConfiguration(new CustomApolloConfigurationProvider("Apollo").ConfigureDelegate)
            .ConfigureAppConfiguration(new CustomNacosConfigurationProvider("Nacos").ConfigureDelegate)
            .ConfigureAppConfiguration(new ConsulConfigurationProvider("ConsulConfig").ConfigureDelegate)
            .ConfigureLogging((builderContext, loggingBuilder) =>
            {
                NLoggerFactory.InitNLogger(N_LOGGER_TYPE);
                loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
            })
            .UseServiceProviderFactory(new CustomAutofacServiceProviderFactory())//Aop 注入当前用户
            .UseNLog();
            NLoggerFactory.InitNLogger(N_LOGGER_TYPE);
            return builder;
        }
    }
}
