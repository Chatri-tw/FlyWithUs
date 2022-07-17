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
    public void EnsureGetBookingApiPacts()
    {
        var pactVerifier = TestConfig.GetPactVerifierSource(_output, "FlyWithMeGetBooking");
        //Act, Assert
        pactVerifier
            .Verify();
    }
}
