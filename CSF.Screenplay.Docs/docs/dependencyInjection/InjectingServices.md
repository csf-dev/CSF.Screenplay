---
uid: InjectingServicesArticle
---

# Injecting services

There are a few techniques in which injecting dependencies is relevant. These are summarised below.
All but one of these techniques provide access to [the services which are added to the container].

[the services which are added to the container]: InjectableServices.md

## Into test logic

When using automated test logic based upon Screenplay, the use of dependency injection typically takes one of two forms.
Which of these depends upon the nature and paradigm of the test framework.

For frameworks which are based on **test methods** such as [NUnit], services are typically injected via _method parameter injection_ into the test methods.
[If Screenplay were to be extended] to work with frameworks such as xUnit or MSTest then this is likely to be the technique used.

For frameworks which are based on **binding classes** such as [SpecFlow], services are constructor-injected into binding classes.

Use dependencies injected in this way to get access to [commonly-used Screenplay services] and anything else required at the root level of your test logic.

[NUnit]: https://nunit.org/
[If Screenplay were to be extended]: ../extendingScreenplay/TestIntegrations.md
[SpecFlow]: https://specflow.org/
[commonly-used Screenplay services]: InjectableServices.md

## Into standalone performance logic

If you are [using Screenplay standalone] then the [`Screenplay.ExecuteAsPerformanceAsync`] permits resolution of dependencies via its parameter.
That parameter is a `Func<IServiceProvider,CancellationToken,Task<bool?>>`.
The service provider may be used to resolve dependency services for the performance's logic.

Developers are urged to consider encapsulating their performance logic in implementations of [`IHostsPerformance`].
Through an overload (extension method) named [`ExecuteAsPerformanceAsync<T>`], developers may specify the concrete implementation of that interface.
This extension method will resolve that implementation type along with any of its constructor-injected dependencies.
This avoids the service locator anti-pattern and provides a convenient pattern by which to write performance logic.

Use services resolved from the service provider, or injected into your [`IHostsPerformance`] implementation, to get access to [commonly-used Screenplay services] and anything else required at the root level of your performance logic.

[using Screenplay standalone]: ../StandaloneScreenplay.md
[`Screenplay.ExecuteAsPerformanceAsync`]: xref:CSF.Screenplay.Screenplay.ExecuteAsPerformanceAsync(System.Func{System.IServiceProvider,System.Threading.CancellationToken,System.Threading.Tasks.Task{System.Nullable{System.Boolean}}},System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)
[`IHostsPerformance`]: xref:CSF.Screenplay.IHostsPerformance
[`ExecuteAsPerformanceAsync<T>`]: xref:CSF.Screenplay.ScreenplayExtensions.ExecuteAsPerformanceAsync``1(CSF.Screenplay.Screenplay,System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)

## Into personas

Types which derive from [`IPersona`] support constructor-injected dependencies.
Personas are typically used by either [the cast] or [the stage] to get an [Actor].
The technique in which they are used means that they are resolved, along with their constructor-injected dependencies, from DI.

Use constructor-injected dependencies in persona classes to provide access to the APIs required to resolve [Abilities] that the actor is to be granted.

[`IPersona`]: xref:CSF.Screenplay.IPersona
[the cast]: xref:CSF.Screenplay.ICast
[the stage]: xref:CSF.Screenplay.IStage
[Actor]: xref:CSF.Screenplay.Actor
[Abilities]: ../../glossary/Ability.md

## Into performables

Crucially, [Performable] types _do not participate in dependency injection from the container_.
Performables are [created using the builder pattern] and do not hold references to any dependency services.
Instead, the state of the performable - set either via constructor parameters or a public settable property - represents the _'parameters'_ for that performable.

For example, a [Task] which adds an item to the user's shopping basket might have a constructor parameter which is the unique ID of the product to be added to the basket.

Performables, specifcally [Actions] and [Questions], should access their dependencies via the Actor's **[Abilities]**.
In this sense, Abilities are a form of dependency injection or service locator (for a specific use case).
Whilst service locators are usually considered an anti-pattern, the benefits of this pattern outweigh the disadvantages.
That is - being able to create performables from builders which includes all of their 'parameters' in their state, thus executable from one of three interface methods.
By ensuring performables are unencumbered by their dependencies, they become composable in almost any configuration that you can imagine.

Importantly, _[Tasks] should never make use of [Abilities]_.
Only Actions and Questions should use Abilities.

[Performable]: ../../glossary/Performable.md
[created using the builder pattern]: ../builderPattern/index.md
[Task]: ../../glossary/Task.md
[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
[Tasks]: ../../glossary/Task.md
