﻿using System.Collections.Generic;
using System.Linq;
using demoWebApi.Filters;
using demoWebApi.InputModels;
using demoWebApi.Models;
using demoWebApi.Services;
using MagenicMetrics;
using MagenicMetrics.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demoWebApi.Controllers
{
    /// <summary>
    ///     This controller manages the APIs to add, delete, and retrieve database entries.
    /// </summary>
    /// <seealso cref="ApiBaseController" />
    [ApiController]
    public class ValuesController : ApiBaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValuesController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        /// <param name="context">The context.</param>
        /// <param name="healthCheckStatus">The health check status.</param>
        public ValuesController(IMetric metric, ApiContext context, IHealthCheckStatus healthCheckStatus) : base(metric, healthCheckStatus)
        {
            _context = context;
        }

        /// <summary>
        ///     This is the context for the persistence store.
        /// </summary>
        private readonly ApiContext _context;

        /// <summary>
        ///     This logically deletes the record identified by <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <remarks>DELETE api/values/5</remarks>
        [HttpDelete("{id}")]
        [EnsureDefinitionExists]
        [MetricDetails(Source = "id")]
        public IActionResult Delete(int id)
        {
            var match = _context.Definitions.Find(id);
            match.IsDeleted = true;
            _metric.ResultCount = _context.SaveChanges();
            return Ok(match);
        }

        /// <summary>
        ///     This retrieves all undeleted records from the persistence store.
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET api/values</remarks>
        [HttpGet]
        public ActionResult<IEnumerable<Definition>> Get()
        {
            var results = _context.Definitions.ToList();
            _metric.ResultCount = results.Count;
            return Ok(results);
        }

        /// <summary>
        ///     This retrieves the record identified by <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The matching definition.</returns>
        /// <remarks>GET api/values/5</remarks>
        [HttpGet("{id}")]
        [EnsureDefinitionExists]
        [MetricDetails(Source = "id")]
        public ActionResult<Definition> Get(int id)
        {
            var match = _context.Definitions.Find(id);
            _metric.ResultCount = 1;
            return Ok(match);
        }

        /// <summary>
        ///     This retrieves all records, including logically deleted records, in the persistence store.
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET api/values/GetAll</remarks>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Definition>> GetAll()
        {
            var results = _context.Definitions.IgnoreQueryFilters().ToList();
            _metric.ResultCount = results.Count;
            return Ok(results);
        }

        /// <summary>
        ///     This adds a new definition to the persistence store.
        /// </summary>
        /// <param name="definition">This is the new definition.</param>
        /// <returns>The newly added definition.</returns>
        /// <remarks>
        ///     <para>POST api/values?DefinitionInput</para>
        ///     <para>Note 1: Need to account for logically deleted definitions which may include the highest id used.</para>
        /// </remarks>
        [HttpPost]
        [MetricDetails(Source = "definition")]
        public IActionResult Post(DefinitionInput definition)
        {
            // Note 1
            var nextId = _context.Definitions.IgnoreQueryFilters().Max(d => d.DefinitionId) + 1;
            _context.Definitions.Add(new Definition() { DefinitionId = nextId, Name = definition.Name });
            _metric.ResultCount = _context.SaveChanges();
            var match = _context.Definitions.Find(nextId);
            return Ok(match);
        }

        /// <summary>
        ///     This updates the record identified by <paramref name="definition" />.
        /// </summary>
        /// <param name="definition">This is the definition update.</param>
        /// <returns>The updated definition.</returns>
        /// <remarks>PUT api/values?DefinitionInput</remarks>
        [HttpPut]
        [EnsureDefinitionExists]
        [MetricDetails(Source = "definition")]
        public IActionResult Put(DefinitionInput definition)
        {
            var match = _context.Definitions.Find(definition.DefinitionId);
            if (match == null)
            {
                return NotFound();
            }
            match.Name = definition.Name;
            _metric.ResultCount = _context.SaveChanges();
            return Ok(match);
        }

        /// <summary>
        ///     This restores the record identified by <paramref name="id" />.
        /// </summary>
        /// <param name="id">This is the record to logically restore.</param>
        /// <returns>The restored definition.</returns>
        /// <remarks>PUT api/values/Undelete?id</remarks>
        [HttpPut("Undelete")]
        [EnsureDefinitionExists]
        [MetricDetails(Source = "id")]
        public IActionResult Undelete(int id)
        {
            var match = _context.Definitions.IgnoreQueryFilters().FirstOrDefault(d => d.DefinitionId == id);
            match.IsDeleted = false;
            _metric.ResultCount = _context.SaveChanges();
            return Ok(match);
        }
    }
}
