using System;
using System.Diagnostics;
using demoWebApp.Models;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace demoWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMetric metric) : base(metric)
        {
        }

        public IActionResult About()
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            _metric.ResultCount = 1;
            ViewData["Message"] = $"Your application description page.<br />Metric:<br />{JsonConvert.SerializeObject(_metric)}";
            return View();
        }

        public IActionResult BadCode()
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            throw new NullReferenceException("Abort failed");
        }

        public IActionResult BadResult()
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            return StatusCode(401);
        }

        public IActionResult Contact()
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            _metric.ResultCount = 2;
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ForceStatusCode(int code = 401)
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            return StatusCode(code);
        }

        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            _metric.ResultCount = 5;
            return View();
        }

        public IActionResult Privacy()
        {
            if (!ModelState.IsValid)
            {
                _metric.ResultCount = -1;
                _metric.ExceptionMessage = GetValidationErrors();
                return View();
            }
            _metric.ResultCount = 0;
            return View();
        }
    }
}
