namespace CSF.Screenplay.AddingUp;

public class AddNumbers : ICanReport
{
    int currentNumber;

    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} is able to add numbers to a running total", actor);

    public void Add(int number) => currentNumber += number;

    public void Set(int number) => currentNumber = number;

    public int Get() => currentNumber;
}