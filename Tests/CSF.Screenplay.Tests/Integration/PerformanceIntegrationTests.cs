using CSF.Screenplay.Stubs;
using Microsoft.Extensions.DependencyInjection;
using static CSF.Screenplay.PerformanceStarter;

namespace CSF.Screenplay.Integration;

[TestFixture, Parallelizable]
public class PerformanceIntegrationTests
{
    [Test,AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldExecuteThreePerformables(SampleAction sampleAction,
                                                                              SampleQuestion sampleQuestion1,
                                                                              SampleGenericQuestion sampleQuestion2,
                                                                              [DefaultScreenplay] Screenplay screenplay)
    {
        object? question1Result = null;
        string? question2Result = null;

        await screenplay.ExecuteAsPerformanceAsync(async (s, c) => {
            var cast = s.GetRequiredService<ICast>();
            var actor = cast.GetActor("Joe");
            await Given(actor).WasAbleTo(sampleAction, c);
            question1Result = await When(actor).AttemptsTo(sampleQuestion1, c);
            question2Result = await Then(actor).Should(sampleQuestion2, c);
            return true;
        });

        Assert.Multiple(() => {
            Assert.That(sampleAction.ActorName, Is.EqualTo("Joe"), "Action result");
            Assert.That(question1Result, Is.EqualTo(5), "Question 1 result");
            Assert.That(question2Result, Is.EqualTo("Joe"), "Question 2 result");
        });
    }
}