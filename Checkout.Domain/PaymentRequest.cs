using Checkout.Application;

namespace Checkout.Domain;

public class PaymentRequest
{
    private PaymentRequest()
    {
    }
    
    public int Id { get; private set;}
    public string? Identifier { get; private set; }

    public string RequestCardNumber { get; private set; }
    public string RequestCardHolderName { get; private set; }
    public string RequestCardSecurityNumber { get; private set; }
    public DateTime RequestCardExpiration { get; private set; }
    public PaymentRequestStatus Status { get; set; }
    public string MaskedCcNumber => $"************{RequestCardNumber.Trim().Substring(12, 4)}";


    public PaymentRequest(string requestCardNumber, string requestCardHolderName, string requestCardSecurityNumber, DateTime requestCardExpiration)
    {
        RequestCardNumber = requestCardNumber;
        RequestCardHolderName = requestCardHolderName;
        RequestCardSecurityNumber = requestCardSecurityNumber;
        RequestCardExpiration = requestCardExpiration;
    }

    public void UpdateWithBankReponse(BankResponse bankResponse)
    {
        Status = bankResponse.ResponseType == BankResponseType.Success ? PaymentRequestStatus.Success : PaymentRequestStatus.Error;
        Identifier = bankResponse.TransactionIdentifier;
    }

}