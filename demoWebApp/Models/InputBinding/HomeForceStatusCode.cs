using System.ComponentModel.DataAnnotations;

namespace demoWebApp.Models.InputBinding
{
    /// <summary>
    ///     This is the input and binding model for status code input.
    /// </summary>
    public class HomeForceStatusCode
    {
        /// <summary>
        ///     Gets or sets the status code value.
        /// </summary>
        /// <value>This is the status code value.</value>
        [Range(200, 599)]
        public int Code { get; set; }
    }
}
