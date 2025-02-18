using CalculatorApp;

namespace ReqnrollPlugins.Autofac.Support;

internal class TestCalculatorConfiguration : ICalculatorConfiguration
{
    public bool AllowMultiply => true;
}
