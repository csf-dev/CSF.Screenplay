# Tasks should not use abilities

The logic of [Task classes] should not interact with the actor's **[Abilities]**.
Logic which interacts with abilities should be limited to [Action] and/or [Question] classes.

Move logic which needs to interact with Abilities into Action/Question classes and ensure that they are appropriately parameterized.
Consume such actions or questions from your custom Task class.

[Task classes]: ../../glossary/Task.md
[Abilities]: ../../glossary/Ability.md
[Action]: ../../glossary/Action.md
[Question]: ../../glossary/Question.md
