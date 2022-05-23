using AutoMapper;
using Checkout.Application.Queries;
using Checkout.Domain;

public class PaymentRequestProfile : Profile
{
    public PaymentRequestProfile()
    {
        CreateMap<PaymentRequest, PaymentRequestDto>();
    }
}