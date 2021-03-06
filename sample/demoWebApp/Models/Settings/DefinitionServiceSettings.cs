﻿using demoWebApp.Controllers;

namespace demoWebApp.Models.Settings
{
    /// <summary>
    ///     This class contains the setting options for the <see cref="DefinitionController" />.
    /// </summary>
    public class DefinitionServiceSettings
    {
        /// <summary>
        ///     Gets or sets the base URL for the Definition WEB API.
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseUrl { get; set; }
    }
}
