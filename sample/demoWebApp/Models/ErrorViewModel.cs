namespace demoWebApp.Models
{
    /// <summary>
    ///     This is the view model for errors.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        ///     Gets or sets the request identifier.
        /// </summary>
        /// <value>The request identifier.</value>
        public string RequestId { get; set; }

        /// <summary>
        ///     Gets a value indicating if the request identifier should be shown.
        /// </summary>
        /// <value><c>true</c> if the request identifier should be shown; otherwise, <c>false</c>.</value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
