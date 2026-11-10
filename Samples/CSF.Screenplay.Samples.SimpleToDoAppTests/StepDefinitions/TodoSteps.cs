using System.Linq;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.TodoPerformableBuilder;
using NUnit.Framework;

namespace CSF.Screenplay.StepDefinitions;

[Binding]
public class TodoSteps(IStage stage)
{
    [When("(?:s?he|they) creates? a todo item reading (.+)")]
    public async Task WhenTheyCreateATodoItem(string text)
    {
        var actor = stage.GetSpotlitActor();
        await When(actor).AttemptsTo(CreateATodoReading(text));
    }

    [When("he creates several todo items")]
    public async Task WhenTheyCreateSomeTodoItems(DataTable dataTable)
    {
        var items = dataTable.Rows.Select(x => x.Values.Single()).ToArray();
        var actor = stage.GetSpotlitActor();
        foreach(var item in items)
            await When(actor).AttemptsTo(CreateATodoReading(item));
    }

    [Given("he has created several todo items")]
    public async Task GivenTheyCreatedSomeTodoItems(DataTable dataTable)
    {
        var items = dataTable.Rows.Select(x => x.Values.Single()).ToArray();
        var actor = stage.GetSpotlitActor();
        foreach(var item in items)
            await Given(actor).WasAbleTo(CreateATodoReading(item));
    }

    [Then("the count of displayed todos should be (\\d+)")]
    public async Task ThenAssertTheCountOfDisplayedTodos(int expected)
    {
        var actor = stage.GetSpotlitActor();
        var actual = await Then(actor).Should(GetTheCountOfDisplayedTodos());
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Then("the item displayed at position (\\d+) should read (.+)")]
    public async Task ThenTheItemAtAPositionShouldRead(int position, string expected)
    {
        var actor = stage.GetSpotlitActor();
        var actual = await Then(actor).Should(GetTheTodoItemAtPosition(position));
        Assert.That(actual, Is.EqualTo(expected));
    }

    [When("(?:s?he|they) marks the item at position (\\d+) as completed")]
    public async Task WhenTheyMarkAnItemAsCompleted(int position)
    {
        var actor = stage.GetSpotlitActor();
        await When(actor).AttemptsTo(ToggleTheCompletedStateOfItem(position));
    }
}