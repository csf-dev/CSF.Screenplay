namespace CSF.Screenplay.AddingUp;

public class AddNumbers : ICanReport
{
    int currentNumber;

    public string GetReportFragment(IHasName actor) => $"{actor.Name} is able to add numbers to a running total";

    public void Add(int number) => currentNumber += number;

    public void Set(int number) => currentNumber = number;

    public int Get() => currentNumber;
}