using Checkout.Application;
using Checkout.Application.Commands;
using Checkout.Domain;
using Refit;

namespace Checkout.Infrastructure.Services;

public interface IBankSimulatorClient
{
    [Post("/Payment")]
    Task<BankResponse> GetBankResponse(PaymentRequestCommand command);
}