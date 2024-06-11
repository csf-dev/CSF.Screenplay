# Ability

An ability is an arbitrary object which may be granted to **[Actors]** in order to allow them to interact with the application as part of a **[Performance]**.
Unlike many other Screenplay object-types, there is no particular interface which Abilities should implement, as their functionality, capabilities and very nature are specific to them alone.
That said - it is recommended for Abilities to implement [`ICanReport`] if possible, as this allows for the production of a more pleasant human-readable report.

**[Actions]** and/or **[Questions]** may interact directly with an actor's abilities in order to perform their functionality.
**[Tasks]**, on the other hand, should not.

[Actors]: Actor.md
[Performance]: Performance.md
[`ICanReport`]: xref:CSF.Screenplay.ICanReport
[Actions]: Action.md
[Questions]: Question.md
[Tasks]: Task.md
