# Writing builders

The first step toward writing builders for Screenplay [Performables] is to create a `static` class.
This static class will act as the entry point to building performables and is typically consumed via [a `using static` directive].

Such a class may have many methods, each representing a different use case.
Normally, it would be bad practice to fill a class with a large number of methods.
Because (as shown below) the methods tend to be very short - typically only one line - and because of the convenience, consider using one static class for all related builders/performables in a logical package.

[Performables]: ../../glossary/Performable.md
[a `using static` directive]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive#the-static-modifier

## Performables with no parameters

To build a performable which requires no parameters, only the static method is required.
Good names for such a static method begin with a **verb**, which indicates that it does something.

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

Where it comes to performables which have more than one parameter then it is often best for the static entry point method to instead return a specialised builder for that performable. 
That specialised builder type exposes further methods which may be used to provide the other parameters in a human-readable manner.

Particularly if one or more of the parameters is optional, consider writing [an implicit conversion operator overload] for your specialised builder type.
The return type of such an operator should be the appropriate concrete performable type.
Such an operator would allow the use of the builder to get the performable without needing an explicit `.Build()` method.

> [!TIP]
> Beware of [writing performables with too many parameters]; this can make them difficult to consume. 
> Consider writing high-level performables with fewer parameters, representing actual use cases.

[an implicit conversion operator overload]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators
[writing performables with too many parameters]: ../writingPerformables/ParameterizeLowLevelPerformables.md

### Example

Here is an example of a fictitious builder.

```csharp
public class MakeAHotDrinkBuilder
{
  readonly string drinkType;
  string whitener;
  int sugars;

  public MakeAHotDrinkBuilder WithMilk()
  {
    whitener = "Milk";
    return this;
  }
  
  public MakeAHotDrinkBuilder WithCream()
  {
    whitener = "Cream";
    return this;
  }

  public MakeAHotDrinkBuilder WithSugars(int howMany)
  {
    sugars = howMany;
    return this;
  }

  public MakeAHotDrinkBuilder(string drinkType)
  {
    this.drinkType = drinkType;
  }

  public static implicit operator MakeACupOfHotDrink(MakeAHotDrinkBuilder builder) => new MakeACupOfHotDrink(builder.drinkType, builder.whitener, sugars);
}

// Separately, in a static entry-point class: 
public static MakeAHotDrinkBuilder MakeACupOf(string drinkType)
  => new MakeAHotDrinkBuilder(drinkType);
```

The above example might be consumed from a [Task] in the following manner: 

```csharp
await actor.PerformAsync(MakeACupOf("Tea").WithMilk().WithSugars(2), cancellationToken);
```

[Task]: ../../glossary/Task.md
