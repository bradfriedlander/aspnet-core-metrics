using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWebReact.Models;
using MagenicMetrics;
using MagenicMetrics.Controllers;
using MagenicMetrics.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace demoWebReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MetricsController : MetricsBaseController
    {
        public MetricsController(IMetric metric, IMetricService metricService) : base(metric)
        {
            _metricService = metricService;
        }

        private readonly IMetricService _metricService;

        [HttpPost("Get")]
        [MetricDetails(Source = "query")]
        public async Task<ActionResult<MetricsPage>> Get([FromBody]MetricsQuery query)
        {
            var metricsPage = new MetricsPage
            {
                ApplicationFilter = query.ApplicationFilter,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                Metrics = new List<IMetric>()
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(metricsPage);
            }
            var results = await _metricService.GetLatest(query.PageSize, query.PageNumber, query.ApplicationFilter ?? string.Empty);
            var metricList = results.ToList();
            _metric.ResultCount = metricList.Count;
            metricsPage.Metrics = metricList;
            return Ok(metricsPage);
        }
    }
}
