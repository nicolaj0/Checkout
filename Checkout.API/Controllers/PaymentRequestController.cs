using Checkout.Application;
using Checkout.Application.Commands;
using Checkout.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Checkout.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentRequestController : ControllerBase
{
    private readonly ILogger<PaymentRequestController> _logger;
    private readonly IMediator _mediator;


    public PaymentRequestController(IMediator mediator, ILogger<PaymentRequestController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<PaymentRequestResponse> Post([FromBody] PaymentRequestCommand command)
    {
        _logger.LogInformation(
            "----- Sending command: {CommandName}  ({@Command})",
            command.GetGenericTypeName(),
            command);
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<PaymentRequestDto> RetrievePaymentRequest(string identifier)
    {
        var query = new PaymentRequestQuery(identifier);
        
        
        _logger.LogInformation(
            "----- Sending query: {RequestName}  ({@Query})",
            query.GetGenericTypeName(),
            query);
        return await _mediator.Send(query);
    }
   
   
}