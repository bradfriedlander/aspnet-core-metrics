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
        public int? Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
