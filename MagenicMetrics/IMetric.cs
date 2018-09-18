using System;

namespace MagenicMetrics
{
    public interface IMetric
    {
        /// <summary>
        ///     Gets or sets the name of current application.
        /// </summary>
        /// <value>This is the name of the application.</value>
        string Application { set; get; }

        string Details { get; set; }

        int ElpasedTime { get; set; }

        string ExceptionMessage { get; set; }

        int MetricId { get; set; }

        string Query { get; set; }

        string RequestPath { get; set; }

        string RequestMethod { get; set; }

        int ResultCode { get; set; }

        int ResultCount { get; set; }

        /// <summary>
        ///     Gets or sets the name of the server instance that responded to a request.
        /// </summary>
        /// <value>The name of the server instance.</value>
        string ServerName { get; set; }

        DateTime StartTime { get; set; }

        string TraceId { get; set; }

        string UserName { get; set; }
    }
}
