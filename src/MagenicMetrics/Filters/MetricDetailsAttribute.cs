using System;
using MagenicMetrics.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MagenicMetrics.Filters
{
    /// <summary>
    ///     This class defines a filter for setting the object to be serialized into <see cref="IMetric.Details" />.
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <seealso cref="IActionFilter" />
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MetricDetailsAttribute : Attribute, IActionFilter
    {
        /// <summary>
        ///     Gets or sets the source identifier.
        /// </summary>
        /// <value>
        ///     This is the source identifier. It is the name of the action parameter that gets serialized into <see cref="IMetric.Details" />.
        /// </value>
        public string Source { get; set; }

        /// <summary>
        ///     Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutedContext" />.</param>
        /// <remarks>
        ///     <para>This method does nothing.</para>
        /// </remarks>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        ///     Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">This is the <see cref="ActionExecutingContext" /> context.</param>
        /// <exception cref="ArgumentOutOfRangeException">Source</exception>
        /// <remarks>
        ///     <para>
        ///         This method invokes <see cref="MetricsBaseController.SetMetricDetails(object)" /> to save the object identified by <see
        ///         cref="Source" />.
        ///     </para>
        /// </remarks>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey(Source))
            {
                throw new ArgumentOutOfRangeException(nameof(Source), $"'{Source}' is not a valid parameter for the {context.ActionDescriptor.DisplayName} action.");
            }
            ((MetricsBaseController)context.Controller).SetMetricDetails(context.ActionArguments[Source]);
        }
    }
}
