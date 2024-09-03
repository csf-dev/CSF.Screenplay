# Task

A Task is a kind of **[Performable]** which represents a high-level interaction with the application.
Where **[Actions]** or **[Questions]** are highly granular for maximum reusability, tasks may be as specific as the use-case warrants.

Action & question classes are often shipped with Screenplay frameworks.
Tasks are typically written by the developer who is making use of Screenplay.
In practice, tasks are just compositions of actions, questions or other lower-level tasks.

In a Screenplay which controls a web application, an example of a task is the completion of a registration form which involves entering data into multiple input fields.

[Performable]: Performable.md
[Actions]: Action.md
[Questions]: Question.md

## Writing tasks

Tasks may implement [any of the three performable interfaces].
Developers are encouraged to [follow these best practices] when writing Task classes.

[any of the three performable interfaces]: Performable.md#the-three-performable-interfaces-and-icanreport
[follow these best practices]: ../docs/writingPerformables/index.md
