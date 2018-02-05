using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Waits
{
  /// <summary>
  /// A performable which represents the actor waiting for a condition to become true.
  /// </summary>
  public class TargettedWait<T> : Performable, ITargettedWait
  {
    readonly ITarget target;
    readonly IQuery<T> query;
    readonly Func<T,bool> predicate;
    readonly TimeSpan timeout;
    readonly ISet<Type> ignoredExceptionTypes;
    readonly IDurationFormatter durationFormatter;

    /// <summary>
    /// Gets a collection of <c>System.Type</c> representing exception types which will be ignored during the wait
    /// operation.
    /// </summary>
    /// <value>The ignored exception types.</value>
    public virtual ISet<Type> IgnoredExceptionTypes => ignoredExceptionTypes;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      var actorName = actor.Name;
      var targetName = target.GetName();
      var matchDescription = query.GetMatchDescription();
      var timeoutString = durationFormatter.GetDuration(timeout);

      return $"{actorName} waits for at most {timeoutString} or until {targetName} {matchDescription}.";
    }

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = GetAbility(actor);
      Wait(actor, ability);
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
    /// Gets a <see cref="IWebElementAdapter"/> instance to interrogate for the answer to the question.
    /// </summary>
    /// <returns>The web element.</returns>
    /// <param name="driver">The web driver.</param>
    protected virtual IWebElementAdapter GetWebElementAdapter(IWebDriver driver)
    {
      return target.GetWebElementAdapter(driver);
    }

    /// <summary>
    /// Waits until the condition is satisfied.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    protected virtual void Wait(IPerformer actor, BrowseTheWeb ability)
    {
      var wait = new WebDriverWait(ability.WebDriver, timeout);
      ConfigureWait(wait);
      Wait(actor, wait);
    }

    /// <summary>
    /// Performs additional configuration upon the Wait object before it is executed.
    /// </summary>
    /// <param name="wait">Wait.</param>
    protected virtual void ConfigureWait(IWait<IWebDriver> wait)
    {
      wait.IgnoreExceptionTypes(ignoredExceptionTypes.ToArray());
    }

    /// <summary>
    /// Waits until the condition is satisfied.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="wait">Wait.</param>
    protected virtual void Wait(IPerformer actor,
                                IWait<IWebDriver> wait)
    {
      try
      {
        wait.Until(WaitConditionIsSatisfied);
      }
      catch(WebDriverTimeoutException ex)
      {
        throw new GivenUpWaitingException("Given up waiting", ex);
      }
    }

    /// <summary>
    /// Returns a value indicating whether or not the given wait-condition has been satisfied.
    /// </summary>
    /// <returns><c>true</c>, if the condition was satisfied, <c>false</c> otherwise.</returns>
    /// <param name="driver">A web driver.</param>
    protected virtual bool WaitConditionIsSatisfied(IWebDriver driver)
    {
      var adapter = GetWebElementAdapter(driver);
      var webElementValue = query.GetElementData(adapter);
      return predicate(webElementValue);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TargettedWait{T}"/> class.
    /// </summary>
    /// <param name="target">Target.</param>
    /// <param name="query">Query.</param>
    /// <param name="predicate">Predicate.</param>
    /// <param name="timeout">Timeout.</param>
    public TargettedWait(ITarget target,
                         IQuery<T> query,
                         Func<T,bool> predicate,
                         TimeSpan timeout)
    {
      if(predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      if(query == null)
        throw new ArgumentNullException(nameof(query));
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
      this.query = query;
      this.predicate = predicate;
      this.timeout = timeout;

      ignoredExceptionTypes = new HashSet<Type>();
      durationFormatter = new DurationFormatter();
    }
  }
}
