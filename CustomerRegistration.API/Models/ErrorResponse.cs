using Newtonsoft.Json;

namespace CustomerRegistration.API.Models;

public class ErrorResponse
{
    public int StatusCode { get; private set; }
    public Guid ErrorId { get; private set; }
    public string ContextError { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;

    public ErrorResponse(Guid requestId, int statusCode, string contextError, string message)
    {
        StatusCode = statusCode;
        ErrorId = requestId;
        ContextError = contextError;
        Message = message;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}