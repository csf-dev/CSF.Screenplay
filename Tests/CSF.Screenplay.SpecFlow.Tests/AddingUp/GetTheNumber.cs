namespace CSF.Screenplay.AddingUp;

public class GetTheNumber() : IPerformableWithResult<int>, ICanReport
{
    public string GetReportFragment(IHasName actor) => $"{actor.Name} gets the running total";

    ValueTask<int> IPerformableWithResult<int>.PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken)
    {
        var ability = actor.GetAbility<AddNumbers>();
        return ValueTask.FromResult(ability.Get());
    }
}