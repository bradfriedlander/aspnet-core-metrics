using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MagenicMetrics
{
	public class MetricService : IMetricService
	{
		public MetricService(IOptions<MetricServiceOptions> options, ILogger<MetricService> logger)
		{
			_logger = logger;
			_options = options;
			_logger.LogDebug($"'TableName' value is ''{options.Value.TableName}.");
			_logger.LogDebug($"'MetricServiceConnection' value is '{options.Value.MetricServiceConnection}'.");
		}

		private readonly ILogger _logger;
		private readonly IOptions<MetricServiceOptions> _options;

		/// <summary>
		///     This method adds an instance of <paramref name="metric" /> to the persistence store and commits the addition.
		/// </summary>
		/// <param name="metric">The metric.</param>
		/// <exception cref="NotImplementedException"></exception>
		public void AddAndSave(IMetric metric)
		{
			_logger.LogCritical($"{nameof(AddAndSave)} has not been implemented.");
			throw new NotImplementedException();
		}
	}
}
