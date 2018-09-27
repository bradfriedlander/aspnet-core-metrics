using System.ComponentModel.DataAnnotations;

namespace demoWebApi.InputModels
{
    /// <summary>
    ///     This is the <see cref="Models.Definition" /> binding model for requests.
    /// </summary>
    public class DefinitionInput
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>This is the identifier.</value>
        [Range(1, int.MaxValue)]
        public int? DefinitionId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
