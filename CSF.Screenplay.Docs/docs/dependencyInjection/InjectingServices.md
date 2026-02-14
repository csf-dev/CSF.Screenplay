---
uid: InjectingServicesArticle
---
# Injecting services

The table below summarises how to get services, from various contexts. In the first column are contexts/situations in which a developer could require services from DI. The second column indicates and links to the appropriate technique for that scenario.

All but one of these techniques (the last) provides full access to [the services which are added to the container].

| Context                                              | Technique                                                |
| ---------------------------------------------------- | -------------------------------------------------------- |
| NUnit style test method                              | [Parameter inject into the test] method                  |
| Reqnroll style binding class                         | [Constructor inject into the binding] class              |
| Standalone Screenplay: [`ExecuteAsPerformanceAsync`] | [Create a performance host, or use the service provider] |
| Persona class                                        | [Constructor inject into the Persona] class              |
| Performable class, such as a Task                    | [Use the actor's abilities]                              |

[the services which are added to the container]: InjectableServices.md
[Parameter inject into the test]: ParameterInjectionForTests.md
[Constructor inject into the binding]: ConstructorInjectionForBindings.md
[Create a performance host, or use the service provider]: InjectionForStandaloneScreenplay.md
[Constructor inject into the Persona]: ConstructorInjectIntoThePersona.md
[`ExecuteAsPerformanceAsync`]: xref:CSF.Screenplay.Screenplay.ExecuteAsPerformanceAsync(System.Func{System.IServiceProvider,System.Threading.CancellationToken,System.Threading.Tasks.Task{System.Nullable{System.Boolean}}},System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)
[Use the actor's abilities]: Performables.md
