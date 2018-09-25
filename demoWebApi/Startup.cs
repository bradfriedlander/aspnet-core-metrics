using demoWebApi.Models;
using demoWebApi.Services;
using MagenicMetrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace demoWebApi
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

        private HealthCheckStatus healthCheckStatus;

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.Map("/healthcheck", branch =>
            {
                branch.Run(context =>
                {
                    healthCheckStatus.AliveSeconds = Convert.ToInt32((DateTime.UtcNow - healthCheckStatus.StartTime).TotalSeconds);
                    var resultObject = JsonConvert.SerializeObject(healthCheckStatus);
                    var data = Encoding.UTF8.GetBytes(resultObject);
                    context.Response.ContentType = "application/json";
                    return context.Response.Body.WriteAsync(data, 0, data.Length);
                });
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Web API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseMetrics();
            app.UseMvc();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The existing collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            CreateHealthCheckStatusSingleton(services);
            services.Configure<MetricServiceOptions>(Configuration.GetSection("MetricServiceSettings"));
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddXmlSerializerFormatters();
            var metricConnectionString = Configuration.GetSection("MetricServiceSettings")["MetricServiceConnection"];
            services.AddDbContext<MetricService>((serviceProvider, options) => options.UseSqlServer(metricConnectionString, ob => ob.MigrationsAssembly("demo4")));
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("SimpleDatabase"));
            services.AddMetrics();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Demo API", Version = "v1" });
            });
        }

        /// <summary>
        ///     Creates the health check status singleton.
        /// </summary>
        /// <param name="services">The services.</param>
        private void CreateHealthCheckStatusSingleton(IServiceCollection services)
        {
            services.AddSingleton(new HealthCheckStatus(DateTime.UtcNow));
            var sp = services.BuildServiceProvider();
            healthCheckStatus = sp.GetService<HealthCheckStatus>();
        }
    }
}
