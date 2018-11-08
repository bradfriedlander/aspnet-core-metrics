using demoWebReact.Models;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace demoWebReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : MetricsBaseController
    {
        public AuthenticationController(IMetric metric) : base(metric)
        {
        }

        [HttpGet("GetAuthentication")]
        public IActionResult GetAuthentication()
        {
            var userName = ControllerContext.HttpContext.User.Identity.Name ?? "";
            var results = new Authentication { IsAuthenticated = !string.IsNullOrEmpty(userName), UserName = userName };
            _metric.Details = JsonConvert.SerializeObject(results);
            return Ok(results);
        }
    }
}
