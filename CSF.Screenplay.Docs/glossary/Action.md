# Action

An Action is a kind of **[Performable]** in which an **[Actor]** does something or interacts with the application in such as way as to change its state.
Specifically, an action should be the smallest, most granular change or interaction possible; something which cannot reasonably be split into constituent parts.

In an application of Screenplay which controls a web browser, an action might be a single mouse click, or entering some specified text into an input field.
To create higher-level interactions, use **[Tasks]** to compose actions.
Out of the kinds of performable, actions are the smallest building blocks available.

Actions don't have a direct representation in Screenplay code because they are really just an arbitrary category of performable.
Actions _most commonly_ implement the [`IPerformable`] interface though, as they usually do not return any results.

Generally, the logic of actions interacts directly with the actor's **[Abilities]** in order to provide the functionality required to perform the action.

[Performable]: Performable.md
[Actor]: xref:CSF.Screenplay.Actor
[Tasks]: Task.md
[`IPerformable`]: xref:CSF.Screenplay.IPerformable
[Abilities]: Ability.md