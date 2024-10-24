using Newtonsoft.Json;

namespace CustomerRegistration.Application.Messaging.CustomerMainAddressRegistered.PublishEvent
{
    public class AddressEvent
    {
        public AddressEvent(
            string postalCode, 
            string state, 
            string city, 
            string neighborhood, 
            string street, 
            uint number, 
            string complement)
        {
            PostalCode = postalCode;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            Street = street;
            Number = number;
            Complement = complement;
        }

        [JsonProperty("postal_code")]
        public string PostalCode { get; private set; } = string.Empty;

        [JsonProperty("state")]
        public string State { get; private set; } = string.Empty;

        [JsonProperty("city")]
        public string City { get; private set; } = string.Empty;

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; private set; } = string.Empty; 

        [JsonProperty("street")]
        public string Street { get; private set; } = string.Empty;

        [JsonProperty("number")]
        public uint Number { get; private set; }

        [JsonProperty("complement")]
        public string Complement { get; private set; } = string.Empty;
    }
}