using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;

namespace demoWebApp
{
    /// <summary>
    ///     This is the starting class for the WebApp demo.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var hostingConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables()
                .Build();
            return WebHost.CreateDefaultBuilder(args)
                    .UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Loopback, 5100);
                        //options.Listen(IPAddress.Loopback, 5101, listenOptions => { listenOptions.UseHttps("localhost.pfx", "magenic"); });
                        options.Listen(IPAddress.Loopback, 5101, listenOptions => { listenOptions.UseHttps(hostingConfig["Https:File"], hostingConfig["Https:Password"]); });
                    })
                    .UseStartup<Startup>();
        }

        /// <summary>
        ///     This is the entry point for the application.
        /// </summary>
        /// <param name="args">This is the command line arguments.</param>
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();
    }
}
