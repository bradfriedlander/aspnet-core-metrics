﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
    /// <seealso cref="DbContext" />
    /// <seealso cref="IMetricService" />
    public class MetricService : DbContext, IMetricService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MetricService" /> class.
        /// </summary>
        /// <param name="options">These are the service configuration options.</param>
        /// <param name="logger">This is the logger.</param>
        /// <param name="contextOptions">This is the options for <see cref="DbContext" />.</param>
        public MetricService(IOptions<MetricServiceOptions> options, ILogger<MetricService> logger, DbContextOptions<MetricService> contextOptions) : base(contextOptions)
        {
            _logger = logger;
            _options = options.Value;
            _logger.LogDebug("'TableName' value is '{TableName}'.", options.Value.TableName);
            _logger.LogDebug("'MetricServiceConnection' value is '{MetricServiceConnection}'.", options.Value.MetricServiceConnection);
        }

        /// <summary>
        ///     Gets or sets the <see cref="DbSet{TEntity}" /> for <see cref="Metric" />.
        /// </summary>
        /// <value>This is the <see cref="DbSet{TEntity}" /> for <see cref="Metric" />.</value>
        public DbSet<Metric> Metrics { get; set; }

        /// <summary>
        ///     This is the logger instance passed to the constructor.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     This is the configuration options passed to the constructor.
        /// </summary>
        private readonly MetricServiceOptions _options;

        /// <summary>
        ///     This method adds an instance of <paramref name="metric" /> to the persistence store and commits the addition.
        /// </summary>
        /// <param name="metric">The metric to be added to the persistence store.</param>
        /// <returns>This is the number of entries written to the persistent store.</returns>
        public async Task<int> AddAndSave(IMetric metric)
        {
            try
            {
                Metrics.Add((Metric)metric);
                return await SaveChangesAsync();
            }
            catch (Exception efEx)
            {
                _logger.LogError(efEx, "Unable to persist '{metricObject}'.", JsonConvert.SerializeObject(metric));
                return 0;
            }
        }

        /// <summary>
        ///     This method gets the most recent <see cref="IMetric" /> records based on the provided filters.
        /// </summary>
        /// <param name="pageSize">This is the maximum number of records to retrieve for a given page.</param>
        /// <param name="pageNumber">This is the page number where <paramref name="pageSize" /> is the page size.</param>
        /// <param name="applicationFilter">This is the filter for the application name.</param>
        /// <returns>This is the matching collection of the records.</returns>
        /// <remarks>
        ///     <para>
        ///         This method support inclusive and exclusive filtering for <paramref name="applicationFilter" />. If <paramref
        ///         name="applicationFilter" /> begins with "!", the filtering is exclusive; otherwise, it is inclusive. In either case, the balance of
        ///         the filter value has an implicit wild card at the beginning and end.
        ///     </para>
        /// </remarks>
        public async Task<IQueryable<IMetric>> GetLatest(int pageSize, int pageNumber, string applicationFilter)
        {
            var skipCount = (pageNumber - 1) * pageSize;
            var isExclude = applicationFilter.StartsWith("!");
            var actualFilter = isExclude ? applicationFilter.Substring(1) : applicationFilter;
            var pageOfMetrics = await Metrics
                .OrderByDescending(m => m.StartTime)
                .Where(m => isExclude ? !m.Application.Contains(actualFilter) : m.Application.Contains(actualFilter))
                .Skip(skipCount)
                .Take(pageSize).ToListAsync();
            return pageOfMetrics.AsQueryable();
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from the entity types exposed in <see
        ///     cref="Microsoft.EntityFrameworkCore.DbSet{T}" /> properties on your derived context. The resulting model may be cached and re-used for
        ///     subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context. Databases (and other extensions) typically define extension methods on
        ///     this object that allow you to configure aspects of the model that are specific to a given database.
        /// </param>
        /// <remarks>
        ///     <para>
        ///         If a model is explicitly set on the options for this context (via <see
        ///         cref="DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />) then this method will not be run.
        ///     </para>
        ///     <para>
        ///         The name of the table containing the <see cref="Metric" /> records is define by the <see cref="MetricServiceOptions.TableName" />
        ///         in <see cref="_options" />.
        ///     </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Metric>().ToTable(_options.TableName);
            modelBuilder.Entity<Metric>().Property<string>(nameof(Metric.Application)).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Metric>().Property<string>(nameof(Metric.Query)).IsRequired();
            modelBuilder.Entity<Metric>().Property<string>(nameof(Metric.RequestMethod)).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<Metric>().Property<string>(nameof(Metric.RequestPath)).IsRequired();
            modelBuilder.Entity<Metric>().Property<string>(nameof(Metric.ServerName)).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Metric>().Property<string>(nameof(Metric.TraceId)).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Metric>().Property<string>(nameof(Metric.UserName)).HasMaxLength(64).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
