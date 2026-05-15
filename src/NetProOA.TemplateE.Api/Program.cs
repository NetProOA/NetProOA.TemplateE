using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NetProOA.Framework.ApolloConfiguration;
using NetProOA.Framework.Authorization.Interceptor;
using NetProOA.Framework.CommandLine;
using NetProOA.Framework.ConsulConfiguration;
using NetProOA.Framework.Ioc;
using NetProOA.Framework.NacosConfiguration;
using NetProOA.TemplateE.Api;
using NetProOA.TemplateE.Infrastructure.Autofac.Extensions;

namespace NetProOA.TemplateE.Web
{
    public class Program
    {
        private const NLoggerType N_LOGGER_TYPE = NLoggerType.Local;
        public static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(200, 200);
            CommandLineCommandRegister.Instance().Run(args);
            var nlogger = NLoggerFactory.InitNLogger(N_LOGGER_TYPE);
            var logger = nlogger.GetCurrentClassLogger();
            try
            {
                logger.Info("start web");
                CreateHostBuilder(args).Build()
                    .Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                nlogger.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args)
                      .ConfigureAppConfiguration((hostingContext, config) =>
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
                      })
                      .UseCustomMetrics()
                      .ConfigureWebHostDefaults(webBuilder =>
                      {
                          webBuilder.UseStartup<Startup>();
                      })
                      .ConfigureAppConfiguration(new CustomApolloConfigurationProvider("Apollo").ConfigureDelegate)
                      .ConfigureAppConfiguration(new CustomNacosConfigurationProvider("Nacos").ConfigureDelegate)
                      .ConfigureAppConfiguration(new ConsulConfigurationProvider("ConsulConfig").ConfigureDelegate)
                      .ConfigureLogging((builderContext, loggingBuilder) =>
                      {
                          NLoggerFactory.InitNLogger(N_LOGGER_TYPE);
                          loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                      })
                     .UseServiceProviderFactory(new CustomAutofacServiceProviderFactory())
                     .UseNLog();
    }
}
