# Persona

A Persona is a class which serves as [a factory] for a specific **[Actor]**.
In Screenplay, it is strongly recommended to create and re-use well-known Actors across your **[Performances]**.
That is, if an actor with a specified [`Name`] has a certain set of **[Abilities]** in one Performance, then ideally all actors of the same name should have that same set of abilities in other performances.

This leads to the creation of well-known Actors which are well-understood by the team who are working with Screenplay.
Personas help facilitate that by providing a reusable location at which to set the actor's name and to assign & configure their abilities.

Personas in Screenplay are classes which implement the [`IPersona`] interface.

[a factory]: https://en.wikipedia.org/wiki/Factory_method_pattern
[Actor]: xref:CSF.Screenplay.Actor
[Performances]: xref:CSF.Screenplay.Performance
[`Name`]: xref:CSF.Screenplay.IHasName.Name
[Abilities]: Ability.md
[`IPersona`]: xref:CSF.Screenplay.IPersona
