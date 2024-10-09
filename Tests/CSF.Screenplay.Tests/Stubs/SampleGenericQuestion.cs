namespace CSF.Screenplay.Stubs;

public class SampleGenericQuestion : IPerformableWithResult<string>
{
    public ValueTask<string> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var actorName = ((IHasName) actor).Name;
        return ValueTask.FromResult(actorName);
    }
}
