namespace Checkout.Application.Queries;

public class PaymentRequestDto
{
    public string Identifier { get; set; }
    public string RequestCardNumber { get; set; }
    public string RequestCardHolderName { get; set; }    
    public string RequestCardSecurityNumber { get; set; }
    public string RequestCardTypeId { get; set; }    
    public DateTime RequestCardExpiration { get; set; }
    public int Status { get; set; }    
    public string MaskedCcNumber { get; set; }
}