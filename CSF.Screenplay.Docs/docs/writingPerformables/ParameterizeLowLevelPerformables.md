# Parameterize low-level Performables

When writing low-level [Performables], these classes should expose parameters which allow the consumer to provide as many of the variables as make sense.
This _is especially important for [Actions] and/or [Questions]_ but also relevant for lower-level/reusable [Tasks].

Parameters should be accepted via the class' public constructor, the values to these parameters [should then be held `readonly`].
Accept as many parameters as are reasonable, although avoid going so far that parameterisation makes a Performable unclear as to what it does.

[Performables]: ../../glossary/Performable.md
[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
[Tasks]: ../../glossary/Task.md
[should then be held `readonly`]: StatefulButImmutable.md

## An example

Imagine we are writing a Task which makes a cup of coffee, ready to serve.
It would make sense to include parameters which decide the strength of the coffee, how much milk and sweetener to add and similar.

It would usually be a mistake to create a Task which can make _any hot drink_, where the parameters provided decide which hot drink to make.
The processes for making various hot drinks are often fundamentally different; consider the process for making coffee and then that for brewing tea.

In the most extreme case, where such a Task is required, separate the logic of making of each hot drink into Tasks of their own, and consume/execute the relevant one of these from the _any hot drink_ Task.
