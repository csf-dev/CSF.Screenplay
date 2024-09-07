using CSF.Screenplay.Abilities;
using static CSF.Screenplay.Performables.StopwatchBuilder;
using static CSF.Screenplay.PerformanceStarter;

namespace CSF.Screenplay.Performables;

[TestFixture,Parallelizable]
public class StopwatchTests
{
    [Test,AutoMoqData]
    public async Task StartingAndReadingTheStopwatchShouldProducePlausibleResults()
    {
        var actor = new Actor("Joe", Guid.NewGuid());
        actor.IsAbleTo(new UseAStopwatch());

        await Given(actor).WasAbleTo(StartTheStopwatch());
        await Task.Delay(TimeSpan.FromMilliseconds(150));
        var elapsed = await Then(actor).Should(ReadTheStopwatch());
        await Then(actor).Should(StopTheStopwatch());

        // OS precision of Task.Delay is typically 15.6 milliseconds, so we allow ±25ms tolerance and that should be fine
        Assert.That(elapsed, Is.EqualTo(TimeSpan.FromMilliseconds(150)).Within(TimeSpan.FromMilliseconds(25)));
    }

    [Test,AutoMoqData]
    public async Task ReadingTheStopwatchAfterStoppedShouldProduceTheSameResult()
    {
        var actor = new Actor("Joe", Guid.NewGuid());
        actor.IsAbleTo(new UseAStopwatch());
        
        await Given(actor).WasAbleTo(StartTheStopwatch());
        await Task.Delay(TimeSpan.FromMilliseconds(150));
        await When(actor).AttemptsTo(StopTheStopwatch());
        var elapsed1 = await Then(actor).Should(ReadTheStopwatch());
        await Task.Delay(TimeSpan.FromMilliseconds(50));
        var elapsed2 = await Then(actor).Should(ReadTheStopwatch());

        Assert.That(elapsed1, Is.EqualTo(elapsed2));
    }

    [Test,AutoMoqData]
    public async Task ResettingTheStopwatchWhilstStoppedShouldProduceAZeroResult()
    {
        var actor = new Actor("Joe", Guid.NewGuid());
        actor.IsAbleTo(new UseAStopwatch());

        await Given(actor).WasAbleTo(StartTheStopwatch());
        await Task.Delay(TimeSpan.FromMilliseconds(150));
        await When(actor).AttemptsTo(StopTheStopwatch());
        await When(actor).AttemptsTo(ResetTheStopwatch());
        var elapsed = await Then(actor).Should(ReadTheStopwatch());

        Assert.That(elapsed, Is.EqualTo(TimeSpan.Zero));
    }
}
