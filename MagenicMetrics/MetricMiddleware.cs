using System;
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
		/// <param name="httpContext">This is the HTTP context for the request traversing the middleware pipeline.</param>
		/// <param name="metric">This is an instance of the metrics that are persisted.</param>
		/// <param name="metricService">
		///     This is the metric service. It is here (instead of being in the constructor) because middleware components can only have singleton
		///     injected objects. This service must be scoped.
		/// </param>
		/// <returns></returns>
		public async Task Invoke(HttpContext httpContext, IMetric metric, IMetricService metricService)
		{
			var exceptionMessage = string.Empty;
			metric.StartTime = DateTime.UtcNow;
			metric.UserName = httpContext.User.Identity.Name ?? "Unknown";
			metric.RequestPath = httpContext.Request.Path;
			metric.ServerName = "Unknown";
			metric.Application = AppDomain.CurrentDomain.FriendlyName;
			try
			{
				await _next(httpContext);
			}
			catch (Exception genEx)
			{
				exceptionMessage = genEx.Message;
				throw;
			}
			finally
			{
				if (!string.IsNullOrEmpty(exceptionMessage) && string.IsNullOrEmpty(metric.ExceptionMessage))
				{
					metric.ExceptionMessage = exceptionMessage;
				}
				var endTime = DateTime.UtcNow;
				metric.ElpasedTime = Convert.ToInt32((endTime - metric.StartTime).TotalMilliseconds);
				metric.ResultCode = httpContext.Response.StatusCode;
				//_logger.LogInformation(JsonConvert.SerializeObject(metric));
				try
				{
					metricService.AddAndSave(metric);
				}
				catch (Exception metricServiceEx)
				{
					_logger.LogError(metricServiceEx, $"Failed to save '{JsonConvert.SerializeObject(metric)}'.");
				}
			}
		}
	}
}
