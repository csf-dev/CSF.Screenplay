using System.Text.Json;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Reporting;

[TestFixture, Parallelizable]
public class JsonScreenplayReporterTest
{
    [Test, AutoMoqData]
    public void SubscribeTo_ShouldSubscribeToEvents([Frozen] Mock<IHasPerformanceEvents> mockEvents,
                                                    [WithMemoryStream] JsonScreenplayReporter sut)
    {
        sut.SubscribeTo(mockEvents.Object);

        Assert.Multiple(() =>
        {
            mockEvents.VerifyAdd(e => e.ScreenplayStarted += It.IsAny<EventHandler>(), Times.Once);
            mockEvents.VerifyAdd(e => e.ScreenplayEnded += It.IsAny<EventHandler>(), Times.Once);
            mockEvents.VerifyAdd(e => e.PerformanceBegun += It.IsAny<EventHandler<PerformanceEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.PerformanceFinished += It.IsAny<EventHandler<PerformanceFinishedEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.BeginPerformable += It.IsAny<EventHandler<PerformableEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.EndPerformable += It.IsAny<EventHandler<PerformableEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.PerformableResult += It.IsAny<EventHandler<PerformableResultEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.PerformableFailed += It.IsAny<EventHandler<PerformableFailureEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.RecordAsset += It.IsAny<EventHandler<PerformableAssetEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.ActorCreated += It.IsAny<EventHandler<ActorEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.GainedAbility += It.IsAny<EventHandler<GainAbilityEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.ActorSpotlit += It.IsAny<EventHandler<ActorEventArgs>>(), Times.Once);
            mockEvents.VerifyAdd(e => e.SpotlightTurnedOff += It.IsAny<EventHandler<PerformanceScopeEventArgs>>(), Times.Once);
        });
    }

    [Test, AutoMoqData]
    public void UnsubscribeFrom_ShouldUnsubscribeFromEvents([Frozen] Mock<IHasPerformanceEvents> mockEvents,
                                                            [WithMemoryStream] JsonScreenplayReporter sut)
    {
        sut.SubscribeTo(mockEvents.Object);

        sut.UnsubscribeFrom(mockEvents.Object);

        Assert.Multiple(() =>
        {
            mockEvents.VerifyRemove(e => e.ScreenplayStarted -= It.IsAny<EventHandler>(), Times.Once);
            mockEvents.VerifyRemove(e => e.ScreenplayEnded -= It.IsAny<EventHandler>(), Times.Once);
            mockEvents.VerifyRemove(e => e.PerformanceBegun -= It.IsAny<EventHandler<PerformanceEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.PerformanceFinished -= It.IsAny<EventHandler<PerformanceFinishedEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.BeginPerformable -= It.IsAny<EventHandler<PerformableEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.EndPerformable -= It.IsAny<EventHandler<PerformableEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.PerformableResult -= It.IsAny<EventHandler<PerformableResultEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.PerformableFailed -= It.IsAny<EventHandler<PerformableFailureEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.RecordAsset -= It.IsAny<EventHandler<PerformableAssetEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.ActorCreated -= It.IsAny<EventHandler<ActorEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.GainedAbility -= It.IsAny<EventHandler<GainAbilityEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.ActorSpotlit -= It.IsAny<EventHandler<ActorEventArgs>>(), Times.Once);
            mockEvents.VerifyRemove(e => e.SpotlightTurnedOff -= It.IsAny<EventHandler<PerformanceScopeEventArgs>>(), Times.Once);
        });
    }

    [Test, AutoMoqData]
    public void Dispose_ShouldUnsubscribeFromEvents([Frozen] Mock<IHasPerformanceEvents> mockEvents,
                                                    [WithMemoryStream] JsonScreenplayReporter sut)
    {
        sut.SubscribeTo(mockEvents.Object);

        sut.Dispose();

        mockEvents.VerifyRemove(e => e.ScreenplayStarted -= It.IsAny<EventHandler>(), Times.Once);
    }
}