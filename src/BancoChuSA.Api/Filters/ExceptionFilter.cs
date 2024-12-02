using BancoChuSA.Communication.Responses;
using BancoChuSA.Exception;
using BancoChuSA.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BancoChuSA.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BancoChuSAException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var bancoChuSAException = (BancoChuSAException)context.Exception;
        var errorResponse = new ResponseErrorJson(bancoChuSAException.GetErrors());

        context.HttpContext.Response.StatusCode = bancoChuSAException.StatusCode;
        context.Result = new ObjectResult(errorResponse);        
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);

    }
}
