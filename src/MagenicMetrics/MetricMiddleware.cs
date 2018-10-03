using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MagenicMetrics
{
    /// <summary>
    ///     This class implements persisting DevOps metric information to a persistent store.
    /// </summary>
    public class MetricMiddleware
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MetricMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next link in the middleware pipeline.</param>
        /// <param name="logger">This is the logger.</param>
        public MetricMiddleware(RequestDelegate next, ILogger<MetricMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        ///     Invokes the specified HTTP context.
        /// </summary>
        /// <param name="context">This is the HTTP context for the request traversing the middleware pipeline.</param>
        /// <param name="metric">This is an instance of the metrics that are persisted.</param>
        /// <param name="metricService">
        ///     This is the metric service. It is here (instead of being in the constructor) because middleware components can only have singleton
        ///     injected objects. This service must be scoped.
        /// </param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IMetric metric, IMetricService metricService)
        {
            var exceptionMessage = string.Empty;
            metric.UserName = context.User.Identity.Name ?? "Unknown";
            metric.RequestMethod = context.Request.HttpContext.Request.Method;
            var requestPath = metric.RequestPath = context.Request.Path;
            var query = metric.Query = context.Request.QueryString.Value;
            metric.TraceId = context.TraceIdentifier;
            var process = Process.GetCurrentProcess();
            metric.ServerName = process.MachineName;
            var assembly = AppDomain.CurrentDomain;
            metric.Application = assembly.FriendlyName;
            try
            {
                await _next(context);
            }
            catch (Exception genEx)
            {
                exceptionMessage = genEx.Message;
                throw;
            }
            finally
            {
                var endTime = DateTime.UtcNow;
                metric.ElapsedTime = Convert.ToInt32((endTime - metric.StartTime).TotalMilliseconds);
                if (!string.IsNullOrEmpty(exceptionMessage) && string.IsNullOrEmpty(metric.ExceptionMessage))
                {
                    metric.ExceptionMessage = exceptionMessage;
                }
                metric.ResultCode = context.Response.StatusCode;
                if (requestPath != metric.RequestPath)
                {
                    metric.RequestPath = $"{requestPath} => {metric.RequestPath}";
                }
                if (query != metric.Query)
                {
                    metric.Query = $"{query} => {metric.Query}";
                }
                try
                {
                    await metricService.AddAndSave(metric);
                }
                catch (Exception metricServiceEx)
                {
                    _logger.LogError(metricServiceEx, "Failed to save '{SerializeObject}'.", JsonConvert.SerializeObject(metric));
                }
            }
        }
    }
}
