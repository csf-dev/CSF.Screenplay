# Avoid branching logic

[Performances] are written _a lot like scripts_.
Where possible, [Performables] should avoid branching or looping logic.
_This is particularly true_ when Screenplay is being used [as a tool for testing].
Good test logic has [a cyclomatic complexity] of precisely one.

Sometimes looping logic is unavoidable and desirable in a Screenplay, imagine a performable which has an [Actor] repeat a process `N` times.
This is acceptable if used judiciously.

Performables should always avoid branching logic like `if` or `switch` though, and _should never_ contain such logic when being used for tests.
If more than one mode of operation is required then write more than one performable.
The path through a Performance should be completely deterministic, short of an unexpected error or failure.

[Performances]: xref:CSF.Screenplay.IPerformance
[Performables]: ../../glossary/Performable.md
[as a tool for testing]: ../bestPractice/SuitabilityAsATestingTool.md
[a cyclomatic complexity]: https://en.wikipedia.org/wiki/Cyclomatic_complexity
[Actor]: xref:CSF.Screenplay.Actor
