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
                if (result.IsFailure)
                {
                    var statusCode = result.Error?.ErrorType switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        ErrorType.Validation => StatusCodes.Status400BadRequest,
                        ErrorType.Conflict => StatusCodes.Status409Conflict,
                        ErrorType.Failure => StatusCodes.Status500InternalServerError,

                        _ => StatusCodes.Status400BadRequest
                    };

                    object respostaPersonalizada;

                    if (result.Errors.Count == 1)
                    {
                        respostaPersonalizada = new
                        {
                            status = statusCode,
                            error = result?.Error?.Message
                        };
                    }
                    else
                    {
                        respostaPersonalizada = new
                        {
                            status = statusCode,
                            errors = result.Errors.Select(e => new
                            {
                                status = (int)e.ErrorType,
                                error = e.Message
                            }
                            )
                        };
                    }

                    objectResult.Value = respostaPersonalizada;
                    objectResult.StatusCode = statusCode;
                }
                else
                {
                    var value = result.GetValue();

                    var originalStatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;

                    executedContext.Result = value == null!
                        ? new NoContentResult()
                        : new ObjectResult(value) { StatusCode = originalStatusCode };
                }
            }
        }
    }
}