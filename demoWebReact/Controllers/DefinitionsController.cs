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

        /// <summary>
        ///     This retrieves all records, including logically deleted records, in the persistence store.
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET api/values/GetAll</remarks>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            //var results = _context.Definitions.IgnoreQueryFilters().ToList();
            //_metric.ResultCount = results.Count;
            var results = new List<Definition>
            {
                new Definition{DefinitionId=1,IsDeleted=false,Name="Definition 1"},
                new Definition{DefinitionId=2,IsDeleted=true,Name="Definition 2"}
            };
            return Ok(results);
        }

    }

}