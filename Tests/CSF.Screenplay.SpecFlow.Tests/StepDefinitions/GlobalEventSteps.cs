using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.StepDefinitions;

/// <summary>
/// Not strictly a test, but a demonstration that all of the correct events are triggered during a SpecFlow test run.
/// </summary>
/// <remarks>
/// <para>
/// This is not a test in the usual sense, because it is rather difficult to perform a true test. What we are proving here is that
/// the correct Screenplay architectural-level logic is executing, whilst exercising Screenplay within a SpecFlow test.
/// The logic in the class writes console output which is reading during a test run, showing that the relevant events have been
/// triggered with appropriate parameter values.
/// </para>
/// </remarks>
[Binding]
public class GlobalEventSteps
{
    static void OnScreenplayStarted(object? sender, EventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnScreenplayStarted)}");
    }

    static void OnScreenplayEnded(object? sender, EventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnScreenplayEnded)}");
    }

    static void OnPerformanceBegun(object? sender, PerformanceEventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnPerformanceBegun)}: {e.PerformanceIdentity}");
    }

    static void OnPerformanceFinished(object? sender, PerformanceFinishedEventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnPerformanceFinished)}: {e.PerformanceIdentity}");
    }

    static void OnBeginPerformable(object? sender, PerformableEventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnBeginPerformable)}: {e.Performable.GetType()}");
    }

    static void OnEndPerformable(object? sender, PerformableEventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnEndPerformable)}: {e.Performable.GetType()}");
    }

    static void OnPerformableResult(object? sender, PerformableResultEventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnPerformableResult)}: {e.Performable.GetType()}, {e.Result}");
    }

    static void OnActorCreated(object? sender, ActorEventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnActorCreated)}: {e.ActorName}");
    }

    static void OnGainedAbility(object? sender, GainAbilityEventArgs e)
    {
        Console.Error.WriteLine($"{nameof(OnGainedAbility)}: {e.ActorName}, {e.Ability.GetType()}");
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var eventBus = ScreenplayPlugin.Screenplay.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();
        eventBus.ScreenplayStarted += OnScreenplayStarted;
        eventBus.ScreenplayEnded += OnScreenplayEnded;
        eventBus.PerformanceBegun += OnPerformanceBegun;
        eventBus.PerformanceFinished += OnPerformanceFinished;
        eventBus.BeginPerformable += OnBeginPerformable;
        eventBus.EndPerformable += OnEndPerformable;
        eventBus.PerformableResult += OnPerformableResult;
        eventBus.ActorCreated += OnActorCreated;
        eventBus.GainedAbility += OnGainedAbility;
    }
}