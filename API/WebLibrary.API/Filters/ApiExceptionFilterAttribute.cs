using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebLibrary.API.Models;
using WebLibrary.BusinessLayer.Exceptions;

namespace WebLibrary.API.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotSuchValidatorException), HandleNotSuchValidatorException },
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleNotSuchValidatorException(ExceptionContext context)
    {
        var exception = (NotSuchValidatorException)context.Exception;

        var badRequest = new CustomBadRequestResult
        {
            Title = "Not implemented",
            StatusCode = StatusCodes.Status501NotImplemented,
            Errors = exception.Message,
        };

        context.Result = new ObjectResult(badRequest)
        {
            StatusCode = StatusCodes.Status501NotImplemented,
        };

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var errors = exception.Errors;

        var badRequest = new CustomBadRequestResult
        {
            Title = "Validation error, please check the error list for more details",
            StatusCode = StatusCodes.Status400BadRequest,
            Errors = errors,
        };

        context.Result = new ObjectResult(badRequest)
        {
            StatusCode = StatusCodes.Status400BadRequest,
        };

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var badRequest = new CustomBadRequestResult
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Errors = "There was a problem handling your request. Please try again.",
        };

        context.Result = new ObjectResult(badRequest)
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };

        context.ExceptionHandled = true;
    }
}