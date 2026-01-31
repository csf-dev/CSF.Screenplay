using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable]
public class ExecuteJavaScriptTests
{
    const string scriptBody = """
                              const elementId = arguments[0];
                              const bgColor = arguments[1];
                              document.getElementById(elementId).style.backgroundColor = bgColor;
                              """;
    static readonly NamedUri testPage = new NamedUri("OpenUrlTests.html", "the test page");

    static readonly ITarget textContent = new ElementId("textContent");

    [Test, Screenplay]
    public async Task ExecuteJavaScriptShouldBeAbleToExecuteAScriptWithParameters(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ExecuteCustomScript(scriptBody).WithTheName("a script that changes the BG colour of an element").WithTheArguments("textContent", "#F00"));
        var backgroundColor = await Then(webster).Should(ReadFromTheElement(textContent).TheCssProperty("background-color"));

        Assert.That(backgroundColor, Is.EqualTo(Colors.RED));
    }
}