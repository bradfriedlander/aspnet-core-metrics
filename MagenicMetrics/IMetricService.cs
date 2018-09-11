namespace MagenicMetrics
{
	/// <summary>
	///     This service provides the persistence for <see cref="IMetric" /> objects.
	/// </summary>
	/// <remarks>
	///     <para>
	///         The application that uses this is responsible for providing the database that will serve as the persistence store and a table that
	///         contains columns for all of the properties defined in <see cref="IMetric" />.
	///     </para>
	/// </remarks>
	public interface IMetricService
	{
		/// <summary>
		///     This method adds an instance of <paramref name="metric" /> to the persistence store and commits the addition.
		/// </summary>
		/// <param name="metric">The metric.</param>
		void AddAndSave(IMetric metric);
	}
}
