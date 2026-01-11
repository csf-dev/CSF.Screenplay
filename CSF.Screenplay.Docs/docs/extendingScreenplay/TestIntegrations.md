# Writing new Test framework integrations

A way in which Screenplay is available for extension is the integration of Screenplay with other Test frameworks.
Screenplay currently ships with integrations for [NUnit] and [SpecFlow] but developers are free to integrate it into other frameworks if they wish.
Developers who are interested in this may use the source code to these two integrations as inspiration.

[NUnit]: https://nunit.org
[SpecFlow]: https://specflow.org

## Requirements

To integrate with a test framework, the minimum requirements are as follows.
Requirements which **must** be fulfilled are crucial to the operation of Screenplay; the integration will malfunction if they are not satisfied.
Requirements which **should** be fulfilled are not crucial, but are strongly recommended for a good developer experience when using the integration.
Terminologies differ between testing frameworks; the word [Scenario] is used to refer to the individual tests that the testing framework executes.

* The test framework **must** create an instance of [`Screenplay`] before it runs any [Scenarios]
    * If the test framework uses dependency injection then it is advantageous to integrate Screenplay with that, via [`AddScreenplay`]
    * If not then consider the [`Create`] factory method
    * Consider permitting extensibility here, using the [`IGetsScreenplay`] interface
* Before any [Scenarios] run, the framework **must** execute [`BeginScreenplay()`] from the Screenplay instance
* After all [Scenarios] have completed it **must** execute [`CompleteScreenplay()`] from the Screenplay instance
* Each [Scenario] in the test framework **must** have its own [Performance], within its own a DI lifetime scope
    * Consider using the [`CreateScopedPerformance`] method to achieve this
* The [Performance] associated with each [Scenario] **should** have its [`NamingHierarchy`] set according to the name of the Scenario
    * Typically this is done via parameter when creating the Performance
    * It may alternatively be updated after creation
    * Test frameworks have different conventions, so the precise semantics of this name is up to the framework itself
    * The purpose of setting this is to clearly match the Performance to the Scenario to which it relates, when reading reports; if missing then the information will not be present in the report
* Before the logic of each [Scenario] starts, the [`BeginPerformance()`] method **must** be executed from the corresponding [Performance]
* After the logic of each [Scenario] ends, the [`FinishPerformance(bool?)`] method **must** be executed from the corresponding [Performance]
* After the logic of each [Scenario] ends, the dependency injection scope associated with the [Performance] **should** be disposed
    * Failure to do this could lead to memory leaks or unnecesarily high resource usage whilst the [Screenplay] is in-progress
* The test framework **must** provide access to at least the [`ICast`] and [`IStage`], resolved from the [Scenario]'s dependency injection scope, to the [Scenario] logic
    * The manner of doing this depends entirely on the test framework
    * By way of example, in NUnit this is performed by providing the values of parameters to the test method, in SpecFlow this is performed by resolving step bindng classes from that same DI scope, allowing constructor injection

[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[Scenarios]: ../../glossary/Scenario.md
[`AddScreenplay`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions.AddScreenplay(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{CSF.Screenplay.ScreenplayOptions})
[`Create`]: xref:CSF.Screenplay.Screenplay.Create(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{CSF.Screenplay.ScreenplayOptions})
[`IGetsScreenplay`]: xref:CSF.Screenplay.IGetsScreenplay
[`BeginScreenplay()`]: xref:CSF.Screenplay.Screenplay.BeginScreenplay
[`CompleteScreenplay()`]: xref:CSF.Screenplay.Screenplay.CompleteScreenplay
[Scenario]: ../../glossary/Scenario.md
[Performance]: xref:CSF.Screenplay.IPerformance
[`CreateScopedPerformance`]: xref:CSF.Screenplay.ScreenplayExtensions.CreateScopedPerformance(CSF.Screenplay.Screenplay,System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Guid)
[`NamingHierarchy`]: xref:CSF.Screenplay.IPerformance.NamingHierarchy
[`BeginPerformance()`]: xref:CSF.Screenplay.Performances.IBeginsAndEndsPerformance.BeginPerformance
[`FinishPerformance(bool?)`]: xref:CSF.Screenplay.Performances.IBeginsAndEndsPerformance.FinishPerformance(System.Nullable{System.Boolean})
[Screenplay]: xref:CSF.Screenplay.Screenplay
[`ICast`]: xref:CSF.Screenplay.ICast
[`IStage`]: xref:CSF.Screenplay.IStage
