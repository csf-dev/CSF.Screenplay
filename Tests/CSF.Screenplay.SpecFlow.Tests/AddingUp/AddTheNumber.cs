namespace CSF.Screenplay.AddingUp;

public class AddTheNumber(int number) : IPerformable, ICanReport
{
    public string GetReportFragment(IHasName actor) => $"{actor.Name} adds {number} to the running total";

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var ability = actor.GetAbility<AddNumbers>();
        ability.Add(number);
        return default;
    }
}
