---
uid: PerformableGlossaryItem
---

# Performable

A 'performable' is a verb in the language of Screenplay; performables are _things that **[Actors]** do_.
Performables are grouped into one of three types, although these are somewhat arbitrary names which do not have direct representation in the framework.

* **[Actions]** are the most granular of individual interactions with the application, composable to form higher-order interactions
* **[Questions]** are granular, like actions, but read the application's state to get information instead of making changes
* **[Tasks]** are higher-order interactions which are formed by composing actions, questions and/or other tasks

Actions & Questions are typically shipped by a framework; they are the fundamental building blocks from which developers create their own tasks.
There are typically a discrete number of actions & questions possible for a specified application of Screenplay.

Tasks, on the other hand, could be unlimited.
Developers using Screenplay should write as many as they need.
Tasks may represent any of the - possibly complex - interactions between the actor and the application, composing actions and/or questions and even other lower-level tasks.
Actions & Questions should interact directly with the Actor's **[Abilities]** to perform their logic; in most cases Tasks should not.

In Screenplay code, a **[Performance]** is a script of sorts, written from at least one performable, usually several.
All performables must implement one of the following three interfaces, but it's also _strongly recommended_ to implement [`ICanReport`] as well.
It's important to understand that whilst there are three interfaces shown here and three kinds of performable listed above, there is no direct correlation between them.

* [`IPerformable`]
* [`IPerformableWithResult`]
* [`IPerformableWithResult<TResult>`]

[Actors]: xref:CSF.Screenplay.Actor
[Actions]: Action.md
[Tasks]: Task.md
[Questions]: Question.md
[Abilities]: Ability.md
[Performance]: xref:CSF.Screenplay.IPerformance
[`ICanReport`]: xref:CSF.Screenplay.ICanReport
[`IPerformable`]: xref:CSF.Screenplay.IPerformable
[`IPerformableWithResult`]: xref:CSF.Screenplay.IPerformableWithResult
[`IPerformableWithResult<TResult>`]: xref:CSF.Screenplay.IPerformableWithResult`1