using NUnit.Framework;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;
using static CSF.Screenplay.PerformableBuilder;

namespace CSF.Screenplay.StepDefinitions;

[Binding]
public class AppSteps(IStage stage)
{
    [Given("Webster has opened the todo app")]
    public async Task GivenWebsterOpenedTheTodoApp()
    {
        var webster = stage.Spotlight<Webster>();
        await Given(webster).WasAbleTo(OpenTheApp());
    }

    [When("Webster opens the todo app")]
    public async Task WhenWebsterOpensTheTodoApp()
    {
        var webster = stage.Spotlight<Webster>();
        await When(webster).AttemptsTo(OpenTheApp());
    }

    [Then(@"the page heading should read ""([^""]+)""")]
    public async Task ThenThePageHeadingShouldRead(string expected)
    {
        var actor = stage.GetSpotlitActor();
        var heading = await Then(actor).Should(ReadFromTheElement(TodoApp.PageHeading).TheText());
        
        Assert.That(heading, Is.EqualTo(expected));
    }
}
