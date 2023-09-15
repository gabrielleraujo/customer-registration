using CustomerRegistration.Application.Commands.RegisterCustomerCommand;
using FluentValidation;
using CustomerRegistration.Application.Validations.Utils;

namespace CustomerRegistration.Application.Validations;
public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        RuleFor(c => c.FirstName).ValidateName("FirstName");
        RuleFor(c => c.LastName).ValidateName("LastName");
        RuleFor(c => c.CellPhone).ValidateNullOrEmpty("CellPhone");
        RuleFor(c => c.MainEmail).ValidateNullOrEmpty("MainEmail");
    }
}
