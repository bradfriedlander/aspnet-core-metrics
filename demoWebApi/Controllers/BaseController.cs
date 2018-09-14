using System.Text;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;

namespace demoWebApi.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IMetric metric)
        {
            _metric = metric;
        }

        public readonly IMetric _metric;

        protected string GetValidationErrors()
        {
            var validationErrors = new StringBuilder();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (validationErrors.Length != 0)
                    {
                        validationErrors.Append("; ");
                    }
                    validationErrors.Append(error.ErrorMessage);
                }
            }
            return validationErrors.ToString();
        }
    }
}
