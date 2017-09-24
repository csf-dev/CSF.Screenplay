using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Tasks
{
  public class NavigateToNewPageByClicking : Performable
  {
    static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(4);

    static readonly ILocatorBasedTarget CurrentHtmlElement = new XPath("/html", "the current web page");

    readonly TimeSpan pageStalenessTimeout, pageReadyTimeout;
    readonly ITarget clickTarget;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} navigates to a new page by clicking on {clickTarget.GetName()}";

    protected override void PerformAs(IPerformer actor)
    {
      var outgoingPage = GetReferenceToOutgoingPage(actor);

      actor.Perform(Click.On(clickTarget));

      actor.Perform(Wait.ForAtMost(pageStalenessTimeout)
                        .OrUntil(ExpectedConditions.StalenessOf(outgoingPage), "the page to unload"));

      actor.Perform(Wait.ForAtMost(pageReadyTimeout).OrUntilThePageLoads());
    }

    IWebElement GetReferenceToOutgoingPage(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();

      var oldPage = actor.Perform(Get.TheElement(CurrentHtmlElement));
      return oldPage.GetUnderlyingElement();
    }

    public NavigateToNewPageByClicking(ITarget clickTarget)
    {
      if(clickTarget == null)
        throw new ArgumentNullException(nameof(clickTarget));

      this.clickTarget = clickTarget;
      this.pageStalenessTimeout = DefaultTimeout;
      this.pageReadyTimeout = DefaultTimeout;
    }

    public NavigateToNewPageByClicking(ITarget clickTarget, TimeSpan pageStalenessTimeout, TimeSpan pageReadyTimeout)
      : this(clickTarget)
    {
      this.pageStalenessTimeout = pageStalenessTimeout;
      this.pageReadyTimeout = pageReadyTimeout;
    }
  }
}
