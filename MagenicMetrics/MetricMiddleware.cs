using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MagenicMetrics
{
	public class MetricMiddleware
	{
		public MetricMiddleware(RequestDelegate next, ILogger<MetricMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public async Task Invoke(HttpContext httpContext)
		{
			var startTime = DateTime.UtcNow;
			var userName = httpContext.User.Identity.Name ?? "Unknown";
			var requestPath = httpContext.Request.Path;
			await _next(httpContext);
			var endTime = DateTime.UtcNow;
			var elpasedTime = Convert.ToInt32((endTime - startTime).TotalMilliseconds);
			_logger.LogInformation($"User='{userName}', Path='{requestPath}', Start Time={startTime:yyy-y-MM-dd HH:mm:ss.000}, Elapsed Milliseconds={elpasedTime:#,##0}");
		}
	}
}
