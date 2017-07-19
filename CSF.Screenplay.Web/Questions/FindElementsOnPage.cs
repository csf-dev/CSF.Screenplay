using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class FindElementsOnPage : Performable<ElementCollection>, IQuestion<ElementCollection>
  {
    static readonly ITarget thePage;
    readonly IPerformable<ElementCollection> findWithinBody;

    protected override string GetReport(INamed actor)
    {
      return findWithinBody.GetReport(actor);
    }

    protected override ElementCollection PerformAs(IPerformer actor)
    {
      return findWithinBody.PerformAs(actor);
    }

    public FindElementsOnPage(ITarget target = null, IElementMatcher matcher = null, string elementGroupName = null)
    {
      findWithinBody = new FindElements(thePage, target, matcher, elementGroupName);
    }

    static FindElementsOnPage()
    {
      thePage = new CssSelector("body", "the page");
    }
  }
}
