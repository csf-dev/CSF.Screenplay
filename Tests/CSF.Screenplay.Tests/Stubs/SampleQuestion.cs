namespace CSF.Screenplay.Stubs;

public class SampleQuestion : IPerformableWithResult
{
    public ValueTask<object> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult<object>(5);
    }
}
