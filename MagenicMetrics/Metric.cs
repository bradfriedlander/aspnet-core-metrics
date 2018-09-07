using System;

namespace MagenicMetrics
{
	public class Metric
	{
		public string Details { get; set; }

		public string RequestPath { get; set; }

		public int ResultCode { get; set; }

		public DateTime StartTime { get; set; }

		public string UserName { get; set; }
	}
}
