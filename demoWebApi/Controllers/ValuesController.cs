using System.Collections.Generic;
using System.Linq;
using demoWebApi.Filters;
using demoWebApi.InputModels;
using demoWebApi.Models;
using demoWebApi.Services;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demoWebApi.Controllers
{
    /// <summary>
    ///     This controller manages the APIs to add, delete, and retrieve database entries.
    /// </summary>
    /// <seealso cref="BaseController" />
    [ApiController]
    public class ValuesController : BaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValuesController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        /// <param name="context">The context.</param>
        public ValuesController(IMetric metric, ApiContext context) : base(metric)
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
        [HttpDelete("{id}"), EnsureDefinitionExists]
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
        public ActionResult<IEnumerable<string>> Get()
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
        [HttpGet("{id}"), EnsureDefinitionExists]
        public ActionResult<string> Get(int id)
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
        public ActionResult<IEnumerable<string>> GetAll()
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
        /// <remarks>POST api/values?DefinitionInput</remarks>
        [HttpPost]
        public IActionResult Post(DefinitionInput definition)
        {
            var nextId = _context.Definitions.Max(d => d.DefinitionId) + 1;
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
        [HttpPut, EnsureDefinitionExists]
        public IActionResult Put(DefinitionInput definition)
        {
            var match = _context.Definitions.Find(definition.Id);
            if (match == null)
            {
                return NotFound();
            }
            match.Name = definition.Name;
            _metric.ResultCount = _context.SaveChanges();
            return Ok(match);
        }

        /// <summary>
        ///     This restores the record identified by <paramref name="definition" />.
        /// </summary>
        /// <param name="definition">This is the definition update.</param>
        /// <returns>The restore definition.</returns>
        /// <remarks>PUT api/values/Undelete?DefinitionInput</remarks>
        [HttpPut("Undelete"), EnsureDefinitionExists]
        public IActionResult Undelete(DefinitionInput definition)
        {
            var match = _context.Definitions.IgnoreQueryFilters().FirstOrDefault(d => d.DefinitionId == definition.Id);
            match.IsDeleted = false;
            _metric.ResultCount = _context.SaveChanges();
            return Ok(match);
        }
    }
}
