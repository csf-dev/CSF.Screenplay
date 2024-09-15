namespace CSF.Screenplay.AddingUp;

public static class AddingUpBuilder
{
    public static IPerformable Add(int number) => new AddTheNumber(number);
    public static IPerformable SetTheTotalTo(int number) => new SetTheNumber(number);
    public static IPerformableWithResult<int> GetTheTotal() => new GetTheNumber();
}
