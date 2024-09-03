---
uid: PerformableGlossaryItem
---

# Performable

A 'performable' is a verb in the language of Screenplay; performables are _things that **[Actors]** do_.
Performables are logically grouped into one of the following three kinds.
_Do not be mistaken in believing_ that these three kinds of performable correspond to the three interfaces which are listed later on this page.
This is entirely coincidental and _there is no direct equivalence_.

* **[Actions]** are the most granular of individual interactions with the application, composable to form higher-order interactions
* **[Questions]** are granular, like actions, but read the application's state to get information instead of making changes
* **[Tasks]** are higher-order interactions which are formed by composing actions, questions and/or other tasks

[Actors]: xref:CSF.Screenplay.Actor
[Actions]: Action.md
[Tasks]: Task.md
[Questions]: Question.md

## Developers using Screenplay will typically be writing Tasks

Actions & Questions are the smallest/low-level building blocks of a Screenplay [Performance].
They are parameterized and should make as few assumptions as possible.
As such, they should be **highly reusable**.
Actions & Questions _rarely need to be written_ by developers who are using Screenplay.
That is because most likely, the Action, Question and corresponding **[Ability]** classes will have been provided in a library, such as a NuGet package.

On the other hand, **Tasks** are higher-level performables which may compose Actions, Questions and/or other lower-level Tasks.
Developers using Screenplay should write as many Tasks as they need.
Tasks may represent any of the - possibly complex - interactions performed by the Actor.
Actions & Questions should interact directly with the Actor's **[Abilities]** to perform their logic; in most cases Tasks should not directly use any Abilities.

When writing your own performables, consider [these best practices] for the best results.

[Performance]: xref:CSF.Screenplay.IPerformance
[Ability]: Ability.md
[Abilities]: Ability.md
[these best practices]: ../docs/writingPerformables/index.md

## The three performable interfaces, and `ICanReport`

In Screenplay code, a **[Performance]** is a script of sorts, written from at least one performable, usually several.
All performables must implement one of the following three interfaces, but it's also _strongly recommended_ to implement [`ICanReport`] as well.
As noted above, _it is coincidence alone_ that there are three kinds of performable and three interfaces for performables.
There is no direct correlation between these interfaces and the kinds of performable.

* [`IPerformable`]
* [`IPerformableWithResult`]
* [`IPerformableWithResult<TResult>`]

[`ICanReport`]: xref:CSF.Screenplay.ICanReport
[`IPerformable`]: xref:CSF.Screenplay.IPerformable
[`IPerformableWithResult`]: xref:CSF.Screenplay.IPerformableWithResult
[`IPerformableWithResult<TResult>`]: xref:CSF.Screenplay.IPerformableWithResult`1
