using demoWebApi.InputModels;
using demoWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace demoWebApi.Filters
{
    /// <summary>
    ///     This filter ensures that the record id is valid and that there is a matching <see cref="Definition" /> record.
    /// </summary>
    /// <seealso cref="IActionFilter" />
    public class EnsureDefinitionExistsFilter : IActionFilter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EnsureDefinitionExistsFilter" /> class.
        /// </summary>
        /// <param name="apiContext">The API context.</param>
        public EnsureDefinitionExistsFilter(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        private readonly ApiContext _apiContext;

        /// <summary>
        ///     Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        ///     This method is executed before the action begins executing.
        /// </summary>
        /// <param name="context">This is the context for the action.</param>
        /// <inheritdoc />
        public async void OnActionExecuting(ActionExecutingContext context)
        {
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
                    context.ModelState.AddModelError(nameof(definitionInput.Id), $"No value provided for {nameof(DefinitionInput.Id)}");
                    context.Result = new BadRequestResult();
                }
                definitionId = (int)definitionInput.Id;
            }
            else
            {
                context.ModelState.AddModelError("definition", $"No value provided for {nameof(DefinitionInput)}");
                context.Result = new BadRequestResult();
                return;
            }
            if (definitionId <= 0)
            {
                context.ModelState.AddModelError(nameof(DefinitionInput.Id), $"{nameof(DefinitionInput.Id)} must be > 0");
                context.Result = new BadRequestResult();
                return;
            }
            if (!await _apiContext.DoesDefinitionExist(definitionId))
            {
                context.ModelState.AddModelError(nameof(DefinitionInput.Id), $"'{definitionId}' does not exist in persistent store.");
                context.Result = new NotFoundResult();
            }
        }
    }
}
