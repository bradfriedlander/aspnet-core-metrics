using System;
using System.Collections.Generic;
using System.Text;

namespace MagenicMetrics
{
	public interface IMetric
	{
		string Details { get; set; }

		string RequestPath { get; set; }

		int ResultCode { get; set; }

		DateTime StartTime { get; set; }

		string UserName { get; set; }
	}
}
