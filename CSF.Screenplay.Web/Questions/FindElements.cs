using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.ElementMatching;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// Finds a collection of web page elements, within a parent target.
  /// </summary>
  public class FindElements : TargettedQuestion<ElementCollection>
  {
    readonly IMatcher matcher;
    readonly ITarget innerTarget;
    readonly string elementGroupName;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets {GetGroupName()} from {GetTargetName()}{GetMatchDescription()}.";
    }

    /// <summary>
    /// Gets the answer for the current instance, using information from the actor, their web browsing ability and
    /// a <see cref="IWebElementAdapter"/> representing the current targetted element.
    /// </summary>
    /// <returns>The question answer.</returns>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The actor's web-browsing ability.</param>
    /// <param name="adapter">The element adapter.</param>
    protected override ElementCollection GetAnswer(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      var elements = GetElements(adapter);
      var matching = GetMatchingElements(elements);
      return GetElementCollection(matching);
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

    /// <summary>
    /// Initializes a new instance of the <see cref="FindElements"/> class from a target.
    /// </summary>
    /// <param name="target">The target within which to search for elements.</param>
    /// <param name="innerTarget">An optional target which the found elements must match.</param>
    /// <param name="matcher">An optional matcher providing criteria for the matched elements.</param>
    /// <param name="elementGroupName">An optional logical/human-readable name for the matched elements.</param>
    public FindElements(ITarget target,
                        ITarget innerTarget = null,
                        IMatcher matcher = null,
                        string elementGroupName = null) : base(target)
    {
      this.innerTarget = innerTarget;
      this.matcher = matcher;
      this.elementGroupName = elementGroupName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FindElements"/> class from a Selenium element.
    /// </summary>
    /// <param name="element">The element within which to search for elements.</param>
    /// <param name="innerTarget">An optional target which the found elements must match.</param>
    /// <param name="matcher">An optional matcher providing criteria for the matched elements.</param>
    /// <param name="elementGroupName">An optional logical/human-readable name for the matched elements.</param>
    public FindElements(IWebElement element,
                        ITarget innerTarget = null,
                        IMatcher matcher = null,
                        string elementGroupName = null) : base(element)
    {
      this.innerTarget = innerTarget;
      this.matcher = matcher;
      this.elementGroupName = elementGroupName;
    }
  }
}
