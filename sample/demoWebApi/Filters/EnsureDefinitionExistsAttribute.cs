using System;
using demoWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace demoWebApi.Filters
{
    /// <summary>
    ///     This attribute ensures that the record id is valid and that there is a matching <see cref="Definition" /> record.
    /// </summary>
    /// <seealso cref="TypeFilterAttribute" />
    /// <seealso cref="ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Method)]
    public class EnsureDefinitionExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EnsureDefinitionExistsAttribute" /> class.
        /// </summary>
        public EnsureDefinitionExistsAttribute() : base(typeof(EnsureDefinitionExistsFilter))
        {
        }
    }
}
