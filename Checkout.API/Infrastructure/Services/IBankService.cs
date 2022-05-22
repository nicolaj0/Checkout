using Checkout.Application;
using Checkout.Application.Commands;
using Checkout.Domain;

namespace Checkout.Infrastructure.Services;

public interface IBankService
{
    Task<BankResponse> GetBankResponse(PaymentRequestCommand paymentRequestCommand);
}