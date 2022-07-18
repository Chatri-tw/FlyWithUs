using System.Collections.Generic;
using System.IO;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace KioskConsumerTest.Config;

public class TestConfig
{
    public static IPactBuilderV3 GetPactBuilder(ITestOutputHelper output, string consumerName, string providerName)
    {
        var pact = Pact.V3(consumerName, providerName, new PactConfig
        {
            LogLevel = PactLogLevel.Debug,
            PactDir = $"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName}{Path.DirectorySeparatorChar}pacts",
            Outputters = new List<IOutput>
            {
                // NOTE: PactNet defaults to a ConsoleOutput, however
                // xUnit 2 does not capture the console output, so this
                // sample creates a custom xUnit outputter. You will
                // have to do the same in xUnit projects.
                new XUnitOutput(output)
            },
        });

        return pact.UsingNativeBackend();
    }
}