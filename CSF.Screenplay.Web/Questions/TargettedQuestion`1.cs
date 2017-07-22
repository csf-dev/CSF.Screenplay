using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// A <see cref="T:Question{T}"/> which gets a piece of information from either an <see cref="ITarget"/> or a
  /// Selenium <c>IWebElement</c> instance.
  /// </summary>
  public class TargettedQuestion<T> : Question<T>
  {
    readonly ITarget target;
    IWebElement element;
    readonly IQuery<T> query;

    /// <summary>
    /// Gets a report of the current question.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    protected override string GetReport(INamed actor)
    {
      return query.GetQuestionReport(actor, GetTargetName());
    }

    /// <summary>
    /// Gets the answer to the question.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="actor">The actor asking this question.</param>
    protected override T GetAnswer(IPerformer actor)
    {
      var ability = GetAbility(actor);
      var ele = GetWebElement(ability);
      var adapter = GetWebElementAdapter(ele);
      return GetAnswer(actor, ability, adapter);
    }

    /// <summary>
    /// Gets the <see cref="BrowseTheWeb"/> ability instance from the given actor.
    /// </summary>
    /// <returns>The ability.</returns>
    /// <param name="actor">Actor.</param>
    protected virtual BrowseTheWeb GetAbility(IPerformer actor)
    {
      return actor.GetAbility<BrowseTheWeb>();
    }

    /// <summary>
    /// Gets a Selenium <c>IWebElement</c> to interrogate for the answer to the question.
    /// </summary>
    /// <returns>The web element.</returns>
    /// <param name="ability">The appropriate browse-the-web ability.</param>
    protected virtual IWebElement GetWebElement(BrowseTheWeb ability)
    {
      if(element != null)
        return element;

      element = WebElementProvider.Instance.GetElement(ability, target);
      return element;
    }

    /// <summary>
    /// Gets a <see cref="IWebElementAdapter"/> instance which wraps the given <c>IWebElement</c>.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="element">The Selenium <c>IWebElement</c>.</param>
    protected virtual IWebElementAdapter GetWebElementAdapter(IWebElement element)
    {
      return new SeleniumWebElementAdapter(element);
    }

    /// <summary>
    /// Gets the human-readable name of the target upon which this question operates.
    /// </summary>
    /// <returns>The target name.</returns>
    protected virtual string GetTargetName()
    {
      if(target != null)
        return target.GetName();

      if(element != null)
        return $"the <{element.TagName}> element";

      // In theory this can't happen because one of the two null-checks above will have returned true
      return "the element";
    }

    /// <summary>
    /// Gets the answer for the current instance, using information from the actor, their web browsing ability and
    /// a <see cref="IWebElementAdapter"/> representing the current targetted element.
    /// </summary>
    /// <returns>The question answer.</returns>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The actor's web-browsing ability.</param>
    /// <param name="adapter">The element adapter.</param>
    protected virtual T GetAnswer(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      return query.GetElementData(adapter);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TargettedQuestion{T}"/> class using an <see cref="ITarget"/>
    /// instance.
    /// </summary>
    /// <param name="target">The target to inspect for the answer.</param>
    protected TargettedQuestion(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TargettedQuestion{T}"/> class using a Selenium
    /// <c>IWebElement</c> instance.
    /// </summary>
    /// <param name="element">The element to inspect for the answer.</param>
    protected TargettedQuestion(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      this.element = element;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TargettedQuestion{T}"/> class using an <see cref="ITarget"/>
    /// instance.
    /// </summary>
    /// <param name="target">The target to inspect for the answer.</param>
    /// <param name="query">A service type which will provide the answers to this question.</param>
    public TargettedQuestion(ITarget target, IQuery<T> query)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      if(query == null)
        throw new ArgumentNullException(nameof(query));

      this.target = target;
      this.query = query;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TargettedQuestion{T}"/> class using a Selenium
    /// <c>IWebElement</c> instance.
    /// </summary>
    /// <param name="element">The element to inspect for the answer.</param>
    /// <param name="query">A service type which will provide the answers to this question.</param>
    public TargettedQuestion(IWebElement element, IQuery<T> query)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));
      if(query == null)
        throw new ArgumentNullException(nameof(query));

      this.element = element;
      this.query = query;
    }
  }
}
