using Microsoft.Extensions.DependencyInjection;
using static CSF.Screenplay.PerformanceStarter;

namespace CSF.Screenplay.Integration;

[TestFixture, Parallelizable]
public class PerformanceIntegrationTests
{
    [Test]
    public async Task ExecuteAsPerformanceAsyncShouldExecuteThreePerformables()
    {
        var screenplay = new Screenplay();
        var sampleAction = new SampleAction();
        var sampleQuestion1 = new SampleQuestion();
        var sampleQuestion2 = new SampleGenericQuestion();
        object? question1Result = null;
        string? question2Result = null;

        await screenplay.ExecuteAsPerformanceAsync(async (s, c) => {
            var cast = s.GetRequiredService<ICast>();
            var actor = cast.GetActor("Joe");
            await Given(actor).WasAbleTo(sampleAction);
            question1Result = await When(actor).AttemptsTo(sampleQuestion1);
            question2Result = await Then(actor).Should(sampleQuestion2);
            return true;
        });

        Assert.Multiple(() => {
            Assert.That(sampleAction.ActorName, Is.EqualTo("Joe"), "Action result");
            Assert.That(question1Result, Is.EqualTo(5), "Question 1 result");
            Assert.That(question2Result, Is.EqualTo("Joe"), "Question 2 result");
        });
    }

    class SampleAction : IPerformable
    {
        public string? ActorName { get; private set; }

        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            ActorName = ((IHasName) actor).Name;
            return ValueTask.CompletedTask;
        }
    }

    class SampleQuestion : IPerformableWithResult
    {
        public ValueTask<object> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<object>(5);
        }
    }

    class SampleGenericQuestion : IPerformableWithResult<string>
    {
        public ValueTask<string> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var actorName = ((IHasName) actor).Name;
            return ValueTask.FromResult(actorName);
        }
    }
}