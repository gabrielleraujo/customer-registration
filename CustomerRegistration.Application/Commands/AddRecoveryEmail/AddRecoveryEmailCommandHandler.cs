using MediatR;
using CustomerRegistration.Domain.Models.Entities;
using CustomerRegistration.Domain.Repositories;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using CustomerRegistration.Domain.Models.ValueObject;

namespace CustomerRegistration.Application.Commands.AddRecoveryEmailCommand
{
    public class AddRecoveryEmailCommandHandler : 
        CommandHandler<AddRecoveryEmailCommandHandler>,
        IRequestHandler<AddRecoveryEmailCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly ICustomerRepository _repository;

        public AddRecoveryEmailCommandHandler(
            ILogger<AddRecoveryEmailCommandHandler> logger,
            IMediator mediator,
            ICustomerRepository repository) : base(logger)
        {
            _mediator = mediator;
            _repository = repository;
        }
        
        public async Task<ValidationResult> Handle(AddRecoveryEmailCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(AddRecoveryEmailCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var customer = await _repository.FindByAsync(x => x.Id == command.CustomerId);

            customer.AddRecoveryEmail(new Email(command.Email));

            _repository.Update(customer);
            await _repository.CommitAsync();

            _logger.LogInformation($"{nameof(AddRecoveryEmailCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}
