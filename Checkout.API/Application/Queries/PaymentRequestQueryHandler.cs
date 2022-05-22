using System.ComponentModel.Design;
using AutoMapper;
using Azure;
using Checkout.Application.Behaviours;
using Checkout.Application.Commands;
using Checkout.Domain;
using Checkout.Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Application.Queries;

public class PaymentRequestQueryHandler : IRequestHandler<PaymentRequestQuery, PaymentRequestDto>
{
    private readonly CheckoutContext _context;
    private readonly ILogger<PaymentRequestQueryHandler> _logger;
    private readonly IMapper _mapper;

    public PaymentRequestQueryHandler(CheckoutContext context, ILogger<PaymentRequestQueryHandler> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _context = context;
    }

    public Task<PaymentRequestDto> Handle(PaymentRequestQuery request, CancellationToken cancellationToken)
    {
        var transaction = _context.PaymentRequests.AsNoTracking().FirstOrDefault(r => r.Identifier == request.Identifier);

        if (transaction == null)
        {
            throw new CheckoutDomainException("Unknown transaction reference");
        }

        var result = _mapper.Map<PaymentRequestDto>(transaction);
      
        return Task.FromResult(result);
    }
}