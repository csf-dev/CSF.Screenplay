# Writing Performables

Screenplay and add-on libraries will ship with pre-created [Actions] and [Questions], two of the three types of [Performable].
Developers making use of Screenplay _might not need to write new Actions or Questions_, because they may use and compose the existing ones.

On the other hand, it is very likely that developers will need to write [Tasks], which are the kind of Performable which composes Actions, Questions and/or other Tasks.

[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
[Performable]: ../../glossary/Performable.md
[Tasks]: ../../glossary/Task.md

## A sample Task

Here is an annotated example of a [Task] which makes use of a fictitious Action and a fictitious Question.

TODO: Write this docco

[Task]: ../../glossary/Task.md

## Guidelines for writing performables

The following list shows some guidelines for writing new performables.
These apply equally across ask if the three types of performables, even though developers are mainly expected to be writing tasks.

* [Implement precisely one performable interface]
* [Implement `ICanReport`]
* [Parameterize low-level performables]
* [Performables should be stateful but immutable]
* [Allow cooperative cancellation]
* [Write a builder]
* [Do not rely on a DI framework]
* [Aim for Pure Functional tasks]
* [Do not interact with Abilities from Tasks]

[Implement precisely one performable interface]: ImplementOnePerformableInterface.md
[Implement `ICanReport`]: ImplementICanReport.md
[Parameterize low-level performables]: ParameterizeLowLevelPerformables.md
[Performables should be stateful but immutable]: StatefulButImmutable.md
[Allow cooperative cancellation]: AllowCooperativeCancellation.md
[Write a builder]: WriteABuilder.md
[Do not rely on a DI framework]: DoNotUseDiFrameworks.md
[Aim for Pure Functional tasks]: PureFunctionalTasks.md
[Do not interact with Abilities from Tasks]: TasksDoNotUseAbilities.md