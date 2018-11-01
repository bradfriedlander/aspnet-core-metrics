using MagenicMetrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace demoWebReact
{
    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">This is the environment.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(env.ContentRootPath)
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        ///     This is the configuration for the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">This is the application builder.</param>
        /// <param name="env">This is the hosting environment.</param>
        /// <remarks>
        ///     <para>
        ///         Note 1: Per "https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-2.1&amp;tabs=aspnetcore2x"
        ///         the default <see cref="CookiePolicyOptions" /> for <see cref="CookieSecurePolicy" /> is <see
        ///         cref="CookieSecurePolicy.SameAsRequest" />. Using <see cref="CookieSecurePolicy.Always" /> is more secure and works if the site
        ///         only allows HTTPS requests.
        ///     </para>
        /// </remarks>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true,
                //    ReactHotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMetrics();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">This is the existing collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            ConfigureMetrics(services);
        }

        /// <summary>
        ///     This method configures the metric middleware and its supporting services.
        /// </summary>
        /// <param name="services">This is the existing collection of services.</param>
        private void ConfigureMetrics(IServiceCollection services)
        {
            var metricServiceSettings = Configuration.GetSection("MetricServiceSettings");
            var metricConnectionString = metricServiceSettings["MetricServiceConnection"];
            services
                .Configure<MetricServiceOptions>(metricServiceSettings)
                .AddDbContext<MetricService>((serviceProvider, options) => options.UseSqlServer(metricConnectionString, ob => ob.MigrationsAssembly("demoMetrics")))
                .AddMetrics();
        }
    }
}
