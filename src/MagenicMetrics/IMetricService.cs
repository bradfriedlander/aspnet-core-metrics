using System.Linq;
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
        ///     This method gets the most recent <see cref="IMetric" /> records based on the provided filters.
        /// </summary>
        /// <param name="pageSize">This is the maximum number of records to retrieve for a given page.</param>
        /// <param name="pageNumber">This is the page number where <paramref name="pageSize" /> is the page size.</param>
        /// <param name="applicationFilter">This is the filter for the application name.</param>
        /// <returns>This is the matching collection of the records.</returns>
        Task<IQueryable<IMetric>> GetLatest(int pageSize, int pageNumber, string applicationFilter);
    }
}
