using System;
using CSF.Screenplay.Builders;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Queries;
using CSF.Screenplay.Selenium.Waits;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builder type which creates targetted waits.
  /// </summary>
  public class Wait
  {
    static readonly IProvidesTimespan DefaultTimeout = new TimespanWrapper(TimeSpan.FromSeconds(10));

    IProvidesTimespan timespanProvider;
    ITarget target;

    /// <summary>
    /// Gets a builder instance for a wait with a specified timeout.
    /// </summary>
    /// <returns>A timespan builder.</returns>
    /// <param name="timeValue">The amount of milliseconds, seconds or minutes to wait.</param>
    public static TimespanBuilder<Wait> ForAtMost(int timeValue)
    {
      var builder = new Wait();
      var wrapper = TimespanBuilder.Create(timeValue, builder);
      builder.timespanProvider = wrapper;
      return wrapper;
    }

    /// <summary>
    /// Gets a builder instance for a wait with a specified timeout.
    /// </summary>
    /// <returns>A timespan builder.</returns>
    /// <param name="timespan">The amount of time to wait.</param>
    public static Wait ForAtMost(TimeSpan timespan) => new Wait { timespanProvider = new TimespanWrapper(timespan) };

    /// <summary>
    /// Gets a builder for a general-purpose wait.
    /// </summary>
    /// <param name="timeValue">Time value.</param>
    public static GeneralWaitBuilder For(int timeValue)
    {
      return new GeneralWaitBuilder(timeValue);
    }

    /// <summary>
    /// Gets a builder for a given target, with the default timeout.
    /// </summary>
    /// <returns>A wait builder.</returns>
    /// <param name="target">Target.</param>
    public static Wait Until(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      
      return new Wait
      {
        target = target,
        timespanProvider = DefaultTimeout,
      };
    }

    /// <summary>
    /// Gets a wait action for a given condition.
    /// </summary>
    /// <returns>A wait builder.</returns>
    /// <param name="expectedCondition">An expected condition.</param>
    /// <param name="conditionName">A name for the condition.</param>
    public static IPerformable Until(Func<IWebDriver,bool> expectedCondition, string conditionName)
    {
      return new WaitForACondition(expectedCondition, conditionName, DefaultTimeout.GetTimespan());
    }

    /// <summary>
    /// Gets a 'wait' performable which completes once the page has finished loading, using the default timeout.
    /// </summary>
    /// <returns>The performable.</returns>
    public static IPerformable UntilThePageLoads() => new WaitUntilThePageLoads(DefaultTimeout.GetTimespan());

    /// <summary>
    /// Gets a 'wait' performable which completes once the page has finished loading.
    /// </summary>
    /// <returns>The performable.</returns>
    public IPerformable OrUntilThePageLoads() => new WaitUntilThePageLoads(timespanProvider.GetTimespan());

    /// <summary>
    /// Gets a wait builder for a given target.
    /// </summary>
    /// <returns>A wait builder.</returns>
    /// <param name="target">Target.</param>
    public Wait OrUntil(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      if(this.target != null)
        throw new InvalidOperationException("You may not choose the target more than once.");

      this.target = target;
      return this;
    }

    /// <summary>
    /// Gets a wait action for a given condition.
    /// </summary>
    /// <returns>A wait builder.</returns>
    /// <param name="expectedCondition">An expected condition.</param>
    /// <param name="conditionName">A name for the condition.</param>
    public IPerformable OrUntil(Func<IWebDriver,bool> expectedCondition, string conditionName)
    {
      return new WaitForACondition(expectedCondition, conditionName, timespanProvider.GetTimespan());
    }

    /// <summary>
    /// Gets a 'wait' performable which completes when a specified text string is displayed in the target.
    /// </summary>
    /// <returns>The the text.</returns>
    /// <param name="text">Text.</param>
    public IPerformable ShowsTheText(string text)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));
      
      return GetTargettedWait(new TextQuery(), x => x == text);
    }

    /// <summary>
    /// Gets a 'wait' performable which completes when a specified text substring is displayed in the target.
    /// </summary>
    /// <returns>The the text.</returns>
    /// <param name="text">Text.</param>
    public IPerformable ContainsTheText(string text)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));
      
      return GetTargettedWait(new TextQuery(), x => x != null && x.Contains(text));
    }

    /// <summary>
    /// Gets a 'wait' performable which completes when the target is visible to the user.
    /// </summary>
    /// <returns>The visible.</returns>
    public IPerformable IsVisible()
    {
      var wait = GetTargettedWait(new VisibilityQuery(), x => x);
      wait.IgnoredExceptionTypes.Add(typeof(NotFoundException));
      wait.IgnoredExceptionTypes.Add(typeof(TargetNotFoundException));
      return wait;
    }

    /// <summary>
    /// Gets a 'wait' performable which completes when the target is visible to the user.
    /// </summary>
    /// <returns>The visible.</returns>
    public IPerformable IsClickable()
    {
      var wait = GetTargettedWait(new ClickableQuery(), x => x);
      wait.IgnoredExceptionTypes.Add(typeof(NotFoundException));
      wait.IgnoredExceptionTypes.Add(typeof(TargetNotFoundException));
      return wait;
    }

    ITargettedWait GetTargettedWait<T>(IQuery<T> query, Func<T,bool> predicate)
    {
      if(query == null)
        throw new ArgumentNullException(nameof(query));
      if(predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      if(target == null)
        throw new InvalidOperationException("You must choose a target.");

      return new TargettedWait<T>(target, query, predicate, timespanProvider.GetTimespan());
    }
  }
}
