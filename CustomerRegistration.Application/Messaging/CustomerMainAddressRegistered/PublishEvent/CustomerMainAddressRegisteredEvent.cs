using Newtonsoft.Json;

namespace CustomerRegistration.Application.Messaging.CustomerMainAddressRegistered.PublishEvent
{
    public class CustomerMainAddressRegisteredEvent
    {
        public CustomerMainAddressRegisteredEvent(
            Guid customerId,
            string firstName,
            string lastName,
            string email,
            AddressEvent address,
            DateTime timestamp)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Timestamp = timestamp;
        }

        [JsonProperty("customer_id")]
        public Guid CustomerId { get; private set; }

        [JsonProperty("first_name")]
        public string FirstName { get; private set; } = string.Empty;

        [JsonProperty("last_name")]
        public string LastName { get; private set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; private set; } = string.Empty;

        [JsonProperty("address")]
        public AddressEvent Address { get; private set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; private set; }
    }
}