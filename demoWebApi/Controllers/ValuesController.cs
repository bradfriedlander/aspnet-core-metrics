using System.Collections.Generic;
using System.Linq;
using demoWebApi.InputModels;
using demoWebApi.Models;
using demoWebApi.Services;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;

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
        ///     This deletes the record identified by <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <remarks>DELETE api/values/5</remarks>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var match = _context.Definitions.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            _context.Definitions.Remove(match);
            _metric.ResultCount = _context.SaveChanges();
            return Ok(match);
        }

        /// <summary>
        ///     This retrieves all records in the persistence store.
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
        /// <returns></returns>
        /// <remarks>GET api/values/5</remarks>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var match = _context.Definitions.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            _metric.ResultCount = 1;
            return Ok(match);
        }

        /// <summary>
        ///     This adds a new definition to the persistence store.
        /// </summary>
        /// <param name="definition">This is the new definition.</param>
        /// <remarks>POST api/values?DefinitionInput</remarks>
        [HttpPost]
        public void Post(DefinitionInput definition)
        {
            var nextId = _context.Definitions.Max(d => d.DefinitionId) + 1;
            _context.Definitions.Add(new Definition() { DefinitionId = nextId, Name = definition.name });
            _metric.ResultCount = _context.SaveChanges();
        }

        /// <summary>
        ///     This updates the record identified by <paramref name="definition" />.
        /// </summary>
        /// <param name="definition">This is the definition update.</param>
        /// <returns></returns>
        /// <remarks>PUT api/values?DefinitionInput</remarks>
        [HttpPut]
        public IActionResult Put(DefinitionInput definition)
        {
            if (!definition.id.HasValue || definition.id <= 0)
            {
                return BadRequest();
            }
            var match = _context.Definitions.Find(definition.id);
            if (match == null)
            {
                return NotFound();
            }
            match.Name = definition.name;
            _metric.ResultCount = _context.SaveChanges();
            return Ok();
        }
    }
}
