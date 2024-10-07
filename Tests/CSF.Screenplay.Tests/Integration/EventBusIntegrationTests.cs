using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using CSF.Screenplay.Stubs;
using Microsoft.Extensions.DependencyInjection;

using static CSF.Screenplay.PerformanceStarter;

namespace CSF.Screenplay.Integration;

[TestFixture,Parallelizable]
public class EventBusIntegrationTests
{
    [Test, AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldEmitCorrectPerformanceEvents(SampleAction sampleAction,
                                                                                  SampleGenericQuestion sampleQuestion,
                                                                                  [DefaultScreenplay] Screenplay sut)
    {
        Actor? createdActor = null;
        bool performanceBegun = false, performanceFinished = false;
        List<object> performablesBegun = [];
        List<object> performablesEnded = [];
        List<object> performableResults = [];
        void OnActorCreated(object? sender, ActorEventArgs ev) => createdActor = ev.Actor;
        void OnBeginPerformable(object? sender, PerformableEventArgs ev) => performablesBegun.Add(ev.Performable);
        void OnEndPerformable(object? sender, PerformableEventArgs ev) => performablesEnded.Add(ev.Performable);
        void OnPerformableResult(object? sender, PerformableResultEventArgs ev) => performableResults.Add(ev.Result);
        void OnPerformanceBegun(object? sender, PerformanceEventArgs ev) => performanceBegun = true;
        void OnPerformanceFinished(object? sender, PerformanceFinishedEventArgs ev) => performanceFinished = true;

        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();
        eventPublisher.PerformanceBegun += OnPerformanceBegun;
        eventPublisher.PerformanceFinished += OnPerformanceFinished;
        eventPublisher.ActorCreated += OnActorCreated;
        eventPublisher.BeginPerformable += OnBeginPerformable;
        eventPublisher.EndPerformable += OnEndPerformable;
        eventPublisher.PerformableResult += OnPerformableResult;

        await sut.ExecuteAsPerformanceAsync(async (s, c) =>
        {
            var cast = s.GetRequiredService<ICast>();
            var joe = cast.GetActor("Joe");

            await When(joe).AttemptsTo(sampleAction, c);
            await Then(joe).Should(sampleQuestion, c);

            return true;
        });

        eventPublisher.PerformanceBegun -= OnPerformanceBegun;
        eventPublisher.PerformanceFinished -= OnPerformanceFinished;
        eventPublisher.ActorCreated -= OnActorCreated;
        eventPublisher.BeginPerformable -= OnBeginPerformable;
        eventPublisher.EndPerformable -= OnEndPerformable;
        eventPublisher.PerformableResult -= OnPerformableResult;

        Assert.Multiple(() =>
        {
            Assert.That(performanceBegun, Is.True, $"{nameof(OnPerformanceBegun)} was triggered");
            Assert.That(performanceFinished, Is.True, $"{nameof(OnPerformanceFinished)} was triggered");
            Assert.That(createdActor, Has.Property(nameof(IHasName.Name)).EqualTo("Joe"), $"{nameof(OnActorCreated)} was triggered");
            Assert.That(performablesBegun, Is.EqualTo(new object[] { sampleAction, sampleQuestion }),  $"{nameof(OnBeginPerformable)} was triggered with the right performables");
            Assert.That(performablesEnded, Is.EqualTo(new object[] { sampleAction, sampleQuestion }),  $"{nameof(OnEndPerformable)} was triggered with the right performables");
            Assert.That(performableResults, Is.EqualTo(new object[] { "Joe" }),  $"{nameof(OnPerformableResult)} was triggered with the right performables");
        });
    }

    [Test,AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldEmitCorrectSpotlightEvents([DefaultScreenplay] Screenplay sut)
    {
        Actor? spotlitActor = null;
        bool spotlightOff = false;
        void OnActorSpotlit(object? sender, ActorEventArgs e) => spotlitActor = e.Actor;
        void OnSpotlightTurnedOff(object? sender, PerformanceScopeEventArgs e) => spotlightOff = true;

        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();

        eventPublisher.ActorSpotlit += OnActorSpotlit;
        eventPublisher.SpotlightTurnedOff += OnSpotlightTurnedOff;

        await sut.ExecuteAsPerformanceAsync((s, c) =>
        {
            var stage = s.GetRequiredService<IStage>();
            var joe = stage.Cast.GetActor("Joe");
            stage.Spotlight(joe);
            stage.TurnSpotlightOff();

            return Task.FromResult<bool?>(true);
        });

        eventPublisher.ActorSpotlit -= OnActorSpotlit;
        eventPublisher.SpotlightTurnedOff -= OnSpotlightTurnedOff;

        Assert.Multiple(() =>
        {
            Assert.That(spotlitActor, Has.Property(nameof(IHasName.Name)).EqualTo("Joe"), $"{nameof(OnActorSpotlit)} was triggered");
            Assert.That(spotlightOff, Is.True, $"{nameof(OnSpotlightTurnedOff)} was triggered");
        });
    }

    [Test,AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldEmitCorrectActorAbilityEvents(object anAbility,
                                                                                   [DefaultScreenplay] Screenplay sut)
    {
        object? capturedAbility = null;
        void OnGainedAbility(object? sender, GainAbilityEventArgs e) => capturedAbility = e.Ability;

        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();

        eventPublisher.GainedAbility += OnGainedAbility;

        await sut.ExecuteAsPerformanceAsync((s, c) =>
        {
            var cast = s.GetRequiredService<ICast>();
            var joe = cast.GetActor("Joe");
            joe.IsAbleTo(anAbility);

            return Task.FromResult<bool?>(true);
        });

        eventPublisher.GainedAbility -= OnGainedAbility;

        Assert.That(capturedAbility, Is.SameAs(anAbility), $"{nameof(OnGainedAbility)} was triggered");
    }

    [Test,AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldEmitCorrectPerformableFailureEventsWhenItThrows(ThrowingAction performable,
                                                                                                     [DefaultScreenplay] Screenplay sut)
    {
        Exception? exceptionCaught = null;
        bool? result = null;
        void OnPerformableFailed(object? sender, PerformableFailureEventArgs e) => exceptionCaught = e.Exception;
        void OnPerformanceFinished(object? sender, PerformanceFinishedEventArgs e) => result = e.Success;

        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();

        eventPublisher.PerformableFailed += OnPerformableFailed;
        eventPublisher.PerformanceFinished += OnPerformanceFinished;

        await sut.ExecuteAsPerformanceAsync(async (s, c) =>
        {
            var cast = s.GetRequiredService<ICast>();
            var joe = cast.GetActor("Joe");
            await When(joe).AttemptsTo(performable, c);

            return true;
        });

        eventPublisher.PerformableFailed -= OnPerformableFailed;
        eventPublisher.PerformanceFinished -= OnPerformanceFinished;

        Assert.Multiple(() =>
        {
            Assert.That(exceptionCaught,
                        Is.InstanceOf<InvalidOperationException>().And.Property(nameof(Exception.Message)).EqualTo(ThrowingAction.Message),
                        $"{nameof(OnPerformableFailed)} was triggered");
            Assert.That(result, Is.False, $"{nameof(OnPerformanceFinished)} was triggered");
        });
    }

    [Test,AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldRecordThePerformanceAsAFailureIfItReturnsFalse([DefaultScreenplay] Screenplay sut)
    {
        bool? result = null;
        void OnPerformanceFinished(object? sender, PerformanceFinishedEventArgs e) => result = e.Success;

        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();

        eventPublisher.PerformanceFinished += OnPerformanceFinished;

        await sut.ExecuteAsPerformanceAsync((s, c) =>
        {
            return Task.FromResult<bool?>(false);
        });

        eventPublisher.PerformanceFinished -= OnPerformanceFinished;

        Assert.That(result, Is.False, $"{nameof(OnPerformanceFinished)} was triggered");
    }

    [Test,AutoMoqData]
    public void BeginScreenplayShouldEmitTheCorrectEvent([DefaultScreenplay] Screenplay sut)
    {
        bool started = false;
        void OnScreenplayStarted(object? sender, EventArgs e) => started = true;
        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();

        eventPublisher.ScreenplayStarted += OnScreenplayStarted;
        sut.BeginScreenplay();
        eventPublisher.ScreenplayStarted -= OnScreenplayStarted;

        Assert.That(started, Is.True);
    }
    
    [Test,AutoMoqData]
    public void CompleteScreenplayShouldEmitTheCorrectEvent([DefaultScreenplay] Screenplay sut)
    {
        bool ended = false;
        void OnScreenplayEnded(object? sender, EventArgs e) => ended = true;
        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();

        eventPublisher.ScreenplayEnded += OnScreenplayEnded;
        sut.CompleteScreenplay();
        eventPublisher.ScreenplayEnded -= OnScreenplayEnded;

        Assert.That(ended, Is.True);
    }

    [Test,AutoMoqData]
    public async Task RecordAssetShouldEmitTheCorrectEvent([DefaultScreenplay] Screenplay sut, string expectedPath, string expectedSummary, object performable)
    {
        string? filePath = null, fileSummary = null;
        void OnRecordsAsset(object? sender, PerformableAssetEventArgs e)
        {
            filePath = e.FilePath;
            fileSummary = e.FileSummary;
        };

        var eventPublisher = sut.ServiceProvider.GetRequiredService<IHasPerformanceEvents>();
        eventPublisher.RecordAsset += OnRecordsAsset;
        
        await sut.ExecuteAsPerformanceAsync((s, c) =>
        {
            var cast = s.GetRequiredService<ICast>();
            var joe = cast.GetActor("Joe");
            ((ICanPerform) joe).RecordAsset(performable, expectedPath, expectedSummary);

            return Task.FromResult<bool?>(true);
        });

        eventPublisher.RecordAsset -= OnRecordsAsset;

        Assert.Multiple(() =>
        {
            Assert.That(filePath, Is.EqualTo(expectedPath), "File path");
            Assert.That(fileSummary, Is.EqualTo(expectedSummary), "File summary");
        });
    }
}