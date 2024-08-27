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
    /// If a performable does not implement <see cref="ICanReport"/> then it will receive default text when the
    /// <see cref="Performance"/> report is generated.
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
        /// <param name="cancellationToken">An optional cancellation token by which to abort the performable.</param>
        /// <returns>A task which completes when the performable represented by the current instance is complete.</returns>
        ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default);
    }
}
