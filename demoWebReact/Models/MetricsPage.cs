using System.Collections.Generic;
using demoWebReact.Models;
using MagenicMetrics;

namespace demoWebReact.Models
{
    /// <summary>
    ///     This is the view model for Admin Index.
    /// </summary>
    /// <seealso cref="AdminIndexInput" />
    public class MetricsPage : MetricsQueryModel
    {
        /// <summary>
        ///     Gets or sets the metrics.
        /// </summary>
        /// <value>This is the set of metric to display metrics.</value>
        public List<IMetric> Metrics { get; set; }
    }
}
