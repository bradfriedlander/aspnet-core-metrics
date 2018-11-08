using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using demoWebReact.HttpHelpers;
using demoWebReact.Models;
using demoWebReact.Models.Settings;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using MagenicMetrics.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace demoWebReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DefinitionsController : MetricsBaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefinitionController" /> class.
        /// </summary>
        /// <param name="metric">This is the metric instance used by this controller.</param>
        /// <param name="logger">This is the logger for this controller instance.</param>
        /// <param name="options">These are the options for the definition service.</param>
        public DefinitionsController(IMetric metric, ILogger<DefinitionsController> logger, IOptions<DefinitionServiceSettings> options) : base(metric)
        {
            _logger = logger;
            baseUri = options.Value.BaseUrl;
        }

        private static List<Definition> definitions;

        /// <summary>
        ///     This is the logger instance for this controller.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     This is the base URI for the definition APIs.
        /// </summary>
        private readonly string baseUri;

        [HttpPost("Create")]
        [MetricDetails(Source = "newDefinition")]
        public async Task<ActionResult<Definition>> Create(Definition newDefinition)
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
                    return BadRequest(newDefinition);
                }
                var result = response.ContentAsType<Definition>();
                _metric.ResultCount = 1;
                return Ok(result);
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return BadRequest(genEx.Message);
            }
        }

        [HttpDelete("Delete")]
        [MetricDetails(Source = "definition")]
        public async Task<ActionResult<Definition>> Delete(Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(definition);
            }
            try
            {
                var requestUri = $"{baseUri}/{definition.DefinitionId}";
                var response = await HttpRequestFactory.Delete(requestUri);
                if (!response.IsSuccessStatusCode)
                {
                    _metric.ResultCode = (int)response.StatusCode;
                    ModelState.AddModelError("", $"Could not delete definition {definition.DefinitionId}, status code: '{response.StatusCode}'.");
                    return NotFound(definition);
                }
                var result = response.ContentAsType<Definition>();
                _metric.ResultCount = 1;
                return Ok(result);
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return BadRequest(genEx.Message);
            }
        }

        /// <summary>
        ///     This retrieves the record identified by <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The matching definition.</returns>
        /// <remarks>GET api/[controller]/Details/{id}</remarks>
        [HttpGet("Details/{id}")]
        [MetricDetails(Source = "id")]
        public async Task<ActionResult<Definition>> Details(int id)
        {
            var requestUri = $"{baseUri}/{id}";
            var response = await HttpRequestFactory.Get(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                _metric.ResultCode = (int)response.StatusCode;
                ModelState.AddModelError("", $"Could not get definition {id}, status code: '{response.StatusCode}'.");
                return NotFound(id);
            }
            var results = response.ContentAsType<Definition>();
            _metric.ResultCount = 1;
            return Ok(results);
        }

        /// <summary>
        ///     This retrieves all records, including logically deleted records, in the persistence store.
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET api/[controller]/Get</remarks>
        [HttpGet("Get")]
        [MetricDetails(Source = "definition")]
        public async Task<ActionResult<List<Definition>>> Get()
        {
            var requestUri = $"{baseUri}/Get";
            var response = await HttpRequestFactory.Get(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                _metric.ResultCode = (int)response.StatusCode;
                ModelState.AddModelError("", $"Could not retrieve definitions, status code: '{response.StatusCode}'.");
                return BadRequest(definitions);
            }
            definitions = response.ContentAsType<List<Definition>>();
            _metric.ResultCount = definitions.Count;
            return Ok(definitions);
        }

        /// <summary>
        ///     This retrieves all records, including logically deleted records, in the persistence store.
        /// </summary>
        /// <returns>This is the current list of <see cref="Definition" />.</returns>
        /// <remarks>GET api/[controller]/GetAll</remarks>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Definition>>> GetAll()
        {
            var requestUri = $"{baseUri}/GetAll";
            var response = await HttpRequestFactory.Get(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                _metric.ResultCode = (int)response.StatusCode;
                ModelState.AddModelError("", $"Could not retrieve definitions, status code: '{response.StatusCode}'.");
                return BadRequest(definitions);
            }
            definitions = response.ContentAsType<List<Definition>>();
            _metric.ResultCount = definitions.Count;
            return Ok(definitions);
        }

        [HttpPut("Undelete")]
        [MetricDetails(Source = "definition")]
        public async Task<ActionResult<Definition>> Undelete(Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(definition);
            }
            try
            {
                var requestUri = $"{baseUri}/Undelete?id={definition.DefinitionId}";
                var response = await HttpRequestFactory.Put(requestUri, string.Empty);
                if (!response.IsSuccessStatusCode)
                {
                    _metric.ResultCode = (int)response.StatusCode;
                    ModelState.AddModelError("", $"Could not undelete definition {definition.DefinitionId}, status code: '{response.StatusCode}'.");
                    return BadRequest(definition);
                }
                var results = response.ContentAsType<Definition>();
                _metric.ResultCount = 1;
                return Ok(results);
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return BadRequest(genEx.Message);
            }
        }

        [HttpPut("Update")]
        [MetricDetails(Source = "definition")]
        public async Task<ActionResult<Definition>> Update(Definition definition)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(definition);
                }
                var requestUri = $"{baseUri}";
                var response = await HttpRequestFactory.Put(requestUri, definition);
                if (!response.IsSuccessStatusCode)
                {
                    _metric.ResultCode = (int)response.StatusCode;
                    ModelState.AddModelError("", $"Could not edit definition {definition.DefinitionId}, status code: '{response.StatusCode}'.");
                    return BadRequest(definition);
                }
                var results = response.ContentAsType<Definition>();
                _metric.ResultCount = 1;
                return Ok(results);
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return BadRequest(genEx.Message);
            }
        }
    }
}
