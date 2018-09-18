using System.Text;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;

namespace demoWebApp.Controllers
{
    /// <summary>
    ///     This is the base class for all controllers.
    /// </summary>
    /// <seealso cref="Controller" />
    public class BaseController : Controller
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        public BaseController(IMetric metric)
        {
            _metric = metric;
        }

        /// <summary>
        ///     The is the current <see cref="IMetric" /> instance.
        /// </summary>
        public readonly IMetric _metric;

        /// <summary>
        ///     Gets the concatenated validation errors.
        /// </summary>
        /// <returns>This is the concatenated validation errors with "|" uses as a separator.</returns>
        protected string GetValidationErrors()
        {
            var validationErrors = new StringBuilder();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (validationErrors.Length != 0)
                    {
                        validationErrors.Append("| ");
                    }
                    validationErrors.Append(error.ErrorMessage);
                }
            }
            return validationErrors.ToString();
        }
    }
}
