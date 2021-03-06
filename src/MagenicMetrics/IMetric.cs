﻿using System;

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

        /// <summary>
        ///     Gets or sets the action provided details.
        /// </summary>
        /// <value>This is the details. It should be a serialized JSOB object.</value>
        string Details { get; set; }

        /// <summary>
        ///     Gets or sets the elapsed time in milliseconds.
        /// </summary>
        /// <value>This is the elapsed time for the request.</value>
        int ElapsedTime { get; set; }

        /// <summary>
        ///     Gets or sets the exception message.
        /// </summary>
        /// <value>If an exception occurs, this is the exception message. Otherwise, it is a pipe delimited collection of validation messages.</value>
        string ExceptionMessage { get; set; }

        /// <summary>
        ///     Gets or sets the identity identifier for the metric instance.
        /// </summary>
        /// <value>This is the identity identifier for the metric.</value>
        int MetricId { get; set; }

        /// <summary>
        ///     Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        string Query { get; set; }

        /// <summary>
        ///     Gets or sets the request method.
        /// </summary>
        /// <value>The request method.</value>
        string RequestMethod { get; set; }

        /// <summary>
        ///     Gets or sets the request path.
        /// </summary>
        /// <value>The request path.</value>
        string RequestPath { get; set; }

        /// <summary>
        ///     Gets or sets the result code.
        /// </summary>
        /// <value>The result code.</value>
        int ResultCode { get; set; }

        /// <summary>
        ///     Gets or sets the result count.
        /// </summary>
        /// <value>This is the result count. It should be set to the number of items processed by the request.</value>
        int ResultCount { get; set; }

        /// <summary>
        ///     Gets or sets the name of the server instance that responded to a request.
        /// </summary>
        /// <value>The name of the server instance.</value>
        string ServerName { get; set; }

        /// <summary>
        ///     Gets or sets the UTC start time.
        /// </summary>
        /// <value>This is the UTC start time.</value>
        DateTime StartTime { get; set; }

        /// <summary>
        ///     Gets or sets the trace identifier.
        /// </summary>
        /// <value>The trace identifier.</value>
        string TraceId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        string UserName { get; set; }
    }
}
