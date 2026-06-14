# Writing Performables

Screenplay and add-on libraries will ship with pre-created [Actions] and [Questions], two of the three types of [Performable].
Developers making use of Screenplay _might not need to write new Actions or Questions_, because they may use and compose the existing ones.

On the other hand, it is very likely that developers will need to write [Tasks], which are the kind of Performable which composes Actions, Questions and/or other Tasks.

[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
[Performable]: ../../glossary/Performable.md
[Tasks]: ../../glossary/Task.md

## A sample Task

Here is an annotated example of a [Task] which makes use of a fictitious Action and Question.

```csharp
// A parameter is constructor injected, so that this task may be parameterised.
// This is entirely optional, if no parameters are needed then none need be added.
public class BuyMilkIfNeeded(int cupsOfCoffee) : IPerformable, ICanReport
{
    // Always implement the ICanReport interface if possible.
    // Write a human-readable report of what the Task does.
    // There's no need to detail the work done by consumed performables; they will get their own reports.
    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} adds milk to their shopping list, if they need it to make {Cups} cup(s) of coffee", actor, cupsOfCoffee);

    public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        // Use of a fictitious Question, remember to await it and to pass the cancellation token.
        // This question, which looks in the refridgerator and gets a decimal which represents the
        // quantity of an item inside, measured in litres, is ficitious.
        // It is consumed here from a fictitious builder method.
        var litresOfMilk = await actor.PerformAsync(LookInTheRefridgeratorFor("Milk").AndTellMeHowMuchRemainsInLitres(), cancellationToken);
        var isEnough = IsThisEnoughMilkToMakeCoffee(cupsOfCoffee, litresOfMilk);
        if(!isEnough)
            // Use of a fictitious Action, again remember to await it and pass the cancellation token.
            await actor.PerformAsync(AddToMyShoppingList("Milk"), cancellationToken);
        
    }

    static bool IsThisEnoughMilkToMakeCoffee(int cupsOfCoffee, decimal litresOfMilk)
    {
        // Implementation omitted, determines if the litresOfMilk param is enough milk to make
        // the indicated number of cupsOfCoffee.
    }
}
```

[Task]: ../../glossary/Task.md

## Guidelines for writing performables

The following list shows some guidelines for writing new performables.
These apply equally across ask if the three types of performables, even though developers are mainly expected to be writing tasks.

* [Implement precisely one performable interface]
* [Implement `ICanReport`]
* [Parameterize low-level performables]
* [Avoid branching logic]
* [Performables should be stateful but immutable]
* [Allow cooperative cancellation]
* [Write a builder]
* [Do not rely on a DI framework]
* [Aim for Pure Functional tasks]
* [Do not interact with Abilities from Tasks]

[Implement precisely one performable interface]: ImplementOnePerformableInterface.md
[Implement `ICanReport`]: ImplementICanReport.md
[Parameterize low-level performables]: ParameterizeLowLevelPerformables.md
[Avoid branching logic]: AvoidBranchingLogic.md
[Performables should be stateful but immutable]: StatefulButImmutable.md
[Allow cooperative cancellation]: AllowCooperativeCancellation.md
[Write a builder]: WriteABuilder.md
[Do not rely on a DI framework]: DoNotUseDiFrameworks.md
[Aim for Pure Functional tasks]: PureFunctionalTasks.md
[Do not interact with Abilities from Tasks]: TasksDoNotUseAbilities.md
