
namespace Checkout.Bank;

public class PaymentRequestCommand
{
    public string CardNumber { get; set;}
    public string CardHolderName { get; set;}
    public DateTime CardExpiration { get; set;}
    public string CardSecurityNumber { get; set;}
    public int CardTypeId { get; set;}
    
}