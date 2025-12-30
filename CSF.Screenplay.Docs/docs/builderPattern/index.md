---
uid: BuilderPatternMainArticle
---

# The builder pattern

As noted elsewhere, [Performables] are _stateful_ objects which _are not resolved from dependency injection_.
The state/dependencies which should be provided to a performable object should be _parameter values_.
For example, a performable which adds a product to a shopping cart might need two pieces of state: 

* Which product to add
* The quantity

Recall that any external dependencies should come from the performing actor's [Abilities].

It is possible to create instances of performable types with just the `new` keyword.
The state could be provided using constructor-injected parameter values or via property setters.
Whilst the techniques above will work, neither are as attractive or easy to read and comprehend as the builder pattern.

[Performables]: ../../glossary/Performable.md
[Abilities]: ../../glossary/Ability.md

* [Writing builders for Screenplay](WritingBuilders.md)
* [Consuming builders to create performables](ConsumingBuilders.md)
