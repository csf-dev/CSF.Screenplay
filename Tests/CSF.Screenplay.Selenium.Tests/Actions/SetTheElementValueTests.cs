using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using Moq;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable, Category("WebDriver")]
public class SetTheElementValueTests
{
    [Test, AutoMoqData]
    public async Task PerformAsAsyncShouldUseTheSimulatedInteractiveScriptIfApplicable([MockDriver] BrowseTheWeb browseTheWeb,
                                                                                       object value,
                                                                                       Actor actor,
                                                                                       ITarget target,
                                                                                       SeleniumElement element)
    {
        actor.IsAbleTo(browseTheWeb);
        Mock.Get(target).Setup(x => x.GetElement(browseTheWeb.WebDriver)).Returns(element);

        var sut = SetTheValueOf(target).To(value).AsIfSetInteractively();
        await sut.PerformAsAsync(actor);
        
        Mock.Get(browseTheWeb.WebDriver)
            .As<IJavaScriptExecutor>()
            .Verify(x => x.ExecuteScript(Scripts.SetElementValueSimulatedInteractively.ScriptBody, element.WebElement, value));
    }

    [Test, AutoMoqData]
    public async Task PerformAsAsyncShouldUseTheNonInteractiveScriptInteractiveNotRequested([MockDriver] BrowseTheWeb browseTheWeb,
                                                                                            object value,
                                                                                            Actor actor,
                                                                                            ITarget target,
                                                                                            SeleniumElement element)
    {
        actor.IsAbleTo(browseTheWeb);
        Mock.Get(target).Setup(x => x.GetElement(browseTheWeb.WebDriver)).Returns(element);

        var sut = ((IGetsPerformable) SetTheValueOf(target).To(value)).GetPerformable();
        await sut.PerformAsAsync(actor);
        
        Mock.Get(browseTheWeb.WebDriver)
            .As<IJavaScriptExecutor>()
            .Verify(x => x.ExecuteScript(Scripts.SetElementValue.ScriptBody, element.WebElement, value));
    }
}