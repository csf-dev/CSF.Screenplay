# Stage

The Stage is an optional but recommended component of Screenplay.
Its purpose is to enable developers to write **[Performances]** which use the passive voice and pronouns instead of needing to keep repeating an **[Actor's]** name.

Consider a performance like the following, described in [Gherkin BDD syntax].

```txt
Given Jack can wash dishes
  And Jack has filled a basin with hot water
 When Jack washes a dinner plate
 Then Jack should have one clean dinner plate
```

This is perfectly functional but each **[Performable]** has been qualified with the name "Jack", to indicate the Actor.
This could be more readable but also more reusable from a code perspective if we had a concept of _a currently-active actor_.
This is precisely what the stage provides.

In code, the stage is the [`IStage`] interface, which may be dependency-injected into your **[Scenario]** logic.

[Performances]: Performance.md
[Actor's]: Actor.md
[Gherkin BDD syntax]: https://cucumber.io/docs/gherkin/
[Performable]: Performable.md
[`IStage`]: xref:CSF.Screenplay.IStage
[Scenario]: Scenario.md

## Spotlight

When the stage is used to get an actor, that actor is placed 'in the **Spotlight**'. That same actor remains in the spotlight until a different actor is placed there.
A stage may be used as any time to get the actor who is currently in the spotlight.
Thus, it is possible to infer an actor in Performance logic without needing their name.
