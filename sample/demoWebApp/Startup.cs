using demoWebApp.Services;
using MagenicMetrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace demoWebApp
{
    /// <summary>
    ///     This is the <see cref="Startup" /> class for the "demoWebApp" application.
    /// </summary>
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
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Note 1
            app.UseCookiePolicy(new CookiePolicyOptions { Secure = CookieSecurePolicy.Always });
            app.UseAuthentication();
            app.UseMetrics();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">This is the existing collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            ConfigureAuthentication(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            ConfigureMetrics(services);
        }

        /// <summary>
        ///     This method configures authentication for this application.
        /// </summary>
        /// <param name="services">This is the existing collection of services.</param>
        private void ConfigureAuthentication(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
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
