using System.Text.Json.Serialization;
using CustomerRegistration.Application.ViewModels;
using MediatR;

namespace CustomerRegistration.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQuery: IRequest<CustomerViewModel?>
    {
        public GetCustomerByIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }
    }
}
