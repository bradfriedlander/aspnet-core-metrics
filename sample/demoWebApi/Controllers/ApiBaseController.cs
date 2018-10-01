using demoWebApi.Models;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace demoWebApi.Controllers
{
    /// <summary>
    ///     This is the base class for all API controllers.
    /// </summary>
    /// <remarks>
    ///     <para>This class defines the standard route prefix ("api/[controller]") for all API controllers.</para>
    /// </remarks>
    /// <seealso cref="MetricsBaseController" />
    [Route("api/[controller]")]
    public class ApiBaseController : MetricsBaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiBaseController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        /// <param name="healthCheckStatus">The health check status.</param>
        public ApiBaseController(IMetric metric, IHealthCheckStatus healthCheckStatus) : base(metric)
        {
            healthCheckStatus.IncrementRequestsProcessed();
        }
    }
}
