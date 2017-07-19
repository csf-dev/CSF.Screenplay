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
    readonly ITarget innerTarget;
    readonly string elementGroupName;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets {GetGroupName()} from {GetTargetName()}{GetMatchDescription()}.";
    }

    protected override ElementCollection GetAnswer(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      var elements = GetElements(adapter);
      var matching = GetMatchingElements(elements);
      return GetElementCollection(matching);
    }

    protected override IElementDataProvider<ElementCollection> GetDataProvider()
    {
      throw new NotSupportedException();
    }

    string GetGroupName()
    {
      if(!String.IsNullOrEmpty(elementGroupName))
        return elementGroupName;
      else if(innerTarget != null)
        return innerTarget.GetName();
      else
        return "the elements";
    }

    string GetMatchDescription()
    {
      if(matcher != null)
        return $" which {matcher.GetDescription()}";
      else
        return String.Empty;
    }

    IEnumerable<IWebElement> GetElements(IWebElementAdapter adapter)
    {
      if(innerTarget != null)
        return adapter.Find(innerTarget);

      return adapter.Find();
    }

    IEnumerable<IWebElement> GetMatchingElements(IEnumerable<IWebElement> source)
    {
      if(matcher == null)
        return source;
      
      return source.Where(x => matcher.IsMatch(x));
    }

    ElementCollection GetElementCollection(IEnumerable<IWebElement> source)
    {
      return new ElementCollection(source.ToArray(), elementGroupName);
    }

    public FindElements(ITarget target, ITarget innerTarget = null, IElementMatcher matcher = null, string elementGroupName = null) : base(target)
    {
      this.innerTarget = innerTarget;
      this.matcher = matcher;
      this.elementGroupName = elementGroupName;
    }

    public FindElements(IWebElement element, ITarget innerTarget = null, IElementMatcher matcher = null, string elementGroupName = null) : base(element)
    {
      this.innerTarget = innerTarget;
      this.matcher = matcher;
      this.elementGroupName = elementGroupName;
    }
  }
}
