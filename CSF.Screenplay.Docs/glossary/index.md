# Glossary

Following is a glossary of Screenplay terminology; each term is a link to its own documentation.
Many of these terms are implemented directly as .NET types in the Screenplay architecture.
Where applicable, the glossary item links directly to the relevant type within the [API documentation].

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
| [Feature]     | A logical group of related scenarios, this is a test class or test fixture in some testing frameworks          |
| [Integration] | A consumer of the Screenplay framework, such as a testing framework                           |

[API documentation]: xref:CSF.Screenplay
[Screenplay]: xref:CSF.Screenplay.Screenplay
[Performance]: xref:CSF.Screenplay.IPerformance
[Performable]: Performable.md
[Action]: Action.md
[Question]: Question.md
[Task]: Task.md
[Actor]: xref:CSF.Screenplay.Actor
[Ability]: Ability.md
[Persona]: Persona.md
[Cast]: xref:CSF.Screenplay.ICast
[Stage]: xref:CSF.Screenplay.IStage
[Spotlight]: Spotlight.md
[Report]: Report.md
[Scenario]: Scenario.md
[Feature]: Feature.md
[Integration]: Integration.md