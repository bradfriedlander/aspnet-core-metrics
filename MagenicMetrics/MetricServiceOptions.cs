namespace MagenicMetrics
{
	public class MetricServiceOptions

	{
		/// <summary>
		///     Gets or sets the connection string for the database containing the <see cref="TableName" /> table.
		/// </summary>
		/// <value>The connection string.</value>
		public string MetricServiceConnection { get; set; }

		/// <summary>
		///     Gets or sets the name of the table that contains the properties for <see cref="IMetric" />.
		/// </summary>
		/// <value>This is the name of the table.</value>
		public string TableName { get; set; }
	}
}
