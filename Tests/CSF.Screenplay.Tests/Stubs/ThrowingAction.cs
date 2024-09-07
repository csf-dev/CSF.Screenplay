namespace CSF.Screenplay.Stubs;

public class ThrowingAction : IPerformable
{
    internal const string Message = "This is a sample exception";

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException(Message);
    }
}
