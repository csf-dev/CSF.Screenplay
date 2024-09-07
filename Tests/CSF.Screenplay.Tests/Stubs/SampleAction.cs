namespace CSF.Screenplay.Stubs;

public class SampleAction : IPerformable
{
    public string? ActorName { get; private set; }

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        ActorName = ((IHasName)actor).Name;
        return ValueTask.CompletedTask;
    }
}
