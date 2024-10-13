namespace CSF.Screenplay.AddingUp;

public static class AddingUpBuilder
{
    public static IPerformable Add(int number) => new AddTheNumber(number);
    public static IPerformable AddThreeNumbers(int number1, int number2, int number3) => new AddThreeNumbers(number1, number2, number3);
    public static IPerformable SetTheTotalTo(int number) => new SetTheNumber(number);
    public static IPerformableWithResult<int> GetTheTotal() => new GetTheNumber();
}
