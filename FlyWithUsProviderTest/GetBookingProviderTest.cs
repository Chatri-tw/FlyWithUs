using FlyWithUsProviderTest.Config;
using Xunit;
using Xunit.Abstractions;

namespace FlyWithUsProviderTest;

public class GetBookingProviderTest
{
    
    private readonly ITestOutputHelper _output;

    public GetBookingProviderTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void EnsureEventApiHonoursPactWithKiosk()
    {
        var pactVerifier = TestConfig.GetPactVerifierSource(_output, "FlyWithMeGetBooking", "Kiosk");
        //Act, Assert
        pactVerifier
            .Verify();
    }
    
    [Fact]
    public void EnsureEventApiHonoursPactWithCounterCheckin()
    {
        var pactVerifier = TestConfig.GetPactVerifierSource(_output, "FlyWithMeGetBooking", "CounterCheckin");
        //Act, Assert
        pactVerifier
            .Verify();
    }
}
