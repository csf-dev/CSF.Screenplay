# Avoid branching logic

[Performances] are written _a lot like scripts_.
Where possible, [Performables] should avoid branching or looping logic.
_This is particularly true_ when Screenplay is being used [as a tool for testing].
Good test logic has [a cyclomatic complexity] as close to one as possible.

Sometimes looping logic is unavoidable and desirable in a Screenplay, imagine a performable which has an [Actor] repeat a process `N` times.
Sometimes branching logic is required to deal with external systems which present differing behaviour.
The [Selenium Extension] makes use of this technique to accomodate different web browsers and web driver implementations.
Such logic is acceptable if used judiciously.

Perhaps the best guideline is for the developer to ask themselves:

> Could I separate this into two or more tasks and decide which I need at the point of usage?

If the answer is "yes", then it's often better to do just that, rather than write a multi-function Task.

[Performances]: xref:CSF.Screenplay.IPerformance
[Performables]: ../../glossary/Performable.md
[as a tool for testing]: ../bestPractice/SuitabilityAsATestingTool.md
[a cyclomatic complexity]: https://en.wikipedia.org/wiki/Cyclomatic_complexity
[Actor]: xref:CSF.Screenplay.Actor
[Selenium Extension]: ../extensions/selenium/index.md