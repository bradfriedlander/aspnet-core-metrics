namespace demoWebApi.Models
{
    /// <summary>
    ///     This is the definition class that corresponds to the database table.
    /// </summary>
    public class Definition
    {
        /// <summary>
        ///     Gets or sets the definition identifier.
        /// </summary>
        /// <value>This is the identity definition identifier.</value>
        public int DefinitionId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     Gets or sets the name for the definition.
        /// </summary>
        /// <value>This is the name.</value>
        public string Name { get; set; }
    }
}
