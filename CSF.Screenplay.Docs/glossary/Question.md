# Question

A Question is a kind of **[Performable]** in which an **[Actor]** gets or reads some information from the application, ideally in such a way that does not change the application's state.
Similar to **[Actions]**, questions should be as small in their scope as possible, to make them as reusable and composable as possible.
In code, questions get a value and return it to the consuming logic, so they will always implement one of [`IPerformableWithResult<TResult>`] or its non-generic counterpart [`IPerformableWithResult`].

In an application of Screenplay which controls a web browser, a questions might represent reading the text from a single HTML element, or reading the enabled/disabled state of a button.
To create higher-level questions with broader scope, compose them with **[Tasks]**.

Generally, the logic of questions interacts directly with the actor's **[Abilities]** in order to provide the functionality required to get the requested information.

[Performable]: Performable.md
[Actor]: xref:CSF.Screenplay.Actor
[Actions]: Action.md
[`IPerformableWithResult`]: xref:CSF.Screenplay.IPerformableWithResult
[`IPerformableWithResult<TResult>`]: xref:CSF.Screenplay.IPerformableWithResult`1
[Tasks]: Task.md
[Abilities]: Ability.md
