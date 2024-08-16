# Makeup of a Screenplay

The diagram below shows the fundamental makeup of [a Screenplay Performance].

```mermaid
block-beta
columns 3
    Performance["A performance"]:3
    Actor("Actors") space
    block:Perf
        columns 3
        space
        Task["Tasks"]
        space:4
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
    Task -- "Compose" --> Action
    Task -- "Compose" --> Question
    style Perf fill:#E0E0F0,stroke:#C0C0E0
```

[a Screenplay Performance]: glossary/Performance.md

## Explanation

A performance involves one or more [Actors].
Each actor usually has at least one [Ability].
The actor performs [Performables], of which there are three fundamental types:

* [Tasks]
* [Actions]
* [Questions]

Actions and questions make direct use of the actor's abilities to perform their work.
Tasks, on the other hand, are compositions of any of actions, questions and/or other tasks.

[A full Screenplay] comprises of one or more performances.

[Actors]: glossary/Actor.md
[Ability]: glossary/Ability.md
[Performables]: glossary/Performable.md
[Tasks]: glossary/Task.md
[Actions]: glossary/Action.md
[Questions]: glossary/Question.md
[A full Screenplay]:glossary/Screenplay.md
