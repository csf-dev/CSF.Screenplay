# Glossary

This page offers a glossary of Screenplay terminology.

## Screenplay

A Screenplay (used as a noun) refers to a complete execution of the Screenplay software.
A Screenplay is composed of at least one **[Performance]**, and typically a Screenplay contains many performances. 

In a testing framework, a Screenplay corresponds to a complete test run.

[Performance]: Glossary.md#Performance

## Performance

A Performance is a start-to-finish collection of [performables] which describes a journey to an intended outcome.
A [Screenplay] typically contains many performances.

In a testing framework a Performance corresponds to a single test.
This might alternatively be called a "scenario", "test case" or "theory".

Performances have a direct representation in the Screenplay library: the [`Performance`] class.

[performables]: Glossary.md#Performable
[Screenplay]: Glossary.md#Screenplay
[`Performance`]: xref:TODO

## Performable

A 'performable' is a verb in the language of Screenplay.
Performables are things that [actors] do