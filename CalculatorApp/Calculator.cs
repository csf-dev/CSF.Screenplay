namespace CalculatorApp;

public class Calculator(ICalculatorConfiguration _calculatorConfiguration) : ICalculator
{
    private readonly Stack<int> _numbers = new();

    public bool HasResult => _numbers.Count > 0;

    public void Enter(int number)
    {
        _numbers.Push(number);
    }

    public void Reset()
    {
        _numbers.Clear();
    }

    public int GetResult()
    {
        return _numbers.Peek();
    }

    public void Add()
    {
        _numbers.Push(_numbers.Pop() + _numbers.Pop());
    }

    public void Multiply()
    {
        if (!_calculatorConfiguration.AllowMultiply)
            throw new InvalidOperationException("Multiplication is not allowed.");

        _numbers.Push(_numbers.Pop() * _numbers.Pop());
    }
}