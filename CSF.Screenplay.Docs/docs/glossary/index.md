# Glossary

Following is a glossary of Screenplay terminology; each term is a link to its own documentation.
Where applicable, the glossary definition documentation links to relevant types & members within the [API documentation].

| Term          | Summary                                                                                       |
| ----          | -------                                                                                       |
| [Screenplay]  | A complete execution of the Screenplay software                                               |
| [Performance] | A single end-to-end script of performables                                                    |
| [Performable] | A Screenplay verb; something that an actor can do                                             |
| [Action]      | A kind of peformable; the lowest-level interaction that changes the state of the application  |
| [Question]    | A kind of peformable; the lowest-level interrogation that reads application state             |
| [Task]        | A composition of actions, questions and/or other tasks to create higher-level performables    |
| [Actor]       | Typically a human user of the application, directs the use of performables                    |
| [Ability]     | Something that an actor is able to do or has; provides the dependencies for actions/questions |
| [Persona]     | A factory or template for consistently creating reusable, well-known actors                   |
| [Cast]        | A factory & registry for actors which facilitates managing multiple actors in a performance   |
| [Stage]       | Provides situational context; a concept of 'the currently active actor'                       |
| [Spotlight]   | The currently active actor, facilitated by the stage                                          |
| [Report]      | An output which details every performance/scenario and the outcomes of theie performables     |
| [Scenario]    | Typically similar to a performance, this is a single test within a testing framework          |
| [Integration] | A consumer of the Screenplay framework, such as a testing framework                           |

[API documentation]: ../../api/index.md
[Screenplay]: Screenplay.md
[Performance]: Performance.md
[Performable]: Performable.md
[Action]: Action.md
[Question]: Question.md
[Task]: Task.md
[Actor]: Actor.md
[Ability]: Ability.md
[Persona]: Persona.md
[Cast]: Cast.md
[Stage]: Stage.md
[Spotlight]: Stage.md#spotlight
[Report]: Report.md
[Scenario]: Scenario.md
[Integration]: Integration.md
