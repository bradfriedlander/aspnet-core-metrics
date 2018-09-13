using System.Collections.Generic;
using System.Threading.Tasks;

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
		/// <param name="metric">The metric to be added to the persistence store.</param>
		/// <returns>This is the number of entries written to the persistent store.</returns>
		Task<int> AddAndSave(IMetric metric);

		/// <summary>
		///     this method gets the <paramref name="count" /> most recent <see cref="IMetric" /> records .
		/// </summary>
		/// <param name="count">This is the count of records to retrieve.</param>
		/// <returns>This is a collection of the records.</returns>
		Task<IEnumerable<IMetric>> GetLatest(int count);
	}
}
