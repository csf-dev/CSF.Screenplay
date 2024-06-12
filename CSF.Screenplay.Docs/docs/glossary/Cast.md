# Cast

The Cast is an optional but recommended tool within Screenplay to manage multiple **[Actors]** during a **[Performance]**.

It acts as both [a factory] and as [a registry] for Actors, for the lifetime of each Performance.
When getting an Actor from the Cast, if the actor already exists then that existing actor is returned instead of creating another copy.

The Cast is directly represented in code; it is [an injectable service] of type [`ICast`].
The cast has a lifetime which is automatically scoped to the lifetime of the Performance. 
Each Performance gets its own cast.

[Actors]: Actor.md
[Performance]: Performance.md
[a factory]: https://en.wikipedia.org/wiki/Factory_method_pattern
[a registry]: https://martinfowler.com/eaaCatalog/registry.html
[an injectable service]: ../dependencyInjection/index.md
[`ICast`]: xref:CSF.Screenplay.ICast