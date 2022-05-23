using Checkout.Application.Commands;
using FluentValidation;

namespace Checkout.Application.Validations;

public class PaymentRequestCommandValidator : AbstractValidator<PaymentRequestCommand>
{
    public PaymentRequestCommandValidator(ILogger<PaymentRequestCommandValidator> logger)
    {
        RuleFor(command => command.CardNumber).NotEmpty().Length(12, 19);
        RuleFor(command => command.CardHolderName).NotEmpty();
        RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date");
        RuleFor(command => command.CardSecurityNumber).NotEmpty().Length(3);

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }

    private bool BeValidExpirationDate(DateTime dateTime)
    {
        return dateTime >= DateTime.UtcNow;
    }
}