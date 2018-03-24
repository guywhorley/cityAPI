using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace CityInfo.API
{
	public class Program
    {
        public static void Main(string[] args)
        {
	        // NLog: setup the logger first to catch all errors
	        var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

	        logger.Debug("Initialize main");
            BuildWebHost(args)
				.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
	            .ConfigureLogging(logging =>
	            {
		            logging.ClearProviders();
		            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	            })
	            .UseNLog()  // NLog: setup NLog for Dependency injection
				.Build();
    }
}
