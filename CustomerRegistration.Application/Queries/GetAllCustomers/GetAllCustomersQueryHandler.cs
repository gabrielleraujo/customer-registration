using CustomerRegistration.Application.ViewModels;
using CustomerRegistration.Domain.Models.Entities;
using CustomerRegistration.Domain.Repositories;
using MediatR;

namespace CustomerRegistration.Application.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerViewModel>>
    {
        private readonly ICustomerRepository _repository;

        public GetAllCustomersQueryHandler(
            ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerViewModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAllAsync();

            if (customers == null) return new List<CustomerViewModel>();

            var customersViewModel = CustomerViewModel.MapFromDomain(customers);

            return customersViewModel;
        }
    }
}
