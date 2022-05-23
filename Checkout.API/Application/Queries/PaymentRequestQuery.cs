using MediatR;

namespace Checkout.Application.Queries;

public class PaymentRequestQuery : IRequest<PaymentRequestDto>
{
    public PaymentRequestQuery(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; set; }
}