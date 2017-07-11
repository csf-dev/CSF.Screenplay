using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class FindElements : TargettedQuestion<ElementCollection>
  {
    readonly IElementMatcher matcher;
    readonly string elementGroupName;

    protected override string GetReport(INamed actor)
    {
      var report = $"{actor.Name} gets {elementGroupName?? "elements"} from within {GetTargetName()}";

      if(matcher != null)
      {
        report += $" which {matcher.GetDescription()}";
      }
        
      return report;
    }

    protected override ElementCollection PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      var elements = GetElements(adapter);
      var wrapped = GetWrappedElements(elements);
      var matching = GetMatchingElements(wrapped);
      return GetElementCollection(matching);
    }

    IEnumerable<IWebElement> GetElements(IWebElementAdapter adapter)
    {
      if(matcher?.TargetMatch != null)
        return adapter.Find(matcher.TargetMatch);

      return adapter.Find();
    }

    IEnumerable<IWebElementAdapter> GetWrappedElements(IEnumerable<IWebElement> source)
    {
      return source.Select(x => new WebElementAdapter(x)).ToArray();
    }

    IEnumerable<IWebElementAdapter> GetMatchingElements(IEnumerable<IWebElementAdapter> source)
    {
      if(matcher == null)
        return source;
      
      return source.Where(matcher.GetMatchPredicate()).ToArray();
    }

    ElementCollection GetElementCollection(IEnumerable<IWebElementAdapter> source)
    {
      var elements = source.Select(x => x.GetSourceElement()).ToArray();
      return new ElementCollection(elements, elementGroupName);
    }

    public FindElements(ITarget target, IElementMatcher matcher = null, string elementGroupName = null) : base(target)
    {
      this.matcher = matcher;
      this.elementGroupName = elementGroupName;
    }

    public FindElements(IWebElement element, IElementMatcher matcher = null, string elementGroupName = null) : base(element)
    {
      this.matcher = matcher;
      this.elementGroupName = elementGroupName;
    }
  }
}
