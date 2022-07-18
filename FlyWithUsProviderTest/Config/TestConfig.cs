using System;
using System.Collections.Generic;
using System.IO;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace FlyWithUsProviderTest.Config;

public static class TestConfig
{
    private static readonly string PactRoot = Path.Combine("..",
        "..",
        "..",
        "..",
        $"pacts{Path.DirectorySeparatorChar}");

    private static readonly TimeSpan RequestTimeout = new(0, 0, 30);

    private static string GetPactPath(string pactName)
    {
        return  $"{PactRoot}{pactName}.json";
    }

    public static IPactVerifierSource GetPactVerifierSource(ITestOutputHelper output, string providerName, string consumerName)
    {
        var config = new PactVerifierConfig
        {
            LogLevel = PactLogLevel.Debug,
            Outputters = new List<IOutput>
            {
                // NOTE: PactNet defaults to a ConsoleOutput, however
                // xUnit 2 does not capture the console output, so this
                // sample creates a custom xUnit outputter. You will
                // have to do the same in xUnit projects.
                new XUnitOutput(output)
            },
        };
        
        IPactVerifier pactVerifier = new PactVerifier(config);
        return pactVerifier
            .ServiceProvider(providerName, new Uri("http://localhost:5012"))
            .WithPactBrokerSource(new Uri("http://localhost:9292"),
                options =>
                {
                    options.ConsumerVersionSelectors(new ConsumerVersionSelector { Consumer = consumerName, Latest = true });
                    options.PublishResults("0.0.3", publishOptions =>
                    {
                        publishOptions.ProviderBranch("main").BuildUri(
                            new Uri("https://example.com/"));
                    });
                });
    }
}
