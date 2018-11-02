using System.ComponentModel.DataAnnotations;

namespace demoWebReact.Models
{
    /// <summary>
    ///     This is the input class for the APIs of the Metric controller.
    /// </summary>
    public class MetricsQuery
    {
        /// <summary>
        ///     Gets or sets the application name filter.
        /// </summary>
        /// <value>This is the application name filter (using T-SQL case-insensitive contains).</value>
        [Display(Name = "Application Name Filter")]
        public string ApplicationFilter { get; set; }

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        /// <value>This is the page number where <see cref="PageSize" /> is the page size.</value>
        [Range(1, int.MaxValue, ErrorMessage = "Page No must be > 0")]
        [Display(Name = "Page No")]
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets the maximum number of records to be shown on a page.
        /// </summary>
        /// <value>The count.</value>
        [Range(1, int.MaxValue, ErrorMessage = "Page Size must be > 0")]
        [Display(Name = "Page Size")]
        public int PageSize { get; set; }
    }
}
