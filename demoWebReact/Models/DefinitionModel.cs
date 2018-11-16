using System.ComponentModel.DataAnnotations;

namespace demoWebReact.Models
{
    /// <summary>
    ///     This is the view model for the retrieved definitions.
    /// </summary>
    public class DefinitionModel
    {
        /// <summary>
        ///     Gets or sets the definition identifier.
        /// </summary>
        /// <value>This is the identity definition identifier.</value>
        [Display(Name = "Id")]
        public int DefinitionId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [Display(Name = "Deleted?")]
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     Gets or sets the name for the definition.
        /// </summary>
        /// <value>This is the name.</value>
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
