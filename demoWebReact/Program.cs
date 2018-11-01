using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace demoWebReact
{
    public class Program
    {
        /// <summary>
        ///     Creates the web host builder.
        /// </summary>
        /// <param name="args">These are the command line arguments.</param>
        /// <param name="hostingConfig">This is the hosting configuration.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot hostingConfig)
        {
            var fileName = hostingConfig["Https:File"];
            var password = hostingConfig["Https:Password"];
            var certificate = new X509Certificate2(fileName, password);
            return WebHost.CreateDefaultBuilder(args)
                    .UseKestrel(options => options.ConfigureHttpsDefaults(configureOptions => configureOptions.ServerCertificate = certificate))
                    .UseStartup<Startup>();
        }

        /// <summary>
        ///     This is the entry point for the application.
        /// </summary>
        /// <param name="args">This is the command line arguments.</param>
        public static void Main(string[] args)
        {
            var hostingConfig = GetHostingConfiguration(args);
            CreateWebHostBuilder(args, hostingConfig)
                .Build()
                .Run();
        }

        /// <summary>
        ///     Gets the hosting configuration.
        /// </summary>
        /// <returns>This is the hosting configuration information.</returns>
        private static IConfigurationRoot GetHostingConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .AddUserSecrets<Startup>()
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();
        }
    }
}
