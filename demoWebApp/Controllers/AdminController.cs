using System.Linq;
using System.Threading.Tasks;
using demoWebApp.Models.InputBinding;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;

namespace demoWebApp.Controllers
{
    /// <summary>
    ///     This controllers is used to display the latest metrics.
    /// </summary>
    /// <seealso cref="BaseController" />
    public class AdminController : BaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AdminController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        /// <param name="metricService">The metric service.</param>
        public AdminController(IMetric metric, IMetricService metricService) : base(metric)
        {
            _metricService = metricService;
        }

        private readonly IMetricService _metricService;

        /// <summary>
        ///     Retrieve the latest <see cref="Metric" /> records. <paramref name="adminIndex.Count" /> limits the number to return.
        /// </summary>
        /// <param name="adminIndex">This is the input for this action.</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(AdminIndex adminIndex)
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            var metricList = await _metricService.GetLatest(adminIndex.Count ?? 25);
            ViewBag.Count = _metric.ResultCount = metricList.Count();
            return View(metricList);
        }
    }
}
