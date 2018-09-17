using System.ComponentModel.DataAnnotations;

namespace demoWebApp.Models.InputBinding
{
    /// <summary>
    ///     This is the input class for the Index action of the Admin controller.
    /// </summary>
    public class AdminIndexInput
    {
        /// <summary>
        ///     Gets or sets the application name filter.
        /// </summary>
        /// <value>This is the application name filter (using T-SQL case-insensitive contains).</value>
        public string ApplicationFilter { get; set; }

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        /// <value>This is the page number where <see cref="PageSize" /> is the page size.</value>
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets the maximum number of records to be shown on a page.
        /// </summary>
        /// <value>The count.</value>
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
    }
}
