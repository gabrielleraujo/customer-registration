using CustomerRegistration.Application.ViewModels;
using MediatR;

namespace CustomerRegistration.Application.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery: IRequest<IEnumerable<CustomerViewModel>>
    {
    }
}