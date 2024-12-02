# Consuming builders

Core to a Screenplay [Performance] are [Performables].
Getting these performables is easiest when using a builder. 
This results in clear, human-readable code inside your performance. 

The entry point to most builders is a `static class` which exposes [factory methods] for the performables which it supports.
These are easiest to consume with [a `using static` directive], such as: 

```csharp
using static MyNamespace.Builders.MyBuilderEntryPoint;
```

This means that the methods of the static entry point class may be used in the positions where a performable is required, such as the following.

```csharp
using static DrinksNamespace.DrinksBuilder;

await actor.PerformAsync(MakeACupOf("Coffee"), cancellationToken);
```

In the example above, the fictitious `MakeACupOf` method is a static method of the fictitious `DrinksBuilder` static entry-point class.

[Performance]: xref:CSF.Screenplay.IPerformance
[Performables]: ../../glossary/Performable.md
[factory methods]: https://en.wikipedia.org/wiki/Factory_method_pattern
[a `using static` directive]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive#the-static-modifier