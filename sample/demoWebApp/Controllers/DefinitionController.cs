﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using demoWebApp.HttpHelpers;
using demoWebApp.Models.Settings;
using demoWebApp.Models.ViewBinding;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using MagenicMetrics.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        /// <param name="metric">This is the metric instance used by this controller.</param>
        /// <param name="logger">This is the logger for this controller instance.</param>
        /// <param name="options">These are the options for the definition service.</param>
        public DefinitionController(IMetric metric, ILogger<DefinitionController> logger, IOptions<DefinitionServiceSettings> options) : base(metric)
        {
            _logger = logger;
            baseUri = options.Value.BaseUrl;
            definitions = new List<DefinitionIndexView>();
        }

        /// <summary>
        ///     This is the list of definitions managed by the current action.
        /// </summary>
        private static List<DefinitionIndexView> definitions;

        /// <summary>
        ///     This is the logger instance for this controller.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     This is the base URI for the definition APIs.
        /// </summary>
        private readonly string baseUri;

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
        [MetricDetails(Source = "newDefinition")]
        public async Task<IActionResult> Create(DefinitionIndexView newDefinition)
        {
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
        [MetricDetails(Source = "id")]
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
        [MetricDetails(Source = "id")]
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
        [MetricDetails(Source = "id")]
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
        [MetricDetails(Source = "definition")]
        public async Task<IActionResult> Edit(DefinitionIndexView definition)
        {
            try
            {
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
        [MetricDetails(Source = "id")]
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
