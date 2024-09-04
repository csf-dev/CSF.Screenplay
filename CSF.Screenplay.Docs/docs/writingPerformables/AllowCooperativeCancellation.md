# Allow cooperative cancellation

All [Performables implement one of three interfaces] and each of these interfaces exposes a `PerformAsAsync` method which accepts a [`CancellationToken`] as a parameter.
This cancellation token is [to facilitate cooperative cancellation], so that a [Performance] may be cancelled/terminated gracefully.

* If an [Action] or [Question] makes use of an asynchronous method from an [Ability] then pass the cancellation token as a parameter to the Ability method that is used.
* Any time a [Performance] or [Task] executes a performable, pass the cancellation token down to the consumed performable as a parameter.
* Consider the strategies below for dealing with long-running synchronous methods, which do not natively participate in cooperative cancellation.

There is no need for every performable (particularly tasks) to execute [`ThrowIfCancellationRequested()`] as a matter of course.
Many performables complete their logic in microseconds or less, so excess cancellation-checking will bloat the logic for no perceivable gain.

So long as the cancellation token is passed from _'the top of the [Performance]'_ downwards, and any Actions or Questions which perform `async` or long-running logic are cancellation-enabled, cancellation should work in a timely fashion.

[Performables implement one of three interfaces]: ../../glossary/Performable.md#the-three-performable-interfaces-and-icanreport
[`CancellationToken`]: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken
[to facilitate cooperative cancellation]: https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads
[Performance]: xref:CSF.Screenplay.IPerformance
[Action]: ../../glossary/Action.md
[Question]: ../../glossary/Question.md
[Ability]: ../../glossary/Ability.md
[Task]: ../../glossary/Task.md
[`ThrowIfCancellationRequested()`]: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.throwifcancellationrequested

## Dealing with long-running synchronous methods

Sometimes an [Action] or [Question] will need to make use of functionality from an [Ability] which does not support cooperative cancellation.
If such functionality is long-running then this can interfere with cancellation.
It can lead to performances which take a long time to respond to cancellation of/when it is requested.

* Perhaps it's legacy code which predates the asynchronous programming model
* Perhaps it doesn't conform to best practice and doesn't accept a cancellation token parameter

_Here are two suggestions to deal with this._

### Throw if cancellation requested

Cancellation token objects have a method [`ThrowIfCancellationRequested()`] which will interrupt and throw an exception if cancellation has been requested.
You may use this method directly before executing a long-running synchronous (non-cancelable) Ability method.

This won't cancel the long-running method if cancellation is requested after it has started its work, but it will prevent it from being started if cancellation is already requested.

Use this technique if it's more important that the long-running method is not interrupted than it is to support timely cancellation.

### Use `Task.Wait` to make the long-running method interruptible

An alternative strategy for dealing with long-running synchronous methods is to wrap the method execution in a new `Task` and use [`Task.Wait()`] to interrupt the task if cancellation is requested.

Here's an example of that technique in action, applied to the `PerformAsAsync` method of a performable.

```csharp
public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken)
{
    Task.Run(() => LongRunningMethod()).Wait(cancellationToken);
    return default;
}
```

Use this technique if it's acceptable to send the long-running method into a separate thread and to 'give up waiting' for it to complete if cancellation has been requested.

This technique might provide better responsiveness to cancellation than the one above, albeit it is the most likely to produce unexpected impact on the behaviour of the performable.

Note that, as is standard for usage of `Task.Wait()`, cancelling the wait _does not abort the execution of the long-running method_. The thread which is executing that logic will usually continue and run to completion. Interrupting the wait just means that the main performance thread stops waiting for the other thread to complete.

[`Task.Wait()`]: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.wait#system-threading-tasks-task-wait(system-threading-cancellationtoken)
