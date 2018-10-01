using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Text;

namespace MagenicMetrics.Controllers
{
    /// <summary>
    ///     This is a base class for all controllers that use <see cref="MetricMiddleware" />.
    /// </summary>
    /// <seealso cref="Controller" />
    public class MetricsBaseController : Controller
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MetricsBaseController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        public MetricsBaseController(IMetric metric)
        {
            _metric = metric;
            _metric.ResultCount = -1;
        }

        /// <summary>
        ///     Gets the Details.
        /// </summary>
        /// <value>This is the Details.</value>
        public object Details { get; private set; }

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

        /// <summary>
        ///     Sets the details.
        /// </summary>
        /// <param name="details">This is the details to serialize into <see cref="IMetric.Details" />.</param>
        protected void SetDetails(object details) => Details = details;

        /// <summary>
        ///     This method sets the value of <see cref="IMetric.Details" />.
        /// </summary>
        /// <remarks>
        ///     <para>This method takes no action if <see cref="Details" /> is <c>null</c> or <see cref="IMetric.Details" /> already has a value.</para>
        ///     <para>
        ///         If <see cref="Details" /> is a <see cref="string" /> or <see cref="Type.IsPrimitive" />, <see cref="IMetric.Details" /> is set to a
        ///         simple string representation of <see cref="Details" />.
        ///     </para>
        ///     <para>Otherwise, <see cref="IMetric.Details" /> is set to a JSON serialization of <see cref="Details" />.</para>
        /// </remarks>
        private void SerializeDetails()
        {
            if (Details == null || !string.IsNullOrEmpty(_metric.Details))
            {
                return;
            }
            _metric.Details = Details is string
                ? Details as string
                : Details.GetType().IsPrimitive
                    ? Details.ToString()
                    : JsonConvert.SerializeObject(Details);
        }
    }
}
