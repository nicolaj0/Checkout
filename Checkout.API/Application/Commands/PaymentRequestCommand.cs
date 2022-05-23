using MediatR;

namespace Checkout.Application.Commands;

public class PaymentRequestCommand : IRequest<PaymentRequestResponse>
{
    public string CardNumber { get; }
    public string CardHolderName { get; }
    public DateTime CardExpiration { get; }
    public string CardSecurityNumber { get; }

    public PaymentRequestCommand(string cardNumber, string cardHolderName, DateTime cardExpiration,
        string cardSecurityNumber)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        CardExpiration = cardExpiration;
        CardSecurityNumber = cardSecurityNumber;
    }
}