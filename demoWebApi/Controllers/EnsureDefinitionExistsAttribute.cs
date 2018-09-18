using demoWebApi.InputModels;
using demoWebApi.Models;
using demoWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace demoWebApi.Controllers
{
    /// <summary>
    ///     This attribute ensure that the record id is valid and that there is a matching <see cref="Definition" /> record.
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    public class EnsureDefinitionExistsAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     This method is executed before the action begins executing.
        /// </summary>
        /// <param name="context">This is the context for the action.</param>
        /// <inheritdoc />
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var definitionContext = (ApiContext)context.HttpContext.RequestServices.GetService(typeof(ApiContext));
            var definitionId = 0;
            if (context.ActionArguments.Keys.Contains("id"))
            {
                definitionId = (int)context.ActionArguments["id"];
            }
            else if (context.ActionArguments.Keys.Contains("definition"))
            {
                var definitionInput = (DefinitionInput)context.ActionArguments["definition"];
                if (!definitionInput.Id.HasValue)
                {
                    context.Result = new BadRequestResult();
                }
                definitionId = (int)definitionInput.Id;
            }
            else
            {
                context.Result = new BadRequestResult();
                return;
            }
            if (definitionId <= 0)
            {
                context.Result = new BadRequestResult();
                return;
            }
            if (!await definitionContext.DoesDefinitionExist(definitionId))
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
