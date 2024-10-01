# Advice for writing tests with Screenplay

Whichever testing integration you choose to use, there are some general pieces of advice and best practice which apply. 

## Use high-level tasks

Avoid the use of too many tasks/performables in each test.
Too many [performables] in a single test makes it hard to comprehend at-a-glance and forces the reader to commit too much detail to memory.
Rather than using many performables at the top-level of your tests, create [high-level tasks] which compose and simplify some of that detail into a single logical step.

When using a method-driven testing framework, such as NUnit, five performables in a single test method is a reasonable number.
More than approximately ten performables is too many.

When using a binding-driven testing framework like SpecFlow, each binding should ideally correspond to at-most one performable.

[performables]: ../glossary/Performable.md
[high-level tasks]: ../glossary/Task.md

## Use the Performance Starter

Your top-level test logic should consume its [performables] via the class [`PerformanceStarter`].
This provides for a clear given/when/then appearance to your top-level test logic.

[`PerformanceStarter`]: xref:CSF.Screenplay.PerformanceStarter

## Consider a fluent-style assertions library

Depending upon the testing framework you have chosen, you may or may not have access to fluent-style assertions functionality.
NUnit, for example, provides assertions based upon [the constraint model], which is an extensible fluent-style syntax.

If your chosen testing framework does not provide easy-to-read assertions, consider an external assertion library.
Assertion libraries to consider include [Shouldly] and [Fluent Assertions].

[the constraint model]: https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/constraint.html
[Shouldly]: https://github.com/shouldly/shouldly
[Fluent Assertions]: https://fluentassertions.com/

## Do not include assertions in performables

It might be tempting to include assertion syntax within [performables] such as [tasks].
This is not recommended. 
Where assertions appear within performables:

* They reduce the reusability of the performables
* They create a dependency between your performables and your testing framework/assertion library
* They can make test logic harder to read and understand

It is recommended to keep your performables/tasks free from assertions. 
In your test code, use [questions] or question-like tasks to get values/data from the system under test. 
Write your assertions in your main test logic, asserting that the values retrieved are as-expected.

[tasks]: ../glossary/Task.md
[questions]: ../glossary/Question.md