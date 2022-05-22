
using CheckoutClient;

namespace TestSuites
{
    public class PaymentRequestSuite
    {
        private HttpClient _httpClient;

        public PaymentRequestSuite()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Environment.GetEnvironmentVariable("BASE_URL") ?? DEFAULT_BASE_URL) };
        }

        protected const string DEFAULT_BASE_URL = "http://localhost:5088";

        [Fact]
        public async Task T000_RequestPayment_InvalidRequest_Returns_BadRequest()
        {
            var client = new CheckoutClient.CheckoutClient(baseUrl: null, _httpClient);

            await Assert.ThrowsAsync<ApiException>(async () => await client.PaymentRequestPostAsync(new PaymentRequestCommand
            {
                CardHolderName = "test",
                CardSecurityNumber = "123",
            }));
        }
        
        
        [Fact]
        public async Task T001_RequestPayment_Bank_Returns_Error()
        {
            var client = new CheckoutClient.CheckoutClient(baseUrl: null, _httpClient);
            var res = await client.PaymentRequestPostAsync(new PaymentRequestCommand
            {
                CardExpiration = DateTimeOffset.UtcNow.AddDays(1),
                CardNumber = "1111222233334444",
                CardHolderName = "test",
                CardSecurityNumber = "123",
            });

            Assert.Equal(CheckoutClient.PaymentRequestStatus._2, res.Status);

        }
        
        [Fact]
        public async Task T002_RequestPayment_Bank_Returns_Success()
        {
            var client = new CheckoutClient.CheckoutClient(baseUrl: null, _httpClient);
            var res = await client.PaymentRequestPostAsync(new PaymentRequestCommand
            {
                CardExpiration = DateTimeOffset.UtcNow.AddDays(1),
                CardNumber = "1111111111111111",
                CardHolderName = "test",
                CardSecurityNumber = "123",
            });

            Assert.Equal(CheckoutClient.PaymentRequestStatus._1, res.Status);

        }
        
        [Fact]
        public async Task T003_Reteieve_InvalidRequest_Returns_BadRequest()
        {
            var client = new CheckoutClient.CheckoutClient(baseUrl: null, _httpClient);

            await Assert.ThrowsAsync<ApiException>(async () => await client.PaymentRequestGetAsync(null));
        }
        
        [Fact]
        public async Task T004_Reteieve_With_Unknown_Identifier_Returns_Not_Found()
        {
            var client = new CheckoutClient.CheckoutClient(baseUrl: null, _httpClient);

            await Assert.ThrowsAsync<ApiException>(async () => await client.PaymentRequestGetAsync("unknown"));
        }
        
        [Fact]
        public async Task T005_RequestPayment_And_Retrieve_Details_Returns_Datails_With_Masked_CC_Number()
        {
            var client = new CheckoutClient.CheckoutClient(baseUrl: null, _httpClient);
            var res = await client.PaymentRequestPostAsync(new PaymentRequestCommand
            {
                CardExpiration = DateTimeOffset.UtcNow.AddDays(1),
                CardNumber = "1111111111111111",
                CardHolderName = "test",
                CardSecurityNumber = "123",
            });

            var tra = await client.PaymentRequestGetAsync(res.Identifier);
            
            Assert.Equal("************1111", tra.MaskedCcNumber);
        }


    }
}
