namespace CalculatorApp;

public interface ICalculator
{
    bool HasResult { get; }
    void Enter(int number);
    void Reset();
    int GetResult();
    void Add();
    void Multiply();
}