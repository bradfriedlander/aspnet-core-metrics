﻿using System;
using System.Text;
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

namespace demoWebApi
{
    /// <summary>
    ///     This is the ASP.NET Core <see cref="IStartup" /> implementation for this application.
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
        ///     This is the singleton instance of the health check status
        /// </summary>
        private IHealthCheckStatus healthCheckStatus;

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
            app.UseHttpsRedirection();
            app.UseMetrics();
            app.Map("/healthcheck", branch =>
            {
                branch.Run(context =>
                {
                    healthCheckStatus.UpdateAliveSeconds();
                    var metric = context.RequestServices.GetService<IMetric>();
                    metric.Details = JsonConvert.SerializeObject(healthCheckStatus);
                    metric.ResultCount = 1;
                    context.Response.ContentType = "application/json";
                    var data = Encoding.UTF8.GetBytes(metric.Details);
                    return context.Response.Body.WriteAsync(data, 0, data.Length);
                });
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Web API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The existing collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            CreateHealthCheckStatusSingleton(services);
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddXmlSerializerFormatters();
            ConfigureMetrics(services);
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("SimpleDatabase"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Demo API",
                        Version = "v1",
                        Contact = new Contact
                        {
                            Name = "Brad Friedlander",
                            Email = "bradf@magenic.com"
                        }
                    });
                c.IncludeXmlComments(GetXmlCommentsPath(), includeControllerXmlComments: true);
            });
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

        /// <summary>
        ///     Creates the health check status singleton.
        /// </summary>
        /// <param name="services">The services.</param>
        private void CreateHealthCheckStatusSingleton(IServiceCollection services)
        {
            services.AddSingleton<IHealthCheckStatus>(new HealthCheckStatus(DateTime.UtcNow));
            var sp = services.BuildServiceProvider();
            healthCheckStatus = sp.GetService<IHealthCheckStatus>();
        }

        /// <summary>
        ///     This method gets the absolute pathname to the XML comments.
        /// </summary>
        /// <returns>This is the XMl comment pathname.</returns>
        private string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\demoWebApi.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
