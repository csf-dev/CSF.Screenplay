# Screenplay's capabilities

<ul class="capabilities" id="capabilitiesList">
<li class="intuitive">

[Intuitive, human-readable design](#screenplay-follows-an-intuitive-design)

</li>
<li class="reuse">

[Promotes maximum code reuse](#screenplay-promotes-code-reuse)

</li>
<li class="reporting">

[Produces human-readable reports](#screenplay-produces-reports)

</li>
<li class="testing">

[Integration with testing frameworks](#screenplay-integrates-with-testing-frameworks)

</li>
<li class="extensible">

[Extensibility via plug-in extensions](#screenplay-is-extensible)

</li>
<li class="not-just-testing">

[Not just for automated testing](#screenplay-isnt-limited-to-software-testing)

</li>
</ul>

## Screenplay follows an intuitive design

The logic of a Screenplay [Performance] or [Task] is obvious, to both programmers and non-programmers alike.
Through use of static builders, Screenplay logic forms [a fluent interface] which is instantly readable.
For example, this is a line of Screenplay-based code:

```csharp
await Given(webster).WasAbleTo(AddAProductToTheirCartNamed("Blue widget"));
```

[Performance]: xref:CSF.Screenplay.IPerformance
[Task]: ../../glossary/Task.md
[a fluent interface]: https://en.wikipedia.org/wiki/Fluent_interface

## Screenplay promotes code reuse

Screenplay ruthlessly follows [SOLID design principles], particularly SRP and OCP.
[The architecture of Performables] permits unlimited levels of composition via [Tasks].

Developers are also encouraged to use [Personas] to configure and manage the [Actors] which participate in [a Screenplay].

[SOLID design principles]: https://en.wikipedia.org/wiki/SOLID
[The architecture of Performables]: ../../glossary/Performable.md
[Tasks]: ../../glossary/Task.md
[Personas]: ../../glossary/Persona.md
[Actors]: xref:CSF.Screenplay.Actor
[a Screenplay]: xref:CSF.Screenplay.Screenplay

## Screenplay produces reports

Whenever [a Screenplay] executes, a record of what happened is saved in JSON format.
This JSON file is computer-readable, but [it may be converted to a human-readable report using the included utility].

[it may be converted to a human-readable report using the included utility]: ../GettingReports.md

## Screenplay integrates with Testing frameworks

If you would like to use Screenplay as a test-authoring framework then you are in luck.
Screenplay has [integrations with testing frameworks], out of the box:

* [NUnit 3](https://www.nuget.org/packages/CSF.Screenplay.NUnit)
* [Reqnroll](https://www.nuget.org/packages/CSF.Screenplay.Reqnroll) _and [the now-retired SpecFlow](https://www.nuget.org/packages/CSF.Screenplay.SpecFlow)_

The community is encouraged to create and contribute additional integrations if they wish.
The source code of the existing integrations, and the documentation website for your chosen test framework, are good places to start for writing a new integration.

[integrations with testing frameworks]: ../../glossary/Integration.md

## Screenplay is extensible

Screenplay has two primary extension points, test-framework integrations (above) and [Extensions].
Extensions provide Screenplay with the capabilities to interact-with and automate additional technologies.
Some extensions are provided already:

* [Selenium WebDriver](https://www.nuget.org/packages/CSF.Screenplay.Selenium)
* [Web APIs](https://www.nuget.org/packages/CSF.Screenplay.WebApis)

The community is encouraged to create and contribute additional extensions if they wish.
Writing an extension is a matter of writing [Ability], [Action] and [Question] classes, along with any appropriate [Builders].
The source code for the existing Extensions are a good place to start in learning how to create a new one.

[Extensions]: ../../glossary/Extension.md
[Ability]: ../../glossary/Ability.md
[Action]: ../../glossary/Action.md
[Question]: ../../glossary/Question.md
[Builders]: ../builderPattern/WritingBuilders.md

## Screenplay isn't limited to software testing

Whilst Screenplay is a popular pattern for automated testing, particularly web-browser-based testing, it doesn't have to be limited to that use-case.
Screenplay may be invoked as a standalone process-automation tool.
See the documentation of the [`ExecuteAsPerformanceAsync<T>`] method for information and examples for the recommended way to use Screenplay outside of a testing framework.

[`ExecuteAsPerformanceAsync<T>`]: xref:CSF.Screenplay.ScreenplayExtensions.ExecuteAsPerformanceAsync``1(CSF.Screenplay.Screenplay,System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)
