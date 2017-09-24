using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Tasks;

namespace CSF.Screenplay.Web.Builders
{
  public class Navigate
  {
    IProvidesTimespan timeoutBuilder;

    public static IPerformable ToAnotherPageByClicking(ITarget target)
      => new NavigateToNewPageByClicking(target);

    public static TimespanBuilder<Navigate> WaitingUpTo(int amount)
    {
      var builder = new Navigate();
      var timespanBuilder = TimespanBuilder.Create(amount, builder);
      builder.timeoutBuilder = timespanBuilder;

      return timespanBuilder;
    }

    public IPerformable ToADifferentPageByClicking(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      var timeout = timeoutBuilder.GetTimespan();
      return new NavigateToNewPageByClicking(target, timeout, timeout);
    }
  }
}
