using Microsoft.AspNetCore.Builder;

namespace MagenicMetrics
{
	public static class MetricMiddlewareExtensions
	{
		public static IApplicationBuilder UseMetrics(this IApplicationBuilder app)
		{
			return app.UseMiddleware<MetricMiddleware>();
		}
	}
}
