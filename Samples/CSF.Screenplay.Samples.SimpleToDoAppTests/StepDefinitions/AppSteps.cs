using NUnit.Framework;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.StepDefinitions;

[Binding]
public class AppSteps(IStage stage)
{
    [When("Webster opens the todo app")]
    public async Task WhenWebsterOpensTheTodoApp()
    {
        var webster = stage.Spotlight<Webster>();
        await When(webster).AttemptsTo(NavigateTo(TodoApp.RootUri));
    }

    [Then(@"the page heading should read ""([^""]+)""")]
    public async Task ThenThePageHeadingShouldRead(string expected)
    {
        var actor = stage.GetSpotlitActor();
        var heading = await Then(actor).Should(ReadFromTheElement(TodoApp.PageHeading).TheText());
        
        Assert.That(heading, Is.EqualTo(expected));
    }
}