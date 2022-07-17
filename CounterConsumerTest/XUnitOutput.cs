using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace FlyWithUsConsumerTest;

public class XUnitOutput : IOutput
{
    private readonly ITestOutputHelper _output;

    public XUnitOutput(ITestOutputHelper output)
    {
        this._output = output;
    }

    public void WriteLine(string line)
    {
        this._output.WriteLine(line);
    }
}