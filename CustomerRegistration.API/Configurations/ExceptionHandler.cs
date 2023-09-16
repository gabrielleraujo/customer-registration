using System.Net;
using CustomerRegistration.API.Models;
using CustomerRegistration.Domain.Models.Exceptions;

namespace CustomerRegistration.API.Configurations;

public static class ExceptionHandler
{
    public static Task DomainExceptionHandleAsync(this HttpContext context, DomainException exception, Guid requestId)
    {
        var message = CreateMessageError(context, exception);

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return context.Response.WriteAsync(new ErrorResponse(
            requestId, 
            context.Response.StatusCode, 
            "An error occurred in domain validation while processing your request.", 
            message).ToString());
    }

    public static Task ExceptionHandleAsync(this HttpContext context, Exception exception, Guid requestId)
    {
        var message = CreateMessageError(context, exception);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(new ErrorResponse(
            requestId, 
            context.Response.StatusCode, 
            "An unexpected error occurred while processing your request.", 
            message).ToString());
    }

    private static string CreateMessageError(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        return $"Route: {context.Request.Path} - {exception.Message}";
    }
}