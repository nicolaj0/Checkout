using Checkout.Domain;
using Checkout.Infra;
using Checkout.Infrastructure.Services;
using MediatR;

namespace Checkout.Application.Commands;

public class PaymentRequestCommandHandler : IRequestHandler<PaymentRequestCommand, PaymentRequestResponse>
{
    private readonly IMediator _mediatorObject;
    private readonly IBankService _bankService;
    private readonly ILogger<PaymentRequestCommandHandler> _loggerMockObject;
    private readonly CheckoutContext _context;

    public PaymentRequestCommandHandler(IMediator mediatorObject, IBankService bankService, CheckoutContext context, ILogger<PaymentRequestCommandHandler> loggerMockObject)
    {
        _context = context;
        _mediatorObject = mediatorObject;
        _bankService = bankService;
        _loggerMockObject = loggerMockObject;
    }

    public async Task<PaymentRequestResponse> Handle(PaymentRequestCommand request, CancellationToken cancellationToken)
    {
        var paymentRequest = new PaymentRequest(request.CardNumber, request.CardHolderName, request.CardSecurityNumber, request.CardExpiration);
        _context.Add(paymentRequest);
        var bankResponse =await  _bankService.GetBankResponse(request);
        paymentRequest.UpdateWithBankReponse(bankResponse);
        await _context.SaveChangesAsync(cancellationToken);
        
        return bankResponse.ResponseType switch
        {
            BankResponseType.Success => new PaymentRequestResponse { Status = PaymentRequestStatus.Success, Identifier = bankResponse.TransactionIdentifier},
            _ => new PaymentRequestResponse { Status = PaymentRequestStatus.Error, Identifier = bankResponse.TransactionIdentifier }
        };
    }
}