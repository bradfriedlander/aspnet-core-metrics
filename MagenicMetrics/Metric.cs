using System;

namespace MagenicMetrics
{
	public class Metric : IMetric
	{
		public string Details { get; set; }

		public int ElpasedTime { get; set; }

		public string ExceptionMessage { get; set; }

		public string RequestPath { get; set; }

		public int ResultCode { get; set; }

		public DateTime StartTime { get; set; }

		public string UserName { get; set; }
	}
}
