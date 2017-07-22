using System;
using System.Collections.Generic;
using System.Linq;
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
  /// A <see cref="T:Question{T}"/> which gets a collection of information from either an <see cref="ITarget"/>
  /// (representing a collection of elements) or a collection of  Selenium <c>IWebElement</c> instances.
  /// </summary>
  public class TargettedMultiQuestion<T> : Question<IReadOnlyList<T>>
  {
    readonly ITarget target;
    IReadOnlyList<IWebElement> elements;
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
    protected override IReadOnlyList<T> GetAnswer(IPerformer actor)
    {
      var ability = GetAbility(actor);
      var ele = GetWebElements(ability);
      var adapters = GetWebElementAdapters(ele);
      return GetAnswer(actor, ability, adapters);
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
    /// Gets a collection of Selenium <c>IWebElement</c> to interrogate for the answer to the question.
    /// </summary>
    /// <returns>The web elements.</returns>
    /// <param name="ability">The appropriate browse-the-web ability.</param>
    protected virtual IReadOnlyList<IWebElement> GetWebElements(BrowseTheWeb ability)
    {
      if(elements != null)
        return elements;

      elements = WebElementProvider.Instance.GetElements(ability, target);
      return elements;
    }

    /// <summary>
    /// Gets a <see cref="IWebElementAdapter"/> instance which wraps the given <c>IWebElement</c>.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="elements">The Selenium <c>IWebElement</c>.</param>
    protected virtual IReadOnlyList<IWebElementAdapter> GetWebElementAdapters(IEnumerable<IWebElement> elements)
    {
      return elements.Select(x => new SeleniumWebElementAdapter(x)).ToArray();
    }

    /// <summary>
    /// Gets the human-readable name of the target upon which this question operates.
    /// </summary>
    /// <returns>The target name.</returns>
    protected virtual string GetTargetName()
    {
      if(target != null)
        return target.GetName();

      return "the elements";
    }

    /// <summary>
    /// Gets the answer for the current instance, using information from the actor, their web browsing ability and
    /// a <see cref="IWebElementAdapter"/> representing the current targetted element.
    /// </summary>
    /// <returns>The question answer.</returns>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The actor's web-browsing ability.</param>
    /// <param name="adapters">The element adapter.</param>
    protected virtual IReadOnlyList<T> GetAnswer(IPerformer actor,
                                                 BrowseTheWeb ability,
                                                 IReadOnlyList<IWebElementAdapter> adapters)
    {
      return adapters.Select(x => GetAnswer(actor, ability, x)).ToArray();
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
    /// <param name="query">A service type which will provide the answers to this question.</param>
    public TargettedMultiQuestion(ITarget target, IQuery<T> query)
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
    /// <param name="elements">The element to inspect for the answer.</param>
    /// <param name="query">A service type which will provide the answers to this question.</param>
    public TargettedMultiQuestion(IReadOnlyList<IWebElement> elements, IQuery<T> query)
    {
      if(elements == null)
        throw new ArgumentNullException(nameof(elements));
      if(query == null)
        throw new ArgumentNullException(nameof(query));

      this.elements = elements;
      this.query = query;
    }
  }
}
