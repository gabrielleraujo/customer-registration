using CustomerRegistration.Domain.Models.Exceptions;

namespace CustomerRegistration.API.Configurations;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var requestId = Guid.NewGuid();

        try
        {
            await _next(httpContext);
        }
        catch (DomainException exception)
        {
            LogError(requestId, exception);
            await httpContext.DomainExceptionHandleAsync(exception, requestId);
        }
        catch (Exception exception)
        {
            LogError(requestId, exception);
            await httpContext.ExceptionHandleAsync(exception, requestId);
        }
    }
    
    private void LogError(Guid requestId, Exception exception)
    {
        _logger.LogError($"CustomerRegistration: RequestId: {requestId} - DateTime: {DateTime.Now}\n{exception}");
    }
}
