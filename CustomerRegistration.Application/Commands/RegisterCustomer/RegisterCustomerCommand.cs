using System.Text.Json.Serialization;
using CustomerRegistration.Application.Validations;
using FluentValidation.Results;
using MediatR;

namespace CustomerRegistration.Application.Commands.RegisterCustomerCommand
{
    public class RegisterCustomerCommand : Command, IRequest<ValidationResult>
    {
        public RegisterCustomerCommand(
            string firstName,
            string lastName, 
            string cellPhone, 
            string mainEmail)
        {
            FirstName = firstName;
            LastName = lastName;
            CellPhone = cellPhone;
            MainEmail = mainEmail;
        }

        [JsonPropertyName("first_name")]
        public string FirstName { get; private set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; private set; }

        [JsonPropertyName("cell_phone")]
        public string CellPhone { get; private set; }

        [JsonPropertyName("main_email")]
        public string MainEmail { get; private set; }

        public override bool IsValid() 
        {
            ValidationResult = new RegisterCustomerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
