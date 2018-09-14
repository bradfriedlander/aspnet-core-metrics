using System.Linq;
using System.Threading.Tasks;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;

namespace demoWebApp.Controllers
{
	public class AdminController : Controller
	{
		public AdminController(IMetric metric, IMetricService metricService)
		{
			_metric = metric;
			_metricService = metricService;
		}

		private readonly IMetric _metric;
		private readonly IMetricService _metricService;

		public async Task<IActionResult> Index(int count = 25)
		{
			var metricList = await _metricService.GetLatest(count);
			ViewBag.Count = _metric.ResultCount = metricList.Count();
			return View(metricList);
		}
	}
}
