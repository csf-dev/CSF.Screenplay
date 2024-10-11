# Abilities, Actions & Questions

Screenplay ships [with a small number of Abilities and performables] but it is designed to be extended with new ones.
New [Abilities], [Actions] and [Questions] extend [Screenplay] by allowing [Actors] to interact with new APIs, services and libraries.

Broadly-speaking to extend Screenplay in this way you must: 

* Write one or more new ability types which provide access to the API of the service or library with which you'd like to interact
* Write one or more Action and/or Question [Performables] which make use of that ability

[with a small number of Abilities and performables]: ../performables/index.md
[Screenplay]: xref:CSF.Screenplay.Screenplay

## Writing abilities

Recall that [Abilities] represent capabilities & dependencies granted to or associated with [Actors].
It is normal for developers to want to write new Ability classes in order to provide capabilities/dependencies which are not yet catered-for. 

Ability classes do not _need to derive_ from any particular base type, although it is strongly recommended that they implement [`ICanReport`].
Ability classes [may constructor-inject dependencies] and should declare whatever API is appropriate.

[Abilities]: ../../glossary/Ability.md
[Actors]: xref:CSF.Screenplay.Actor
[`ICanReport`]: xref:CSF.Screenplay.ICanReport
[may constructor-inject dependencies]: ../dependencyInjection/index.md

## Writing Actions and/or Questions

Hand-in-hand with writing new ability classes, comes writing new [Action] and/or [Question] performables.
These classes must derive from [an appropriate Performable interface] and _should also implement [`ICanReport`]_.
These actions/questions should interact with the ability and make use of its functionality or return a value accordingly.

Recall that actions & questions should be _as granular as reasonably possible_, accepting whatever parameters are appropriate for the usage of the ability.
Continue to follow [best practice for performables], though.

[Action]: ../../glossary/Action.md
[Question]: ../../glossary/Question.md
[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
[Performables]: ../../glossary/Performable.md
[an appropriate Performable interface]: ../../glossary/Performable.md
[best practice for performables]: ../writingPerformables/index.md