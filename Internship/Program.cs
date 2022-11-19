using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web;
using NLog;

namespace FinalProjectMyBlog
{
    public class Program
    {
        public static NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
             
        public static void Main(string[] args)
        {          
            try
            {
                Logger.Debug("������ ����������");
                CreateHostBuilder(args).Build().Run(); 
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "Stop programm because of exception");                
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }                 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
                .UseNLog();  // ���������� NLog
    }
}