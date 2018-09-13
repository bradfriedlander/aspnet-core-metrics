using System;

namespace MagenicMetrics
{
	public class Metric : IMetric
	{
		/// <summary>
		///     Gets or sets the name of current application.
		/// </summary>
		/// <value>This is the name of the application.</value>
		public string Application { get; set; }

		public string Details { get; set; }

		public int ElpasedTime { get; set; }

		public string ExceptionMessage { get; set; }

		public int MetricId { get; set; }

		public string RequestPath { get; set; }

		public int ResultCode { get; set; }

		public int ResultCount { get; set; }

		/// <summary>
		///     Gets or sets the name of the server instance that responded to a request.
		/// </summary>
		/// <value>The name of the server instance.</value>
		public string ServerName { get; set; }

		public DateTime StartTime { get; set; }

		public string UserName { get; set; }
	}
}
