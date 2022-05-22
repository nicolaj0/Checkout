using System.Text.Json.Serialization;
using Checkout.Application;

namespace Checkout.Domain;

public class BankResponse
{
    public BankResponseType ResponseType { get;  set; }
    public string TransactionIdentifier { get;  set; }

    
  
}