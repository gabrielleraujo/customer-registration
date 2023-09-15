using System.Text.Json.Serialization;
using CustomerRegistration.Application.Validations;
using FluentValidation.Results;
using MediatR;

namespace CustomerRegistration.Application.Commands.AddClassifiedAddressCommand
{
    public class AddClassifiedAddressCommand : Command, IRequest<ValidationResult>
    {
        public AddClassifiedAddressCommand(
            Guid customerId,
            bool isMain,
            string classified,
            string postalCode, 
            string state, 
            string city, 
            string neighborhood, 
            string street, 
            uint number, 
            string complement)
        {
            CustomerId = customerId;
            IsMain = isMain;
            Classified = classified;
            PostalCode = postalCode;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            Street = street;
            Number = number;
            Complement = complement;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; private set; }

        [JsonPropertyName("classified")]
        public string Classified { get; private set; } = string.Empty;

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; private set; } = string.Empty;

        [JsonPropertyName("state")]
        public string State { get; private set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; private set; } = string.Empty;

        [JsonPropertyName("neighborhood")]
        public string Neighborhood { get; private set; } = string.Empty; 

        [JsonPropertyName("street")]
        public string Street { get; private set; } = string.Empty;

        [JsonPropertyName("number")]
        public uint Number { get; private set; }

        [JsonPropertyName("complement")]
        public string Complement { get; private set; } = string.Empty;

        public override bool IsValid() 
        {
            ValidationResult = new AddClassifiedAddressCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
