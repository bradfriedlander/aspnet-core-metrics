using System.Collections.Generic;
using demoWebReact.Models;
using Microsoft.AspNetCore.Mvc;

namespace demoWebReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefinitionsController : ControllerBase
    {
        public DefinitionsController()
        {
        }

        // TODO: Get this from configuration - use options
        private readonly string baseUri = "https://localhost:5001/api/values";

        [HttpPost("Add")]
        public ActionResult<Definition> Add(Definition definition)
        {
            // TODO: invoke web service
            return Ok(definition);
        }

        [HttpDelete("Delete")]
        public ActionResult<Definition> Delete(Definition definition)
        {
            // TODO: invoke web service
            return Ok(definition);
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
        ///     This retrieves the record identified by <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The matching definition.</returns>
        /// <remarks>GET api/values/5</remarks>
        [HttpGet("{id}")]
        public ActionResult<Definition> Get(int id)
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
        /// <remarks>GET api/values/GetAll</remarks>
        [HttpGet("GetAll")]
        public ActionResult<List<Definition>> GetAll()
        {
            //var results = _context.Definitions.IgnoreQueryFilters().ToList();
            //_metric.ResultCount = results.Count;
            var results = new List<Definition>
            {
                new Definition {DefinitionId=1, IsDeleted=false, Name="Definition 1"},
                new Definition {DefinitionId=2, IsDeleted=true, Name="Definition 2"}
            };
            // TODO: invoke web service
            return Ok(results);
        }

        [HttpPut("Undelete")]
        public ActionResult<Definition> Undelete(Definition definition)
        {
            // TODO: invoke web service
            return Ok(definition);
        }

        [HttpPut("Update")]
        public ActionResult<Definition> Update(Definition definition)
        {
            // TODO: invoke web service
            return Ok(definition);
        }
    }
}
