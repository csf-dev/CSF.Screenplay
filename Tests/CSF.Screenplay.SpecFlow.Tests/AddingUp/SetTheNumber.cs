namespace CSF.Screenplay.AddingUp;

public class SetTheNumber(int number) : IPerformable, ICanReport
{
    public string GetReportFragment(IHasName actor) => $"{actor.Name} sets the running total to {number}";

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var ability = actor.GetAbility<AddNumbers>();
        ability.Set(number);
        return default;
    }
}
