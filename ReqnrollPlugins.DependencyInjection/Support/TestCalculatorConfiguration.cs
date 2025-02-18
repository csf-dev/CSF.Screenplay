using CalculatorApp;

namespace ReqnrollPlugins.DependencyInjection.Support;

internal class TestCalculatorConfiguration : ICalculatorConfiguration
{
    public bool AllowMultiply => true;
}
