using Checkout.Application.Commands;
using Checkout.Application.Queries;
using FluentValidation;

namespace Checkout.Application.Validations;

public class PaymentRequestQueryValidator : AbstractValidator<PaymentRequestQuery>
{
    public PaymentRequestQueryValidator(ILogger<PaymentRequestCommandValidator> logger)
    {
        RuleFor(command => command.Identifier).NotEmpty();

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}