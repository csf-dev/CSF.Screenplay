using System;
namespace CSF.Screenplay.Web.Builders
{
  public class TimespanWrapper : IProvidesTimespan
  {
    TimeSpan timespan;

    TimeSpan IProvidesTimespan.GetTimespan() => timespan;

    public TimespanWrapper(TimeSpan timespan)
    {
      this.timespan = timespan;
    }
  }
}
