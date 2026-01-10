using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture]
public class ExecuteJavaScriptAndGetResultTests
{
    const string scriptBody1 = """
                               const element = arguments[0];
                               const bgColor = arguments[1];
                               element.style.backgroundColor = bgColor;
                               """;
    const string scriptBody2 = """
                               const element = arguments[0];
                               return element.style.backgroundColor;
                               """;
    static readonly NamedUri testPage = new NamedUri("OpenUrlTests.html", "the test page");

    static readonly Locator textContent = new ElementId("textContent", "the text content");

    [Test, Screenplay]
    public async Task ExecuteJavaScriptAndGetResultShouldBeAbleToExecuteAScriptWithParameters(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var element = await Given(webster).WasAbleTo(FindAnElementOnThePage().WhichMatches(textContent));
        await When(webster).AttemptsTo(ExecuteCustomScript(scriptBody1).WithTheName("a script that changes the BG colour of an element").WithTheArguments(element.WebElement, "#CCF"));
        var backgroundColor = await Then(webster).Should(ExecuteCustomScriptWithResult<string>(scriptBody2).WithTheName("a script that gets the BG colour").WithTheArguments(element.WebElement));

        Assert.That(backgroundColor, Is.EqualTo("rgb(204, 204, 255)"));
    }
}