namespace CSF.Screenplay.AddingUp;

public class GetTheNumber() : IPerformableWithResult<int>, ICanReport
{
    public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} gets the running total", actor);

    ValueTask<int> IPerformableWithResult<int>.PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken)
    {
        var ability = actor.GetAbility<AddNumbers>();
        return ValueTask.FromResult(ability.Get());
    }
}