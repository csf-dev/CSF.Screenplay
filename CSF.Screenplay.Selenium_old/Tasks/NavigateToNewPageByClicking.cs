using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Tasks
{
  /// <summary>
  /// A task which navigates to a new page via a click (typically on a hyperlink element).
  /// </summary>
  /// <remarks>
  /// <para>
  /// Please do not rely on the timeouts to test 'failures to navigate in time'.  Various web browsers behave very
  /// differently with regard to these wait times.  For some (such as Google Chrome) the wait time is somewhat
  /// irrelevant because it will automatically wait for pages to load.  Others (such as Safari on OSX) will not
  /// block like Chrome does and will indeed timeout if the page has not loaded in time.
  /// </para>
  /// <para>
  /// Thus, the advice for using this timeout is to provide a high enough value that you are confident that the
  /// page will have loaded in that time.  But expect many web browsers to ignore it and succeed in performing
  /// the navigation anyway, even if the timeout you specify is too low.
  /// </para>
  /// </remarks>
  public class NavigateToNewPageByClicking : Performable
  {
    static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(4);

    static readonly ILocatorBasedTarget CurrentHtmlElement = new XPath("/html", "the current web page");

    readonly TimeSpan pageStalenessTimeout, pageReadyTimeout;
    readonly ITarget clickTarget;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} navigates to a new page by clicking on {clickTarget.GetName()}";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Tasks.NavigateToNewPageByClicking"/> class.
    /// </summary>
    /// <param name="clickTarget">Click target.</param>
    public NavigateToNewPageByClicking(ITarget clickTarget)
    {
      if(clickTarget == null)
        throw new ArgumentNullException(nameof(clickTarget));

      this.clickTarget = clickTarget;
      this.pageStalenessTimeout = DefaultTimeout;
      this.pageReadyTimeout = DefaultTimeout;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Tasks.NavigateToNewPageByClicking"/> class.
    /// </summary>
    /// <param name="clickTarget">Click target.</param>
    /// <param name="pageStalenessTimeout">Page staleness timeout.</param>
    /// <param name="pageReadyTimeout">Page ready timeout.</param>
    public NavigateToNewPageByClicking(ITarget clickTarget, TimeSpan pageStalenessTimeout, TimeSpan pageReadyTimeout)
      : this(clickTarget)
    {
      this.pageStalenessTimeout = pageStalenessTimeout;
      this.pageReadyTimeout = pageReadyTimeout;
    }
  }
}
