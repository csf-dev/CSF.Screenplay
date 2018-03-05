using System;
using CSF.Screenplay.Builders;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Tasks;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds actions relating to navigating to another page.
  /// </summary>
  public class Navigate
  {
    IProvidesTimespan timeoutBuilder;

    /// <summary>
    /// Gets an action which navigates to another page, via a click (typically a hyperlinks).
    /// </summary>
    /// <returns>The action.</returns>
    /// <param name="target">The click target.</param>
    public static IPerformable ToAnotherPageByClicking(ITarget target)
      => new NavigateToNewPageByClicking(target);

    /// <summary>
    /// Indicates a maximum wait period before the navigation has finished.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Please do not rely on this to test 'failures to navigate in time'.  Various web browsers behave very
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
    /// <returns>The up to.</returns>
    /// <param name="amount">Amount.</param>
    public static TimespanBuilder<Navigate> WaitingUpTo(int amount)
    {
      var builder = new Navigate();
      var timespanBuilder = TimespanBuilder.Create(amount, builder);
      builder.timeoutBuilder = timespanBuilder;

      return timespanBuilder;
    }

    /// <summary>
    /// Gets an action which navigates to another page, via a click (typically a hyperlinks).
    /// </summary>
    /// <returns>The action.</returns>
    /// <param name="target">The click target.</param>
    public IPerformable ToADifferentPageByClicking(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      var timeout = timeoutBuilder.GetTimespan();
      return new NavigateToNewPageByClicking(target, timeout, timeout);
    }
  }
}
