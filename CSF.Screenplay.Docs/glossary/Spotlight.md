# Spotlight

When the [Stage] is used to get an [Actor], that actor is placed _in the **Spotlight**_.
That same actor remains in the spotlight until a different actor is placed there.
Note, though, that a Stage and thus its corresponding Spotlight is scoped to the current [Performance].
A different performance has a completely independent Stage and Spotlight.

A stage may be used at any time to get the actor who is currently in the spotlight.
Thus, it is possible to infer an actor in [Performance] logic without needing to use their name.

See the documentation for the [Stage] for more information.

[Performance]: xref:CSF.Screenplay.IPerformance
[Actor]: xref:CSF.Screenplay.Actor
[Stage]: xref:CSF.Screenplay.IStage