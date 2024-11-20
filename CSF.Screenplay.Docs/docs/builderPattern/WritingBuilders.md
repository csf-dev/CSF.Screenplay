# Writing builders

The first step toward writing builders for Screenplay [Performables] is to create a `static` class. 
This class should be used as the basis for all related performables.

[Performables]: ../../glossary/Performable.md

## Performables with no parameters

To build a performable which requires no parameters, a simple static method is best.
Here is an example for a fictitious `MakeACupOfTea` performable.

```csharp
public static IPerformble MakeTheTea()
    => new MakeACupOfTea();
```

Substitute the performable interface (the return type) as applicable.

## Performables with one parameter

If a performable requires one constructor-injected parameter then it is often possible to work this into the method name of a simple static method, similar to above. 
Consider this fictitious example. 

```csharp
public static IPerformble MakeACupOf(string hotDrink)
    => new MakeACupOfHotDrink(hotDrink);
```

This might be consumed as `MakeACupOf("Coffee")`, for example.
This is still very readable.

## Performables with many parameters

Where it comes to performables which have more than one parameter then it is often best for the static builder method to install return a specialised builder for that performable. 
That specialised builder type exposes further methods which may be used to provide those other parameters in a human-readable manner.

Particularly if one or more of the parameters is optional, consider writing [an implicit conversion operator overload] for your specialised builder type, to the appropriate performable interface.
Such an operator would allow the use of the builder to get the performable without needing an explicit `.Build()` method.

[an implicit conversion operator overload]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators