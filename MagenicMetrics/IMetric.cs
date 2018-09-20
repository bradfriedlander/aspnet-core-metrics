using System;

namespace MagenicMetrics
{
    /// <summary>
    ///     This is the information persisted for every MVC action.
    /// </summary>
    public interface IMetric
    {
        /// <summary>
        ///     Gets or sets the name of current application.
        /// </summary>
        /// <value>This is the name of the application.</value>
        string Application { set; get; }

        string Details { get; set; }

        int ElpasedTime { get; set; }

        /// <summary>
        ///     Gets or sets the exception message.
        /// </summary>
        /// <value>If an exception occurs, this is the exception message. Otherwise, it is a pipe delimited collection of validation messages.</value>
        string ExceptionMessage { get; set; }

        int MetricId { get; set; }

        string Query { get; set; }

        string RequestMethod { get; set; }

        string RequestPath { get; set; }

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
