using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace CustomerRegistration.Application.Commands;

public abstract class CommandHandler<T> where T : class
{
    protected ILogger<T> _logger;
    protected ValidationResult ValidationResult;

    protected CommandHandler(ILogger<T> logger)
    {
        _logger = logger;
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string mensagem)
    {
        _logger.LogError($"{nameof(T)} Error: {mensagem}");
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
    }

    public bool IsValid()
    {
        return ValidationResult.IsValid;
    }
}
