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

    public static IPactVerifierSource GetPactVerifierSource(ITestOutputHelper output, string pactName)
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
        //Check if this code is running in CI/CD pipeline
        return pactVerifier
            .ServiceProvider("FlyWithMeProvider", new Uri("http://localhost:5012"))
            .WithFileSource(new FileInfo(GetPactPath(pactName)))
            .WithRequestTimeout(RequestTimeout);
    }
}
