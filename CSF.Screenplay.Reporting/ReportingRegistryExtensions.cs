using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  public static class ReportingRegistryExtensions
  {
    public static void RegisterReporter(this IServiceRegistryBuilder builder,
                                        IReporter reporter,
                                        string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(reporter == null)
        throw new ArgumentNullException(nameof(reporter));
      
      builder.RegisterSingleton(reporter, name);
      if(reporter is IModelBuildingReporter)
      {
        builder.RegisterSingleton((IModelBuildingReporter) reporter, name);
      }
    }
  }
}
