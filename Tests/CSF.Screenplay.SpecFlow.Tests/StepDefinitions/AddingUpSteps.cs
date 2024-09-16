using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.AddingUp.AddingUpBuilder;

namespace CSF.Screenplay.StepDefinitions
{
    [Binding]
    public class StepDefinitions(IStage stage)
    {
        [Given(@"Mathias has the number (\d+)")]
        public async Task GivenMathiashasthenumber(int number)
        {
            var mathias = stage.Spotlight<Mathias>();
            await Given(mathias).WasAbleTo(SetTheTotalTo(number));
        }

        [When(@"(?:he|she|they) adds? (\d+)")]
        public async Task Whenheadds(int number)
        {
            var actor = stage.GetSpotlitActor();
            await When(actor).AttemptsTo(Add(number));
        }

        [Then(@"(?:he|she|they) should have the total (\d+)")]
        public async Task Thenheshouldhavethetotal(int number)
        {
            var actor = stage.GetSpotlitActor();
            var total = await Then(actor).Should(GetTheTotal());

            Assert.That(total, Is.EqualTo(number));
        }
    }
}