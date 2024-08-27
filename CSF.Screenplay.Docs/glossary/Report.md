# Report

After a **[Screenplay]** has completed, the Screenplay software may produce a machine-readable report of what occurred.
This report may be read, processed, stored or transformed into an alternative format as desired.

The report is hierarchical; at its topmost level are **[Features]** and within are **[Scenarios]**.
The scenario directly corresponds to a single **[Performance]**, and within are contained all of the **[Performables]** for that performance.

Performables in a report are also included hierarchically. This means that high-level **[Tasks]** contain information about the performables from which they are composed.

Reports are useful to document what has been performed in a Screenplay.
They help developers diagnose and debug issues when they arise.

[Screenplay]: xref:CSF.Screenplay.Screenplay
[Features]: Feature.md
[Scenarios]: Scenario.md
[Performance]: xref:CSF.Screenplay.Performance
[Performables]: Performable.md
[Tasks]: Task.md