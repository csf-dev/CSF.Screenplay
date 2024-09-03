# Allow cooperative cancellation

All [Performables implement one of three interfaces] and each of these interfaces exposes a `PerformAsAsync` method which accepts a [`CancellationToken`] as a parameter.
This cancellation token is [to facilitate cooperative cancellation], so that a [Performance] may be cancelled/terminated gracefully.

* If an [Action] or [Question] makes use of an asynchronous method from an [Ability] then pass the cancellation token as a parameter to the Ability method that is used.
* Any time a [Task] executes any other performable, pass the cancellation token down to the consumed performable as a parameter.
* If an Action or Question uses a long-running synchronous method from an Ability then consider using [`ThrowIfCancellationRequested()`] immediately before executing the long-running method.

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
