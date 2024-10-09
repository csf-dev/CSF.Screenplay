namespace CSF.Screenplay.AddingUp;

public class AddTheNumber(int number) : IPerformable, ICanReport
{
    public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} adds {Aumber} to the running total", actor, number);

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var ability = actor.GetAbility<AddNumbers>();
        ability.Add(number);
        return default;
    }
}
