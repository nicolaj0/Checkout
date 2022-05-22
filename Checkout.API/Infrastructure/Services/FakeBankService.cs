using Checkout.Application;
using Checkout.Application.Commands;
using Checkout.Domain;

namespace Checkout.Infrastructure.Services;

public class FakeBankService : IBankService
{
    public Task<BankResponse> GetBankResponse(PaymentRequestCommand paymentRequestCommand)
    {
        var identifier = new Random().Next().ToString();
        return paymentRequestCommand.CardNumber switch
        {
            "1111222233334444" => Task.FromResult(new BankResponse{ResponseType = BankResponseType.Error, TransactionIdentifier = identifier}),
            _ => Task.FromResult(new BankResponse{ResponseType = BankResponseType.Success, TransactionIdentifier = identifier}),
        };
    }
}