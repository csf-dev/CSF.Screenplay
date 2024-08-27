# Task

A Task is a kind of **[Performable]** which represents a high-level interaction with the application.
Where **[Actions]** or **[Questions]** are highly granular for maximum reusability, tasks may be as specific as the use-case warrants. 

Action & question classes are often shipped with Screenplay frameworks.
Tasks are typically written by the developer who is making use of Screenplay.
In practice, tasks are just compositions of actions, questions or other lower-level tasks.

Tasks may implement any of the three performable interfaces.
To maximise their reusability, developers are advised to write tasks in a way which _either_:

* Changes the application's state
* Gets information from the application without changing its state

This draws from [the lessons that writing pure functions] teaches us.

In a Screenplay which controls a web application, a good example of a task is completing a registration form which involves entering data into multiple input fields.

Generally, the logic of tasks does not interact with the actor's **[Abilities]**, because logic which does this is held within actions and/or questions.
If a task needed to use an actor's abilities directly then that usage should likely be moved into an action or question class of its own.
That action or question would then be consumed from the task.

[Performable]: Performable.md
[Actions]: Action.md
[Questions]: Question.md
[the lessons that writing pure functions]: https://en.wikipedia.org/wiki/Pure_function
[Abilities]: Ability.md