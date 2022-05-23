using Checkout.Application;
using Checkout.Application.Commands;
using Checkout.Domain;
using Checkout.Infra;
using Checkout.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
namespace Checkout.Tests;

public class PaymentRequestHandlerTest
{
    private readonly Mock<IBankService> _bankService;
    private readonly Mock<IMediator> _mediator;

    public PaymentRequestHandlerTest()
    {
        _mediator = new Mock<IMediator>();
        _bankService = new Mock<IBankService>();
    }

    [Fact]
    public async Task Handle_return_success_response_if_bank_returns_success()
    {
        var fakePaymentrequestCmd = FakePaymentRequest(new Dictionary<string, object>
            { ["cardExpiration"] = DateTime.Now.AddYears(1) });
        var transactionIdentifier = "1234";
        
        _bankService.Setup(svc => svc.GetBankResponse(fakePaymentrequestCmd)).ReturnsAsync(new BankResponse{ResponseType = BankResponseType.Success, TransactionIdentifier = transactionIdentifier});
        
        var mockSet = new Mock<DbSet<PaymentRequest>>();

        var mockContext = new Mock<CheckoutContext>();
        mockContext.Setup(m => m.PaymentRequests).Returns(mockSet.Object);
        var loggerMock = new Mock<ILogger<PaymentRequestCommandHandler>>();
        
        //Act
        var handler = new PaymentRequestCommandHandler(_mediator.Object, _bankService.Object, mockContext.Object, loggerMock.Object);
        var result = await handler.Handle(fakePaymentrequestCmd, new CancellationToken());

        //Assert
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        Assert.Equal(PaymentRequestStatus.Success, result.Status);
        Assert.Equal(result.Identifier, transactionIdentifier);
    }
    
    
    [Fact]
    public async Task Handle_return_failure_response_if_bank_returns_failure()
    {
        var fakePaymentrequestCmd = FakePaymentRequest(new Dictionary<string, object>
            { ["cardExpiration"] = DateTime.Now.AddYears(1) });
        var transactionIdentifier = "1234";
        
        _bankService.Setup(svc => svc.GetBankResponse(fakePaymentrequestCmd)).ReturnsAsync(new BankResponse{ResponseType = BankResponseType.Error, TransactionIdentifier = transactionIdentifier});
        
        var mockSet = new Mock<DbSet<PaymentRequest>>();

        var mockContext = new Mock<CheckoutContext>();
        mockContext.Setup(m => m.PaymentRequests).Returns(mockSet.Object);
        var loggerMock = new Mock<ILogger<PaymentRequestCommandHandler>>();
        
        //Act
        var handler = new PaymentRequestCommandHandler(_mediator.Object, _bankService.Object, mockContext.Object, loggerMock.Object);
        var result = await handler.Handle(fakePaymentrequestCmd, new CancellationToken());

        //Assert
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        Assert.Equal(PaymentRequestStatus.Error, result.Status);
        Assert.Equal(result.Identifier, transactionIdentifier);

    }

    private PaymentRequestCommand FakePaymentRequest(Dictionary<string, object> args = null)
    {
        return new PaymentRequestCommand(
            cardNumber: args != null && args.ContainsKey("cardNumber") ? (string)args["cardNumber"] : "1234",
            cardExpiration: args != null && args.ContainsKey("cardExpiration") ? (DateTime)args["cardExpiration"] : DateTime.MinValue,
            cardSecurityNumber: args != null && args.ContainsKey("cardSecurityNumber") ? (string)args["cardSecurityNumber"] : "123",
            cardHolderName: args != null && args.ContainsKey("cardHolderName") ? (string)args["cardHolderName"] : "XXX");
    }
}