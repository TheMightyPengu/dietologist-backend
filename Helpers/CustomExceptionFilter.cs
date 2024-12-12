using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dietologist_backend.Helpers;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(IWebHostEnvironment environment, ILogger<CustomExceptionFilter> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled) return;

        var exception = context.Exception;

        if (exception is ValidationException validationException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Error = "Validation failed",
                Details = validationException.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                })
            });
            context.ExceptionHandled = true;
            return;
        }

        var includeStackTrace = _environment.IsDevelopment();
        _logger.LogError(exception, "Unhandled exception occurred.");

        context.Result = new ObjectResult(new
        {
            Error = "An unexpected error occurred. Please try again later.",
            Details = includeStackTrace ? exception.Message : null,
            StackTrace = includeStackTrace ? exception.StackTrace : null
        })
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}