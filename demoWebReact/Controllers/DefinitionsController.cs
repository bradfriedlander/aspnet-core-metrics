using System.Collections.Generic;
using System.Threading.Tasks;
using demoWebReact.HttpHelpers;
using demoWebReact.Models;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace demoWebReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefinitionsController : MetricsBaseController
    {
        public DefinitionsController(IMetric metric, ILogger<DefinitionsController> logger/*, IOptions<DefinitionServiceSettings> options*/) : base(metric)
        {
        }

        private static List<Definition> definitions;

        // TODO: Get this from configuration - use options
        private readonly string baseUri = "https://localhost:5001/api/values";

        [HttpPost("Create")]
        public ActionResult<Definition> Create(Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(definition);
            }
            // TODO: invoke web service
            return Ok(definition);
        }

        [HttpDelete("Delete")]
        public ActionResult<Definition> Delete(Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(definition);
            }
            // TODO: invoke web service
            return Ok(definition);
        }

        /// <summary>
        ///     This retrieves the record identified by <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The matching definition.</returns>
        /// <remarks>GET api/[controller]/Details/{id}</remarks>
        [HttpGet("Details/{id}")]
        public ActionResult<Definition> Details(int id)
        {
            var match = new Definition { DefinitionId = id, IsDeleted = false, Name = $"Definition {id}" };
            // TODO: invoke web service
            //var match = _context.Definitions.Find(id);
            //_metric.ResultCount = 1;
            return Ok(match);
        }

        /// <summary>
        ///     This retrieves all records, including logically deleted records, in the persistence store.
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET api/[controller]/Get</remarks>
        [HttpGet("Get")]
        public ActionResult<List<Definition>> Get()
        {
            var results = new List<Definition>
            {
                new Definition {DefinitionId=1, IsDeleted=false, Name="Definition 1"},
                new Definition {DefinitionId=2, IsDeleted=true, Name="Definition 2"}
            };
            // TODO: invoke web service
            return Ok(results);
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
        public ActionResult<Definition> Undelete(Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(definition);
            }
            // TODO: invoke web service
            return Ok(definition);
        }

        [HttpPut("Update")]
        public ActionResult<Definition> Update(Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(definition);
            }
            // TODO: invoke web service
            return Ok(definition);
        }
    }
}
