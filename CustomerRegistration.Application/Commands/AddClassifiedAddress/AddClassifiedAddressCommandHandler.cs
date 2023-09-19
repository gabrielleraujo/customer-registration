using MediatR;
using CustomerRegistration.Domain.Models.Entities;
using CustomerRegistration.Domain.Repositories;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using CustomerRegistration.Domain.Models.ValueObject;

namespace CustomerRegistration.Application.Commands.AddClassifiedAddressCommand
{
    public class AddClassifiedAddressCommandHandler : 
        CommandHandler<AddClassifiedAddressCommandHandler>,
        IRequestHandler<AddClassifiedAddressCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly ICustomerRepository _repository;

        public AddClassifiedAddressCommandHandler(
            ILogger<AddClassifiedAddressCommandHandler> logger,
            IMediator mediator,
            ICustomerRepository repository) : base(logger)
        {
            _mediator = mediator;
            _repository = repository;
        }
        
        public async Task<ValidationResult> Handle(AddClassifiedAddressCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(AddClassifiedAddressCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var customer = await _repository.FindByAsync(x => x.Id == command.CustomerId);

            if (customer == null)
            {
                AddError("Customer not found.");
                return ValidationResult;
            }

            var classifiedAddress = new ClassifiedAddress(
                id: Guid.NewGuid(),
                customerId: command.CustomerId,
                isMain: command.IsMain,
                classified: command.Classified,
                address: new Address(
                    command.PostalCode,
                    command.State,
                    command.City,
                    command.Neighborhood,
                    command.Street,
                    command.Number,
                    command.Complement
                )
            );

            customer.AddAddress(classifiedAddress);

            _repository.Update(customer);
            await _repository.CommitAsync();

            _logger.LogInformation($"{nameof(AddClassifiedAddressCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}
