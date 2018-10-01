using demoWebApp.HttpHelpers;
using demoWebApp.Models.ViewBinding;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demoWebApp.Controllers
{
    /// <summary>
    ///     This controller manages the creation, editing, and deletion of definitions.
    /// </summary>
    /// <seealso cref="MetricsBaseController" />
    [Authorize]
    public class DefinitionController : MetricsBaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefinitionController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        /// <param name="logger">The logger.</param>
        public DefinitionController(IMetric metric, ILogger<DefinitionController> logger/*, IOptions<DefinitionServiceSettings> options*/) : base(metric)
        {
            _logger = logger;
            //baseUri = $"{options.Value.BaseUrl}/api/values";
            definitions = new List<DefinitionIndexView>();
        }

        private static List<DefinitionIndexView> definitions;

        private readonly ILogger _logger;

        // TODO: Get this from configuration - use options
        private readonly string baseUri = "https://localhost:5001/api/values";

        /// <summary>
        ///     Creates new <see cref="DefinitionIndexView" />.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            _metric.ResultCount = 0;
            return View(new DefinitionIndexView());
        }

        /// <summary>
        ///     Creates the specified new definition.
        /// </summary>
        /// <param name="newDefinition">The new definition.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DefinitionIndexView newDefinition)
        {
            SetMetricDetails(newDefinition);
            if (!ModelState.IsValid)
            {
                return View(newDefinition);
            }
            try
            {
                newDefinition.DefinitionId = 1;
                var requestUri = $"{baseUri}";
                var response = await HttpRequestFactory.Post(requestUri, newDefinition);
                if (!response.IsSuccessStatusCode)
                {
                    _metric.ResultCode = (int)response.StatusCode;
                    ModelState.AddModelError("", $"Could not create new definition, status code: '{response.StatusCode}'.");
                    return View(newDefinition);
                }
                _metric.ResultCount = 1;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return View(newDefinition);
            }
        }

        /// <summary>
        ///     Deletes the specified <see cref="DefinitionIndexView" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var requestUri = $"{baseUri}/{id}";
                var response = await HttpRequestFactory.Delete(requestUri);
                if (!response.IsSuccessStatusCode)
                {
                    _metric.ResultCode = (int)response.StatusCode;
                    ModelState.AddModelError("", $"Could not delete definition {id}, status code: '{response.StatusCode}'.");
                    return RedirectToAction(nameof(Index));
                }
                _metric.ResultCount = 1;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                return BadRequest(genEx.Message);
            }
        }

        /// <summary>
        ///     Shows the details of the specified <see cref="DefinitionIndexView" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            var requestUri = $"{baseUri}/{id}";
            var response = await HttpRequestFactory.Get(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                _metric.ResultCode = (int)response.StatusCode;
                ModelState.AddModelError("", $"Could not get definition {id}, status code: '{response.StatusCode}'.");
                return View(new DefinitionIndexView());
            }
            var outputModel = response.ContentAsType<DefinitionIndexView>();
            _metric.ResultCount = 1;
            return View(outputModel);
        }

        /// <summary>
        ///     Edits the specified <see cref="DefinitionIndexView" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var requestUri = $"{baseUri}/{id}";
            var response = await HttpRequestFactory.Get(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                _metric.ResultCode = (int)response.StatusCode;
                ModelState.AddModelError("", $"Could not get definition {id}, status code: '{response.StatusCode}'.");
                return View(new DefinitionIndexView());
            }
            var outputModel = response.ContentAsType<DefinitionIndexView>();
            _metric.ResultCount = 1;
            return View(outputModel);
        }

        /// <summary>
        ///     Edits the specified <see cref="DefinitionIndexView" />.
        /// </summary>
        /// <param name="definition">The definition.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DefinitionIndexView definition)
        {
            try
            {
                SetMetricDetails(definition);
                if (!ModelState.IsValid)
                {
                    return View(definition);
                }
                var requestUri = $"{baseUri}";
                var response = await HttpRequestFactory.Put(requestUri, definition);
                if (!response.IsSuccessStatusCode)
                {
                    _metric.ResultCode = (int)response.StatusCode;
                    ModelState.AddModelError("", $"Could not edit definition {definition.DefinitionId}, status code: '{response.StatusCode}'.");
                    return View(definitions);
                }
                _metric.ResultCount = 1;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return View();
            }
        }

        /// <summary>
        ///     Get all <see cref="DefinitionIndexView" /> records.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var requestUri = $"{baseUri}/GetAll";
            var response = await HttpRequestFactory.Get(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                _metric.ResultCode = (int)response.StatusCode;
                ModelState.AddModelError("", $"Could not retrieve definitions, status code: '{response.StatusCode}'.");
                return View(definitions);
            }
            definitions = response.ContentAsType<List<DefinitionIndexView>>();
            _metric.ResultCount = definitions.Count;
            return View(definitions);
        }

        /// <summary>
        ///     Undeletes the specified <see cref="DefinitionIndexView" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> Undelete(int id)
        {
            try
            {
                var requestUri = $"{baseUri}/Undelete?id={id}";
                var response = await HttpRequestFactory.Put(requestUri, string.Empty);
                if (!response.IsSuccessStatusCode)
                {
                    _metric.ResultCode = (int)response.StatusCode;
                    ModelState.AddModelError("", $"Could not undelete definition {id}, status code: '{response.StatusCode}'.");
                    return RedirectToAction(nameof(Index));
                }
                _metric.ResultCount = 1;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return BadRequest(genEx.Message);
            }
        }
    }
}
