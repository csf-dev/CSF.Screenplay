using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Selenium.Actions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Tasks;

[TestFixture, Parallelizable]
public class OpenUrlRespectingBaseTests
{
    [Test, AutoMoqData]
    public async Task TheActionCreatedByThisTaskShouldContainTheCorrectReport(IWebDriver driver, DriverOptions options)
    {
        var actor = new Actor("Anthony", Guid.NewGuid());
        List<IPerformable> performables = new List<IPerformable>();

        void OnPerform(object? sender, PerformableEventArgs ev) => performables.Add((IPerformable)ev.Performable);

        var namedUri = new NamedUri("test.html", "the test page");
        var baseUri = "https://example.com";
        actor.IsAbleTo(new UseABaseUri(new Uri(baseUri, UriKind.Absolute)));
        actor.IsAbleTo(new BrowseTheWeb(Mock.Of<IGetsWebDriver>(x => x.GetDefaultWebDriver(It.IsAny<Action<DriverOptions>>()) == new WebDriverAndOptions(driver, options))));
        var sut = new NavigateToUrl(namedUri);
        var valueFormatterProvider = new ValueFormatterProvider(new ServiceCollection().AddTransient<ToStringFormatter>().BuildServiceProvider(),
                                                                new ValueFormatterRegistry { typeof(ToStringFormatter) });
        
        var formatter = new ReportFragmentFormatter(new ReportFormatCreator(), valueFormatterProvider);

        actor.EndPerformable += OnPerform;
        try
        {
            await sut.PerformAsAsync(actor);

            using var scope = Assert.EnterMultipleScope();
                Assert.That(performables, Has.One.InstanceOf<OpenUrl>(), "Performable is correct type");
                Assert.That(performables.OfType<OpenUrl>().Single().GetReportFragment(actor, formatter).ToString(),
                            Is.EqualTo("Anthony opens their browser at the test page: https://example.com/test.html"),
                            "The report is correct");
        }
        finally
        {
            actor.EndPerformable -= OnPerform;
        }
    }

    [Test, AutoMoqData]
    public async Task TheActionCreatedByThisTaskShouldNotUpdateAnAlreadyAbsoluteUrl(IWebDriver driver, DriverOptions options)
    {
        var actor = new Actor("Anthony", Guid.NewGuid());
        List<IPerformable> performables = new List<IPerformable>();

        void OnPerform(object? sender, PerformableEventArgs ev) => performables.Add((IPerformable)ev.Performable);

        var namedUri = new NamedUri("https://contoso.com/test.html", "the test page");
        actor.IsAbleTo(new BrowseTheWeb(Mock.Of<IGetsWebDriver>(x => x.GetDefaultWebDriver(It.IsAny<Action<DriverOptions>>()) == new WebDriverAndOptions(driver, options))));
        var sut = new NavigateToUrl(namedUri);
        var valueFormatterProvider = new ValueFormatterProvider(new ServiceCollection().AddTransient<ToStringFormatter>().BuildServiceProvider(),
                                                                new ValueFormatterRegistry { typeof(ToStringFormatter) });
        
        var formatter = new ReportFragmentFormatter(new ReportFormatCreator(), valueFormatterProvider);

        actor.EndPerformable += OnPerform;
        try
        {
            await sut.PerformAsAsync(actor);

        using var scope = Assert.EnterMultipleScope();
                Assert.That(performables, Has.One.InstanceOf<OpenUrl>(), "Performable is correct type");
                Assert.That(performables.OfType<OpenUrl>().Single().GetReportFragment(actor, formatter).ToString(),
                            Is.EqualTo("Anthony opens their browser at the test page: https://contoso.com/test.html"),
                            "The report is correct");
        }
        finally
        {
            actor.EndPerformable -= OnPerform;
        }
    }
}