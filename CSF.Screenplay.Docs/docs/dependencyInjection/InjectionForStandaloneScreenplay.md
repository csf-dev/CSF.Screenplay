# Dependency injection for standalone Screenplay

If you are [using Screenplay standalone] then the [`Screenplay.ExecuteAsPerformanceAsync`] permits resolution of dependencies via its parameter.
That parameter is a `Func<IServiceProvider,CancellationToken,Task<bool?>>`.
The service provider may be used to resolve dependency services for the performance's logic.

Developers are urged to consider encapsulating their performance logic in implementations of [`IHostsPerformance`].
Through an overload (extension method) named [`ExecuteAsPerformanceAsync<T>`], developers may specify the concrete implementation of that interface.
This extension method will resolve that implementation type along with any of its constructor-injected dependencies.
This avoids the service locator anti-pattern and provides a convenient pattern by which to write performance logic.

Use services resolved from the service provider, or injected into your [`IHostsPerformance`] implementation, to get access to [commonly-used Screenplay services] and anything else required at the root level of your performance logic.

[using Screenplay standalone]: ../gettingStarted/nonTesting/index.md
[`Screenplay.ExecuteAsPerformanceAsync`]: xref:CSF.Screenplay.Screenplay.ExecuteAsPerformanceAsync(System.Func{System.IServiceProvider,System.Threading.CancellationToken,System.Threading.Tasks.Task{System.Nullable{System.Boolean}}},System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)
[`IHostsPerformance`]: xref:CSF.Screenplay.IHostsPerformance
[`ExecuteAsPerformanceAsync<T>`]: xref:CSF.Screenplay.ScreenplayExtensions.ExecuteAsPerformanceAsync``1(CSF.Screenplay.Screenplay,System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)
