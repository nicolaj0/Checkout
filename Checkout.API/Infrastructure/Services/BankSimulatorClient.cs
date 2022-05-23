using Checkout.Application.Commands;
using Checkout.Domain;

namespace Checkout.Infrastructure.Services;

public class BankSimulatorClient : IBankService
{
    private readonly IBankSimulatorClient _client1;

    public BankSimulatorClient(IBankSimulatorClient client)
    {
        _client1 = client;
    }

    public async Task<BankResponse> GetBankResponse(PaymentRequestCommand paymentRequestCommand)
    {
        return  await _client1.GetBankResponse(paymentRequestCommand);
    }
}