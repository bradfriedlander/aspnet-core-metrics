using demoWebApi.Models;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace demoWebApi.Controllers
{
    /// <summary>
    ///     This is the base class for all API controllers.
    /// </summary>
    /// <remarks>
    ///     <para>This class defines the standard route prefix ("api/[controller]") for all API controllers.</para>
    /// </remarks>
    /// <seealso cref="Controller" />
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        /// <param name="healthCheckStatus">The health check status.</param>
        public BaseController(IMetric metric, IHealthCheckStatus healthCheckStatus)
        {
            _metric = metric;
            healthCheckStatus.IncrementRequestsProcessed();
        }

        /// <summary>
        ///     The is the current <see cref="IMetric" /> instance.
        /// </summary>
        public readonly IMetric _metric;

        /// <summary>
        ///     Called after the action method is invoked.
        /// </summary>
        /// <param name="context">The action executed context.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                _metric.ExceptionMessage = GetValidationErrors(context);
                _metric.ResultCount = -1;
            }
        }

        /// <summary>
        ///     Gets the concatenated validation errors.
        /// </summary>
        /// <returns>This is the concatenated validation errors with "|" uses as a separator.</returns>
        protected string GetValidationErrors(ActionExecutedContext context)
        {
            var validationErrors = new StringBuilder();
            foreach (var modelState in context.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (validationErrors.Length != 0)
                    {
                        validationErrors.Append("|");
                    }
                    validationErrors.Append(error.ErrorMessage);
                }
            }
            return validationErrors.ToString();
        }
    }
}
