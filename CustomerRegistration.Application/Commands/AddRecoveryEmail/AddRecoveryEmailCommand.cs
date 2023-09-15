using System.Text.Json.Serialization;
using CustomerRegistration.Application.Validations;
using FluentValidation.Results;
using MediatR;

namespace CustomerRegistration.Application.Commands.AddRecoveryEmailCommand
{
    public class AddRecoveryEmailCommand : Command, IRequest<ValidationResult>
    {
        public AddRecoveryEmailCommand(
            Guid customerId,
            string email)
        {
            CustomerId = customerId;
            Email = email;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }

        [JsonPropertyName("email")]
        public string Email { get; private set; } = string.Empty;

        public override bool IsValid() 
        {
            ValidationResult = new AddRecoveryEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
