namespace Checkout.Application;

public record PaymentRequestResponse
{
    public PaymentRequestStatus Status { get; set; }
    public string Identifier { get; set; }
}