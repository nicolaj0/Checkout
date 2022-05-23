using System.Linq;
using AutoMapper;
using Checkout.Application;
using Checkout.Application.Commands;
using Checkout.Application.Queries;
using Checkout.Domain;
using Checkout.Infra;
using Checkout.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
namespace Checkout.Tests;

public class PaymentRequestQueryTest
{
    private readonly Mock<IMediator> _mediator;

    public PaymentRequestQueryTest()
    {
        _mediator = new Mock<IMediator>();
    }

    [Fact]
    public async Task Return_Transaction_Detail_With_Masked_Card_Detail_When_Transaction_Exists()
    {
        var requestCardNumber = "1111222233334444";
        var requestCardExpiration = DateTime.Now;
        var paymentRequest = new PaymentRequest(requestCardNumber, "name", "123", requestCardExpiration);
        var transactionIdentifier = "identifier";
        paymentRequest.UpdateWithBankReponse(new BankResponse{ResponseType = BankResponseType.Success, TransactionIdentifier = transactionIdentifier});
        var data = new List<PaymentRequest>
        {
            paymentRequest,
        }.AsQueryable();

        var mockSet = new Mock<DbSet<PaymentRequest>>();
        mockSet.As<IQueryable<PaymentRequest>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<PaymentRequest>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<PaymentRequest>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<PaymentRequest>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<CheckoutContext>();
        mockContext.Setup(c => c.PaymentRequests).Returns(mockSet.Object);

        var query = new PaymentRequestQuery(transactionIdentifier);
        var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentRequestProfile>());
        
        var loggerMock = new Mock<ILogger<PaymentRequestQueryHandler>>();
        
        //Act
        var handler = await new PaymentRequestQueryHandler(mockContext.Object, loggerMock.Object, config.CreateMapper()).Handle(query, new CancellationToken());

        //Assert
        Assert.Equal("************4444", handler.MaskedCcNumber);
        Assert.Equal("name", handler.RequestCardHolderName);
        Assert.Equal("123", handler.RequestCardSecurityNumber);
        Assert.Equal(requestCardExpiration, handler.RequestCardExpiration);
        
    }
    
    
    
}