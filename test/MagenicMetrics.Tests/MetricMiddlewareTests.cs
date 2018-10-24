using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace MagenicMetrics.Tests
{
    public class MetricMiddlewareTests
    {
        [Fact]
        public async void HandlesExceptions()
        {
            const string exceptionMessage = "'xyzzy' was null.";
            const string RequestPath = "/Home/Index";
            var metric = new Metric();
            var context = new DefaultHttpContext()
            {
                TraceIdentifier = "XYZZY"
            };
            context.Request.Path = RequestPath;
            RequestDelegate next = (ctx) =>
            {
                throw new NullReferenceException(exceptionMessage);
            };
            var metricService = new Mock<IMetricService>();
            metricService.Setup((ms) => ms.AddAndSave(metric)).Returns(Task.FromResult(1));
            var middleware = new MetricMiddleware(next: next, logger: null);
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await middleware.Invoke(context: context, metric: metric, metricService: metricService.Object);
            });
            Assert.Equal("Unknown", metric.UserName);
            Assert.Equal(exceptionMessage, metric.ExceptionMessage);
            Assert.Equal(RequestPath, metric.RequestPath);
            Assert.Equal(200, metric.ResultCode);
        }
    }
}
