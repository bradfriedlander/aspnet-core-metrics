using DataAnnotationsExtensions;

namespace demoWebApp.Models.InputBinding
{
    /// <summary>
    /// This is the input class for the Index action of the Admin controller.
    /// </summary>
    public class AdminIndex
    {
        /// <summary>Gets or sets the maximum number of records.</summary>
        /// <value>The count.</value>
        [Min(1)]
        public int? Count { get; set; }
    }
}
