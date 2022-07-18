using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MobileAppConsumer;
using MobileAppConsumerTest.Config;
using PactNet;
using PactNet.Matchers;
using Xunit;
using Xunit.Abstractions;

namespace MobileAppConsumerTest;

public class GetBookingConsumerTest
{
    private readonly IPactBuilderV3 _pactBuilder;
    
    public GetBookingConsumerTest(ITestOutputHelper output)
    {
        _pactBuilder = TestConfig.GetPactBuilder(output, "MobileApp", "FlyWithMeGetBooking");
    }

    [Fact]
    public async Task GetBooking_WhenPnrExisted_ReturnBooking()
    {
        _pactBuilder.UponReceiving("A GET request to fetch a booking")
            // .Given("PNR existed")
            .WithRequest(HttpMethod.Get, "/booking/BNK48")
            .WithQuery("firstname", "Alice")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(new
            {
                pnr = Match.Type("BNK48"),
                firstname = "Alice",
                departureTime = Match.Type(new DateTimeOffset())
            });
        
        await _pactBuilder.VerifyAsync(async ctx =>
        {
            // Act
            var client = new FlyWithUsClient(ctx.MockServerUri);
            await client.GetBookingByFirstName("BNK48", "Alice");
        });
    }
}