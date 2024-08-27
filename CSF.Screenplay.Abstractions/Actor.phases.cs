using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    public sealed partial class Actor : ICanPerformGiven, ICanPerformWhen, ICanPerformThen
    {
        ValueTask ICanPerformGiven.WasAbleTo(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Given);

        ValueTask<object> ICanPerformGiven.WasAbleTo(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Given);

        ValueTask<T> ICanPerformGiven.WasAbleTo<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Given);

        ValueTask ICanPerformWhen.AttemptsTo(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.When);

        ValueTask<object> ICanPerformWhen.AttemptsTo(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.When);

        ValueTask<T> ICanPerformWhen.AttemptsTo<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.When);

        ValueTask ICanPerformThen.Should(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Then);

        ValueTask<object> ICanPerformThen.Should(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Then);

        ValueTask<T> ICanPerformThen.Should<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Then);
    }
}