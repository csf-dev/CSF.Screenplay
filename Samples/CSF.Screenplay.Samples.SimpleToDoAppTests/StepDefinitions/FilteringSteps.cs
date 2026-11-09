using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.TodoPerformableBuilder;

namespace CSF.Screenplay.StepDefinitions;

[Binding]
public class FilteringSteps(IStage stage)
{
    [When("(?:s?he|they) clicks? the filter named ([^\\s]+)")]
    public async Task WhenTheyClickTheFilter(string filterName)
    {
        var actor = stage.GetSpotlitActor();
        await When(actor).AttemptsTo(ActivateAFilter(filterName));
    }

    [Given("(?:s?he|they) clicked the filter named ([^\\s]+)")]
    public async Task GivenTheyClickedTheFilter(string filterName)
    {
        var actor = stage.GetSpotlitActor();
        await Given(actor).WasAbleTo(ActivateAFilter(filterName));
    }
}