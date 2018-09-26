using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MagenicMetrics
{
    /// <summary>
    ///     This class contains the standard extension methods for an ASP.NET Core middleware service.
    /// </summary>
    public static class MetricMiddlewareExtensions
    {
        /// <summary>
        ///     This methods adds all classes used by <see cref="MetricMiddleware" /> to the service collection.
        /// </summary>
        /// <param name="services">This is the current services collection.</param>
        /// <returns>This is the updated services collection.</returns>
        public static IServiceCollection AddMetrics(this IServiceCollection services)
        {
            return services
                .AddScoped<IMetric, Metric>()
                .AddScoped<IMetricService, MetricService>();
        }

        /// <summary>
        ///     This method performs all of the actions needed to add <see cref="MetricMiddleware" /> to the HTTP request processing pipeline.
        /// </summary>
        /// <param name="app">This is the current application pipeline.</param>
        /// <returns>This is the updated HTTP request processing pipeline.</returns>
        public static IApplicationBuilder UseMetrics(this IApplicationBuilder app) => app.UseMiddleware<MetricMiddleware>();
    }
}
