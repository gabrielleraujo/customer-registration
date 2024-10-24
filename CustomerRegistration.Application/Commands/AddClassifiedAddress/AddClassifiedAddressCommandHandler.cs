using Microsoft.Extensions.Logging;
using FluentValidation.Results;
using System.Text.Json;
using MediatR;
using CustomerRegistration.Domain.Models.Entities;
using CustomerRegistration.Domain.Models.ValueObject;
using CustomerRegistration.Domain.Repositories;
using CustomerRegistration.Domain.Messaging;
using CustomerRegistration.Application.Messaging.CustomerMainAddressRegistered.PublishEvent;

namespace CustomerRegistration.Application.Commands.AddClassifiedAddressCommand
{
    public class AddClassifiedAddressCommandHandler : 
        CommandHandler<AddClassifiedAddressCommandHandler>,
        IRequestHandler<AddClassifiedAddressCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly ICustomerRepository _repository;
        private readonly IMessageBusPublisher _messageBus;  // Mensageria RabbitMQ

        public AddClassifiedAddressCommandHandler(
            ILogger<AddClassifiedAddressCommandHandler> logger,
            IMediator mediator,
            ICustomerRepository repository,
            IMessageBusPublisher messageBus) : base(logger)
        {
            _mediator = mediator;
            _repository = repository;
            _messageBus = messageBus;
        }
        
        public async Task<ValidationResult> Handle(AddClassifiedAddressCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(AddClassifiedAddressCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid\nEnd steps.");
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

            // Verificar se é o endereço principal, se nao for, nao publica evento de cadastro de endereço principal.
            var isMainAddress = classifiedAddress.IsMain;
            if (!isMainAddress)
            {
                _logger.LogInformation($"{nameof(AddClassifiedAddressCommandHandler)} step verify if address is main address: {isMainAddress}\nEnd steps.");
                return ValidationResult; 
            }

            // Publicar um evento de cadastro de cliente na fila de RabbitMQ
            var customerMainAddressRegisteredEvent = new CustomerMainAddressRegisteredEvent(
                customer.Id,
                customer.Name.First,
                customer.Name.Last,
                customer.MainEmail.Text,
                new AddressEvent(
                    classifiedAddress.Address.PostalCode,
                    classifiedAddress.Address.State,
                    classifiedAddress.Address.City,
                    classifiedAddress.Address.Neighborhood,
                    classifiedAddress.Address.Street,
                    classifiedAddress.Address.Number,
                    classifiedAddress.Address.Complement
                ),
                DateTime.UtcNow
            );

            string routingKey = "customer_main_address_registered_event_key";  // Definir a chave de roteamento adequada
            _messageBus.PublishMessage(customerMainAddressRegisteredEvent, routingKey);  // Publica a mensagem no RabbitMQ

            _logger.LogInformation($"{nameof(AddClassifiedAddressCommandHandler)} All steps was successfully completed\nEnd steps.");

            return ValidationResult;
        }
    }
}
