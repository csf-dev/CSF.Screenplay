namespace CSF.Screenplay.AddingUp;

public class SetTheNumber(int number) : IPerformable, ICanReport
{
    public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} sets the running total to {Number}", actor, number);

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var ability = actor.GetAbility<AddNumbers>();
        ability.Set(number);
        return default;
    }
}
