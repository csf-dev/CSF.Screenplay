# Glossary

This page offers a glossary of Screenplay terminology.
Each term is a link to its own documentation.

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
| [Report]      | An output which details every performance/scenario and the outcomes of theie performables     |
| [Scenario]    | Typically similar to a performance, this is a single test within a testing framework          |
| [Integration] | A consumer of the Screenplay framework, such as a testing framework                           |

## Screenplay

A Screenplay (used as a noun) refers to a complete execution of the Screenplay software.
A Screenplay is composed of at least one **[Performance]**, and typically a Screenplay contains many performances. 

In a testing framework, a Screenplay corresponds to a complete test run.

[Performance]: Glossary.md#Performance

## Performance

A Performance is a start-to-finish script of **[Performables]** which describes a journey to an intended outcome.
A **[Screenplay]** typically contains many performances.

In a testing framework a Performance corresponds to a single test.
This might alternatively be called a "scenario", "test case" or "theory".

Performances have a direct representation in the Screenplay library: the [`Performance`] class.

[Performables]: Glossary.md#Performable
[Screenplay]: Glossary.md#Screenplay
[`Performance`]: xref:TODO

## Performable

A 'performable' is a verb in the language of Screenplay; performables are _things that **[Actors]** do_.
Performables are grouped into one of three types, although these are somewhat arbitrary names which do not have direct representation in the framework.

* **[Actions]** are the most granular of individual interactions with the application, composable to form higher-order interactions
* **[Tasks]** are those higher-order interactions which are formed by composing actions, other tasks and possibly ...
* **[Questions]**, which are ways in which the actor may read the application's state to get information

Actions & Questions are typically shipped by a framework; there are a discrete number of them for a specified application of Screenplay because they are so granular.
Tasks, on the other hand, could be limitless because they represent any of the - possibly complex - interactions between the actor and the application, composing actions and/or questions and even other lower-level tasks.

In Screenplay code, a **[Performance]** is a script of sorts, written from at least one performable, usually several.
All performables must implement one of the following three interfaces, but it's also _strongly recommended_ to implement [`ICanReport`] as well.
It's important to understand that whilst there are three interfaces shown here and three kinds of performable listed above, there is no direct correspondance between them.

* [`IPerformable`]
* [`IPerformableWithResult`]
* [`IPerformableWithResult<TResult>`]

[Actors]: Glossary.md#Actor
[Actions]: Glossary.md#Action
[Tasks]: Glossary.md#Task
[Questions]: Glossary.md#Question
[`IPerformable`]: xref:TODO
[`IPerformableWithResult`]: xref:TODO
[`IPerformableWithResult<TResult>`]: xref:TODO
[`ICanReport`]: xref:TODO

## Action

An Action is a kind of **[Performable]** in which an **[Actor]** does something or interacts with the application in such as way as to change its state.
Specifically, an action should be the smallest, most granular change or interaction possible; something which cannot reasonably be split into constituent parts.
In an application of Screenplay which controls a web browser, an action might be a single mouse click, or entering some specified text into an input field.
To create higher-level interactions, use **[Tasks]** to compose actions.
Out of the kinds of performable, actions are the smallest building blocks available.

Actions don't have a direct representation in Screenplay code because they are really just an arbitrary category of performable.
Actions _most commonly_ implement the [`IPerformable`] interface though, as they usually do not return any results.

Generally, the logic of actions interacts directly with the actor's **[Abilities]** in order to provide the functionality required to perform the action.

[Performable]: Glossary.md#Performable
[Actor]: Glossary.md#Actor
[Abilities]: Glossary.md#Ability

## Question

A Question is a kind of **[Performable]** in which an **[Actor]** gets or reads some information from the application, ideally in such a way that does not change the application's state.
Similar to **[Actions]**, questions should be as small in their scope as possible, to make them as reusable and composable as possible.
In code, questions get a value and return it to the consuming logic, so they will always implement one of [`IPerformableWithResult<TResult>`] or its non-generic counterpart [`IPerformableWithResult`].

In an application of Screenplay which controls a web browser, a questions might represent reading the text from a single HTML element, or reading the enabled/disabled state of a button.
To create higher-level questions with broader scope, compose them with **[Tasks]**.

Generally, the logic of questions interacts directly with the actor's **[Abilities]** in order to provide the functionality required to get the requested information.

## Task

A Task is a kind of **[Performable]** which represents a higher-level interaction or reading of the application's state than just **[Actions]** or **[Questions]**.
Wheras action and question classes are often shipped with Screenplay frameworks, Tasks are typically written by the developer who is making use of Screenplay.

Tasks could implement any of the three performable interfaces, because their scope is unlimited.
To maximise the reusability of tasks, though, strongly consider writing your tasks in a way which interacts with the application and changes its state or in a way which gets information from the application without changing its state.
This draws from [the lessons that writing pure functions] teaches us.

In a Screenplay which controls a web application, a good example of a task is completing a registration form which involves entering data into multiple input fields.

Generally, the logic of tasks does not interact with the actor's **[Abilities]**, because logic which does this is held within actions and/or questions.
If a task needed to use an actor's abilities directly then that usage should likely be moved into an action or question class of its own.
That action or question would then be consumed from the task.

[the lessons that writing pure functions]: https://en.wikipedia.org/wiki/Pure_function

## Actor

An Actor is a representation of an autonomous, or at least seemingly-autonomous, person or system which controls the performance.
Each **[Performable]** in the **[Performance]** is performed/executed within the context of a single actor.

The most common example of an actor is a human being using the application.

## Ability

## Persona

## Cast

## Stage

## Report

## Scenario

## Integration
