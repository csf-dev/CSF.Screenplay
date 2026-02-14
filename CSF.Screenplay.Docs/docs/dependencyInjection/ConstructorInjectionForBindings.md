# Constructor injection for bindings

For frameworks which are based on **binding classes** such as [Reqnroll], services are constructor-injected into binding classes.
Use dependencies injected in this way to get access to [commonly-used Screenplay services] and anything else required at the root level of your test logic.

[Reqnroll]: https://reqnroll.net/
[commonly-used Screenplay services]: InjectableServices.md

## Example

This is an example of a Reqnroll-style binding class with a constructor-injected dependency.
This would require [installing the Reqnroll test integration] to run.
Take note of the `stage` parameter in the primary constructor.

```csharp
[Binding]
public class SampleSteps(IStage stage)
{
    [Given("Simon shows a sample step")]
    public async Task GivenSimonShowsASampleStep()
    {
        var simon = stage.Spotlight<Simon>();

        // ... use the actor with some performables, then make assertions etc
    }
}
```

[installing the Reqnroll test integration]: ../gettingStarted/reqnroll/index.md
