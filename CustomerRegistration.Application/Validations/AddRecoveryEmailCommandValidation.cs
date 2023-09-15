using CustomerRegistration.Application.Commands.AddRecoveryEmailCommand;
using CustomerRegistration.Application.Validations.Utils;
using FluentValidation;

namespace CustomerRegistration.Application.Validations;

public class AddRecoveryEmailCommandValidation : AbstractValidator<AddRecoveryEmailCommand>
{
    public AddRecoveryEmailCommandValidation()
    {
        RuleFor(c => c.Email).ValidateNullOrEmpty("CellPhone");
    }
}