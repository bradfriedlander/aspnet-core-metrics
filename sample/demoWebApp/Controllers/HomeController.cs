using System;
using System.Diagnostics;
using System.Threading.Tasks;
using demoWebApp.Models;
using demoWebApp.Models.InputBinding;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using MagenicMetrics.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace demoWebApp.Controllers
{
    /// <summary>
    ///     This is the default controller for the application.
    /// </summary>
    /// <seealso cref="MetricsBaseController" />
    [Authorize]
    public class HomeController : MetricsBaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        public HomeController(IMetric metric) : base(metric)
        {
        }

        /// <summary>
        ///     This controller action shows the about information.
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _metric.ResultCount = 1;
            ViewData["Message"] = $"Your application description page.<br />Metric:<br />{JsonConvert.SerializeObject(_metric)}";
            return View();
        }

        /// <summary>
        ///     This controller action forces <see langword="abstract" /><see cref="NullReferenceException" />.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Abort failed</exception>
        public IActionResult BadCode()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            throw new NullReferenceException("Abort failed");
        }

        /// <summary>
        ///     This controller action forces a <see cref="ConflictResult" />.
        /// </summary>
        /// <returns></returns>
        public IActionResult BadResult()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return new ConflictResult();
        }

        /// <summary>
        ///     This controller action shows the contact information.
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _metric.ResultCount = 2;
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        ///     This controller action shows the error information.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        ///     This display the input form to forces the status code value.
        /// </summary>
        /// <returns></returns>
        public IActionResult ForceStatusCode()
        {
            var initialModel = new HomeForceStatusCode { };
            return View(initialModel);
        }

        /// <summary>
        ///     This action forces the status code to be set to <paramref name="model" />.
        /// </summary>
        /// <param name="model">This is the model containing status code.</param>
        /// <returns></returns>
        [HttpPost]
        [MetricDetails(Source = "model")]
        public IActionResult ForceStatusCode(HomeForceStatusCode model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _metric.ResultCount = 0;
            return StatusCode(model.Code);
        }

        /// <summary>
        ///     This is the default display for this controller.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _metric.ResultCount = 1;
            return View();
        }

        /// <summary>
        ///     This is used to logout the user from this application.
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        /// <summary>
        ///     This controller action shows the privacy information.
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _metric.ResultCount = 1;
            return View();
        }
    }
}
