using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace demoWebApi
{
    /// <summary>
    ///     This is the starting class for the WebApi demo.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        /// <summary>
        ///     This is the entry point for the application.
        /// </summary>
        /// <param name="args">This is the command line arguments.</param>
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();
    }
}
