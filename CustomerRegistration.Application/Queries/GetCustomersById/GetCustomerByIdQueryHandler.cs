using CustomerRegistration.Application.ViewModels;
using CustomerRegistration.Domain.Repositories;
using MediatR;

namespace CustomerRegistration.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByIdQueryHandler(
            ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerViewModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.FindByAsync(x => x.Id == request.CustomerId);

            if (customer == null) return null;

            var purchaseViewModel = CustomerViewModel.MapFromDomain(customer);

            return purchaseViewModel;
        }
    }
}
