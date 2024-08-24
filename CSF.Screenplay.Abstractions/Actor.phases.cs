using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    public partial class Actor : ICanPerformGiven, ICanPerformWhen, ICanPerformThen
    {
        Task ICanPerformGiven.WasAbleTo(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Given);

        Task<object> ICanPerformGiven.WasAbleTo(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Given);

        Task<T> ICanPerformGiven.WasAbleTo<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Given);

        Task ICanPerformWhen.AttemptsTo(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.When);

        Task<object> ICanPerformWhen.AttemptsTo(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.When);

        Task<T> ICanPerformWhen.AttemptsTo<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.When);

        Task ICanPerformThen.Should(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Then);

        Task<object> ICanPerformThen.Should(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Then);

        Task<T> ICanPerformThen.Should<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken, PerformancePhase.Then);
    }
}