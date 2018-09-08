using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MagenicMetrics
{
	public class MetricMiddleware
	{
		public MetricMiddleware(RequestDelegate next, ILogger<MetricMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		private readonly ILogger _logger;
		private readonly RequestDelegate _next;

		public async Task Invoke(HttpContext httpContext, IMetric metric)
		{
			var exceptionMessage = string.Empty;
			metric.StartTime = DateTime.UtcNow;
			metric.UserName = httpContext.User.Identity.Name ?? "Unknown";
			metric.RequestPath = httpContext.Request.Path;
			httpContext.Items["metric"] = JsonConvert.SerializeObject(metric);
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
				var resultMetric = httpContext.Items["metric"]?.ToString() ?? "";
				if (!string.IsNullOrEmpty(resultMetric))
				{
					metric = JsonConvert.DeserializeObject<Metric>(resultMetric);
				}
				if (!string.IsNullOrEmpty(exceptionMessage) && string.IsNullOrEmpty(metric.ExceptionMessage))
				{
					metric.ExceptionMessage = exceptionMessage;
				}
				var endTime = DateTime.UtcNow;
				metric.ElpasedTime = Convert.ToInt32((endTime - metric.StartTime).TotalMilliseconds);
				metric.ResultCode = httpContext.Response.StatusCode;
				_logger.LogInformation(JsonConvert.SerializeObject(metric));
			}
		}
	}
}
