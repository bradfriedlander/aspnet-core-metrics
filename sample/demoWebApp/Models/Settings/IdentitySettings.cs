namespace demoWebApp.Models.Settings
{
    public class IdentitySettings
    {
        /// <summary>
        ///     Gets or sets the URL of the identity server.
        /// </summary>
        /// <value>This is the URL of the identity server.</value>
        public string IdentityServer { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether or not to use the identity server.
        /// </summary>
        /// <value><c>true</c> if the application should use the identity server; otherwise, <c>false</c>.</value>
        public bool UseIdentityServer { get; set; }
    }
}
