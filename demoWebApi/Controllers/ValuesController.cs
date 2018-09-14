using System.Collections.Generic;
using System.Linq;
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
    [Route("api/[controller]")]
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

        private readonly ApiContext _context;

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <remarks>DELETE api/values/5</remarks>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var match = _context.Definitions.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            _metric.ResultCount = -1;
            return Ok();
        }

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET api/values</remarks>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var results = _context.Definitions.ToList();
            _metric.ResultCount = results.Count;
            return new JsonResult(results);
        }

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <remarks>GET api/values/5</remarks>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var match = _context.Definitions.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            _metric.ResultCount = 1;
            return new JsonResult(match);
        }

        /// <summary>
        ///     Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <remarks>POST api/values</remarks>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var nextId = _context.Definitions.Max(d => d.DefinitionId) + 1;
            _context.Definitions.Add(new Definition() { DefinitionId = nextId, Name = value });
            _context.SaveChanges();
            _metric.ResultCount = 1;
        }

        /// <summary>
        ///     Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>PUT api/values/5</remarks>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            var match = _context.Definitions.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            match.Name = value;
            _metric.ResultCount = _context.SaveChanges();
            return Ok();
        }
    }
}
