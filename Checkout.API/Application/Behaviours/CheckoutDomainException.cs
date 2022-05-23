using FluentValidation;

namespace Checkout.Application.Behaviours;

public class CheckoutDomainException : Exception
{
    public CheckoutDomainException()
    { }

    public CheckoutDomainException(string message)
        : base(message)
    { }

    public CheckoutDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}