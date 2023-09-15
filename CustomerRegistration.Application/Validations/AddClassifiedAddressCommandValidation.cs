using CustomerRegistration.Application.Commands.AddClassifiedAddressCommand;
using CustomerRegistration.Application.Validations.Utils;
using FluentValidation;

namespace CustomerRegistration.Application.Validations;

public class AddClassifiedAddressCommandValidation : AbstractValidator<AddClassifiedAddressCommand>
{
    public AddClassifiedAddressCommandValidation()
    {
        RuleFor(c => c.CustomerId).ValidateNullOrEmpty("CustomerId");
        RuleFor(c => c.Classified).ValidateNullOrEmpty("Classified");
        RuleFor(c => c.PostalCode).ValidateNullOrEmpty("PostalCode");
        RuleFor(c => c.State).ValidateNullOrEmpty("State");
        RuleFor(c => c.City).ValidateNullOrEmpty("City");
        RuleFor(c => c.Neighborhood).ValidateNullOrEmpty("Neighborhood");
        RuleFor(c => c.Street).ValidateNullOrEmpty("Street");
        RuleFor(c => c.Complement).ValidateNullOrEmpty("Complement");
    }
}

