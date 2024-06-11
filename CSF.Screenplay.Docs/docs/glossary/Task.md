# Task

A Task is a kind of **[Performable]** which represents a higher-level interaction or reading of the application's state than just **[Actions]** or **[Questions]**.
Wheras action and question classes are often shipped with Screenplay frameworks, Tasks are typically written by the developer who is making use of Screenplay.

Tasks could implement any of the three performable interfaces, because their scope is unlimited.
To maximise the reusability of tasks, though, strongly consider writing your tasks in a way which interacts with the application and changes its state or in a way which gets information from the application without changing its state.
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
