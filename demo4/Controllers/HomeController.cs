using System;
using System.Diagnostics;
using demo4.Models;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace demo4.Controllers
{
	public class HomeController : Controller
	{
		public HomeController(IMetric metric)
		{
			_metric = metric;
		}

		private readonly IMetric _metric;

		public IActionResult About()
		{
			_metric.ResultCount = 1;
			ViewData["Message"] = $"Your application description page.<br />Metric:<br />{JsonConvert.SerializeObject(_metric)}";
			return View();
		}

		public IActionResult BadCode()
		{
			throw new NullReferenceException("Abort failed");
		}

		public IActionResult BadResult()
		{
			return StatusCode(401);
		}

		public IActionResult Contact()
		{
			_metric.ResultCount = 2;
			ViewData["Message"] = $"Your contact page.";

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult ForceStatusCode(int code=401)
		{
			return StatusCode(code);
		}

		public IActionResult Index()
		{
			_metric.ResultCount = 5;
			return View();
		}

		public IActionResult Privacy()
		{
			_metric.ResultCount = 0;
			return View();
		}
	}
}
