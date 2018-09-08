using System;

namespace MagenicMetrics
{
	public interface IMetric
	{
		string Details { get; set; }

		int ElpasedTime { get; set; }

		string ExceptionMessage { get; set; }

		string RequestPath { get; set; }

		int ResultCode { get; set; }

		int ResultCount { get; set; }

		DateTime StartTime { get; set; }

		string UserName { get; set; }
	}
}
