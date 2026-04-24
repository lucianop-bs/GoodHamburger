using GoodHamburger.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IResult = GoodHamburger.Domain.Results.IResult;

namespace GoodHamburger.API.Filters
{
    public class ResultActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Result is ObjectResult objectResult && objectResult.Value is IResult result)
            {
                if (result.isFailure)
                {
                    executedContext.Result = result.ErrorType switch
                    {
                        ErrorType.NotFound => new NotFoundObjectResult(new { erro = result.ErrorMessage }),
                        ErrorType.Validation => new BadRequestObjectResult(new { erro = result.ErrorMessage }),
                        ErrorType.Conflicable => new ConflictObjectResult(new { erro = result.ErrorMessage }),

                        _ => new BadRequestObjectResult(new { erro = result.ErrorMessage })
                    };
                }
                else
                {
                    var value = result.GetValue();

                    executedContext.Result = value == null ? new NoContentResult() : new OkObjectResult(value);
                }
            }
        }
    }
}