using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NetProOA.Framework.ApolloConfiguration;
using NetProOA.Framework.Authorization.Interceptor;
using NetProOA.Framework.Ioc;

namespace NetProOA.TemplateE.DbMigrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nlogger = NLoggerFactory.InitNLogger(NLoggerType.Local);
            var logger = nlogger.GetCurrentClassLogger();
            try
            {
                logger.Info("start web");
                CreateHostBuilder(args).Build().Run();
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
                         .AddJsonFile($"Config/apollo.{environmentName}.json", optional: false, reloadOnChange: true);
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
                     .ConfigureLogging((builderContext, loggingBuilder) =>
                     {
                         loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                     })
                    .UseServiceProviderFactory(new CustomAutofacServiceProviderFactory())//Aop ע�뵱ǰ�û�
                    .UseNLog();
    }
}
