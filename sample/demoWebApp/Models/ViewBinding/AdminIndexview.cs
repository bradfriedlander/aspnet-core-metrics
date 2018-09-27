using System.Collections.Generic;
using demoWebApp.Models.InputBinding;
using MagenicMetrics;

namespace demoWebApp.Models.ViewBinding
{
    /// <summary>
    ///     This is the view model for Admin Index.
    /// </summary>
    /// <seealso cref="demoWebApp.Models.InputBinding.AdminIndexInput" />
    public class AdminIndexView : AdminIndexInput
    {
        /// <summary>
        ///     Gets or sets the metrics.
        /// </summary>
        /// <value>This is the set of metric to display metrics.</value>
        public List<IMetric> Metrics { get; set; }
    }
}
