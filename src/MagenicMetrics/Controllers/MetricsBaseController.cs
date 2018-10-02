using MagenicMetrics.Filters;
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
        }

        /// <summary>
        ///     Gets the <see cref="IMetric" /> Details.
        /// </summary>
        /// <value>This is the <see cref="IMetric" /> Details.</value>
        public object MetricDetails { get; private set; }

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
            SerializeDetails();
        }

        /// <summary>
        ///     Sets the <see cref="IMetric" /> details.
        /// </summary>
        /// <param name="details">This is the details to serialize into <see cref="IMetric.Details" />.</param>
        /// <remarks>
        ///     <para>This method can be invoked from any action that inherits from <see cref="MetricsBaseController" />.</para>
        ///     <para>Since <see cref="MetricDetailsAttribute" /> uses this method, it must be <c>public</c> instead of <c>protected</c>.</para>
        /// </remarks>
        public void SetMetricDetails(object details) => MetricDetails = details;

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
        ///     This method sets the value of <see cref="IMetric.Details" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method takes no action if <see cref="MetricDetails" /> is <c>null</c> or <see cref="IMetric.Details" /> already has a value.
        ///     </para>
        ///     <para>
        ///         If <see cref="MetricDetails" /> is a <see cref="string" /> or <see cref="Type.IsPrimitive" />, <see cref="IMetric.Details" /> is
        ///         set to a simple string representation of <see cref="MetricDetails" />.
        ///     </para>
        ///     <para>Otherwise, <see cref="IMetric.Details" /> is set to a JSON serialization of <see cref="MetricDetails" />.</para>
        /// </remarks>
        private void SerializeDetails()
        {
            if (MetricDetails == null || !string.IsNullOrEmpty(_metric.Details))
            {
                return;
            }
            _metric.Details = MetricDetails is string
                ? MetricDetails as string
                : MetricDetails.GetType().IsPrimitive
                    ? MetricDetails.ToString()
                    : JsonConvert.SerializeObject(MetricDetails);
        }
    }
}
