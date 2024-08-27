using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    public sealed partial class Actor : ICanPerformGiven, ICanPerformWhen, ICanPerformThen
    {
        ValueTask ICanPerformGiven.WasAbleTo(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.Given, cancellationToken);

        ValueTask<object> ICanPerformGiven.WasAbleTo(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.Given, cancellationToken);

        ValueTask<T> ICanPerformGiven.WasAbleTo<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.Given, cancellationToken);

        ValueTask ICanPerformWhen.AttemptsTo(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.When, cancellationToken);

        ValueTask<object> ICanPerformWhen.AttemptsTo(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.When, cancellationToken);

        ValueTask<T> ICanPerformWhen.AttemptsTo<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.When, cancellationToken);

        ValueTask ICanPerformThen.Should(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.Then, cancellationToken);

        ValueTask<object> ICanPerformThen.Should(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.Then, cancellationToken);

        ValueTask<T> ICanPerformThen.Should<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, PerformancePhase.Then, cancellationToken);
    }
}