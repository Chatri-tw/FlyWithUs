using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FlyWithUsConsumer;
using FlyWithUsConsumerTest.Config;
using PactNet;
using PactNet.Matchers;
using Xunit;
using Xunit.Abstractions;

namespace FlyWithUsConsumerTest;

public class GetBookingConsumerTest
{
    private const string ConsumerName = "Avenger";
    private const string ProviderName = "GetBooking";
    private readonly IPactBuilderV3 _pactBuilder;
    
    public GetBookingConsumerTest(ITestOutputHelper output)
    {
        _pactBuilder = TestConfig.GetPactBuilder(output, ConsumerName, ProviderName);
    }

    [Fact]
    public async Task GetBooking_WhenPnrExisted_ReturnBooking()
    {
        _pactBuilder.UponReceiving("blah blah blah")
            // .Given("PNR existed")
            .WithRequest(HttpMethod.Get, "/booking")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(new
            {
                PNR = Match.Type("ABC12")
            });
        
        await _pactBuilder.VerifyAsync(async ctx =>
        {
            // Act
            var client = new Client(ctx.MockServerUri.ToString(), new HttpClient());
            await client.BookingGETAsync();
        });
    }
}