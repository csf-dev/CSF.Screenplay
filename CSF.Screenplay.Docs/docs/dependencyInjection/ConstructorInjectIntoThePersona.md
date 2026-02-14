# Injecting services into Personas

Types which derive from [`IPersona`] support constructor-injected dependencies.
Personas are typically used by either [the cast] or [the stage] to get an [Actor].
The technique in which they are used means that they are resolved, along with their constructor-injected dependencies, from DI.

Use constructor-injected dependencies in persona classes to provide access to the APIs required to resolve [Abilities] that the actor is to be granted.

[`IPersona`]: xref:CSF.Screenplay.IPersona
[the cast]: xref:CSF.Screenplay.ICast
[the stage]: xref:CSF.Screenplay.IStage
[Actor]: xref:CSF.Screenplay.Actor
[Abilities]: ../../glossary/Ability.md