using System.Net;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CustomerRegistration.Application.ViewModels;
using CustomerRegistration.Application.Commands.RegisterCustomerCommand;
using CustomerRegistration.Application.Commands.AddClassifiedAddressCommand;
using CustomerRegistration.Application.Commands.AddRecoveryEmailCommand;
using CustomerRegistration.Application.Queries.GetAllCustomers;
using CustomerRegistration.Application.Queries.GetCustomersById;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerRegistration.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/customer-registration")]
public class CustomerRegistrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerRegistrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Registra um cliente.")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Add([FromBody] RegisterCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsValid ? NoContent() : BadRequest();
    }
    
    [HttpPost("address")]
    [SwaggerOperation(Summary = "Registra um endereço para um cliente registrado.")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddAddress(AddClassifiedAddressCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsValid ? NoContent() : BadRequest();
    }

    [HttpPatch("recovery-email")]
    [SwaggerOperation(Summary = "Registra um email de recuperação para um cliente registrado.")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddRecoveryEmail([FromBody] AddRecoveryEmailCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsValid ? NoContent() : BadRequest();
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Busca todos os clientes registrados.")]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(result);
    }

    [HttpGet("detail")]
    [SwaggerOperation(Summary = "Busca o detalhe de um cliente registrado.")]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCustomerDetail([FromQuery] Guid customerId)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(customerId));
        return result != null ? Ok(result) : NotFound();
    }
}
