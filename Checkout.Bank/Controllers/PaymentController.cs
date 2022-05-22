using Microsoft.AspNetCore.Mvc;

namespace Checkout.Bank.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    public PaymentController(ILogger<PaymentController> logger) { }

    [HttpPost]
    public BankResponse Process([FromBody] PaymentRequestCommand requestCommand)
    {
        return requestCommand.CardNumber switch
        {
            "1111222233334444" => new BankResponse {ResponseType = BankResponseType.Error},
            _ => new BankResponse {ResponseType = BankResponseType.Success},
        };
    }
}

public class BankResponse
{
    public BankResponseType ResponseType { get; set; }
    public string TransactionIdentifier { get; set; }

    public BankResponse()
    {
        TransactionIdentifier = new Random().Next().ToString();
    }
}

public enum BankResponseType
{
    Success,
    Error
}