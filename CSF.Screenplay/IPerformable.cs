using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay
{
    /// <summary>
    /// An object which represents something that a performer (typically an actor) may perform.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Performable objects generally fall into one of three categories in Screenplay:
    /// </para>
    /// <list type="bullet">
    /// <item><description>An action, which is the most fine-grained type of performable, typically "doing something which alters the state of the application"</description></item>
    /// <item><description>A question, which is fine-grained like an action but instead reads state without changing it</description></item>
    /// <item><description>A task, which may be composed of actions, questions or even other tasks</description></item>
    /// </list>
    /// <para>
    /// Objects which implement only this interface are the simplest types of performables; they simply 'do something' and then finish.
    /// In the list given above these are typically actions.
    /// They do not return any form of result except completion.  If you wish to get a result from the performable then consider implementing
    /// an interface derived from this one, such as <see cref="IPerformableWithResult"/> or its strongly-typed counterpart
    /// <see cref="IPerformableWithResult{TResult}"/>.
    /// </para>
    /// <para>
    /// When implementing this interface, consider also implementing <see cref="ICanReport"/>.
    /// If a performable does not implement <see cref="ICanReport"/> then it will receive default text when the performance report is generated.
    /// Implementing <see cref="ICanReport"/> allows a performable to provide a customised human-readable report fragment.
    /// </para>
    /// </remarks>
    /// <seealso cref="IPerformableWithResult"/>
    /// <seealso cref="IPerformableWithResult{TResult}"/>
    public interface IPerformable
    {
        /// <summary>
        /// Performs the action(s) are represented by the current instance.
        /// </summary>
        /// <param name="actor">The actor that is performing.</param>
        /// <param name="cancellationToken">An optional cancellation token by which to abort the performance.</param>
        /// <returns>A task which completes when the performance represented by the current instance is complete.</returns>
        Task PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// An object which represents something that a performer (typically an actor) may perform and which returns a result when it completes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Performable objects generally fall into one of three categories in Screenplay:
    /// </para>
    /// <list type="bullet">
    /// <item><description>An action, which is the most fine-grained type of performable, typically "doing something which alters the state of the application"</description></item>
    /// <item><description>A question, which is fine-grained like an action but instead reads state without changing it</description></item>
    /// <item><description>A task, which may be composed of actions, questions or even other tasks</description></item>
    /// </list>
    /// <para>
    /// Objects which implement this interface are questions or tasks which are composed (at least in-part) from one or more questions.
    /// If you do not wish to get a result from the performable then implement only <see cref="IPerformable"/> instead.
    /// Alternatively, if you wish to return a strongly-typed result then instead consider implementing
    /// <see cref="IPerformableWithResult{TResult}"/>.
    /// </para>
    /// <para>
    /// Objects which implement this interface may be adapted to <see cref="IPerformable"/> via the
    /// <see cref="PerformableExtensions.ToPerformable(IPerformableWithResult)"/> extension method.
    /// </para>
    /// <para>
    /// When implementing this interface, consider also implementing <see cref="ICanReport"/>.
    /// If a performable does not implement <see cref="ICanReport"/> then it will receive default text when the performance report is generated.
    /// Implementing <see cref="ICanReport"/> allows a performable to provide a customised human-readable report fragment.
    /// </para>
    /// </remarks>
    /// <seealso cref="IPerformable"/>
    /// <seealso cref="IPerformableWithResult{TResult}"/>
    /// <seealso cref="PerformableExtensions.ToPerformable(IPerformableWithResult)"/>
    public interface IPerformableWithResult
    {
        /// <summary>
        /// Performs the action(s) are represented by the current instance and returns a value.
        /// </summary>
        /// <param name="actor">The actor that is performing.</param>
        /// <param name="cancellationToken">An optional cancellation token by which to abort the performance.</param>
        /// <returns>A task which exposes a 'result' value when the performance represented by the current instance is complete.</returns>
        Task<object> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default);
    }    

    /// <summary>
    /// An object which represents something that a performer (typically an actor) may perform and which returns a strongly-typed result when it completes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Performable objects generally fall into one of three categories in Screenplay:
    /// </para>
    /// <list type="bullet">
    /// <item><description>An action, which is the most fine-grained type of performable, typically "doing something which alters the state of the application"</description></item>
    /// <item><description>A question, which is fine-grained like an action but instead reads state without changing it</description></item>
    /// <item><description>A task, which may be composed of actions, questions or even other tasks</description></item>
    /// </list>
    /// <para>
    /// Objects which implement this interface are questions or tasks which are composed (at least in-part) from one or more questions.
    /// If you do not wish to get a result from the performable then implement only <see cref="IPerformable"/> instead.
    /// </para>
    /// <para>
    /// Objects which implement this interface may be adapted to <see cref="IPerformable"/> via the
    /// <see cref="PerformableExtensions.ToPerformable{TResult}(IPerformableWithResult{TResult})"/> extension method or to
    /// the non-generic <see cref="IPerformableWithResult"/> via
    /// <see cref="PerformableExtensions.ToNonGenericPerformableWithResult{TResult}(IPerformableWithResult{TResult})"/>.
    /// </para>
    /// <para>
    /// When implementing this interface, consider also implementing <see cref="ICanReport"/>.
    /// If a performable does not implement <see cref="ICanReport"/> then it will receive default text when the performance report is generated.
    /// Implementing <see cref="ICanReport"/> allows a performable to provide a customised human-readable report fragment.
    /// </para>
    /// </remarks>
    /// <seealso cref="IPerformable"/>
    /// <seealso cref="IPerformableWithResult"/>
    /// <seealso cref="PerformableExtensions.ToPerformable{TResult}(IPerformableWithResult{TResult})"/>
    /// <seealso cref="PerformableExtensions.ToNonGenericPerformableWithResult{TResult}(IPerformableWithResult{TResult})"/>
    public interface IPerformableWithResult<TResult>
    {
        /// <summary>
        /// Performs the action(s) are represented by the current instance and returns a strongly-typed value.
        /// </summary>
        /// <param name="actor">The actor that is performing.</param>
        /// <param name="cancellationToken">An optional cancellation token by which to abort the performance.</param>
        /// <returns>A task which exposes a strongly-typed 'result' value when the performance represented by the current instance is complete.</returns>
        Task<TResult> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default);
    }
}
