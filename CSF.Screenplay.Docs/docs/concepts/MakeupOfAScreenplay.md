# Makeup of a Screenplay

_A Screenplay_ conceptually refers to a complete execution screenplay-based logic, written using this framework.
As .NET code, [`Screenplay`] is a class which coordinates the scope & execution of that logic.

A Screenplay is comprised at least one [Performance], typically lots of them.
A Performance is made from [Actors] performing [Performables], of which there are [Tasks], [Actions] and [Questions].
The logic of Actions and/or Questions almost always requires the use of an [Ability].
The diagram below shows the architecture of how these concepts relate to one another.

```mermaid
block-beta
    columns 3
    Screenplay:3
    Performance["Performance<br>(each Screenplay may contain many)"]:3
    Actor("Actors") space
    block:Perf
        columns 3
        space
        Task["Tasks"]
        space
        Action["Actions"]
        space
        Question["Questions"]
    end
    space:3
    Ability["Abilities"]
    Actor -- "Perform" --> Perf
    Ability -- "Has" --> Actor
    Action -- "Use" --> Ability
    Question -- "Use" --> Ability
    style Perf fill:#E0E0F0,stroke:#C0C0E0
```

[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[Performance]: xref:CSF.Screenplay.IPerformance
[Actors]: xref:CSF.Screenplay.Actor
[Ability]: ../../glossary/Ability.md
[Performables]: ../../glossary/Performable.md
[Tasks]: ../../glossary/Task.md
[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
