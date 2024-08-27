using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.ReportModel;
using Ploeh.AutoFixture.Kernel;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class ReportableSpecimenBuilder : ISpecimenBuilder
  {
    public object Create(object request, ISpecimenContext context)
    {
      var type = request as Type;
      if(type == null) return new NoSpecimen();
      if(!typeof(Reportable).IsAssignableFrom(type)) return new NoSpecimen();
        
      return CreatePerformance(context);
    }

    Reportable CreatePerformance(ISpecimenContext context)
    {
      var category = SelectPerformanceCategory();
      switch(category)
      {
      case PerformanceCategory.FailureWithException:
        return CreateFailurePerformance(context);

      case PerformanceCategory.SuccessWithResult:
        return CreateSuccessPerformanceWithResult(context);

      case PerformanceCategory.SuccessWithChildren:
        return CreateSuccessPerformanceWithChildren(context);

      case PerformanceCategory.GainAbility:
        return CreateGainAbility(context);

      default:
        return CreateSuccessPerformance(context);
      }
    }

    Reportable CreateSuccessPerformance(ISpecimenContext context)
    {
      return CreateReportable(context,
                               ReportableType.Success);
    }

    Reportable CreateSuccessPerformanceWithChildren(ISpecimenContext context)
    {
      var howManyChildren = ScenarioCustomisation.Randomiser.Next(1, 5);
      var children = Enumerable.Range(0, howManyChildren)
                               .Select(x => context.Resolve(typeof(Reportable)))
                               .Cast<Reportable>()
                               .ToArray();
      return CreateReportable(context,
                               ReportableType.Success,
                               children: children);
    }

    Reportable CreateSuccessPerformanceWithResult(ISpecimenContext context)
    {
      return CreateReportable(context,
                               ReportableType.SuccessWithResult,
                               context.Resolve(typeof(Guid)));
    }

    Reportable CreateFailurePerformance(ISpecimenContext context)
    {
      return CreateReportable(context,
                               ReportableType.FailureWithError,
                               exception: (Exception) context.Resolve(typeof(Exception)));
    }

    Reportable CreateGainAbility(ISpecimenContext context)
    {
      return CreateReportable(context, ReportableType.GainAbility);
    }

    Reportable CreateReportable(ISpecimenContext context,
                                ReportableType reportableType,
                                  object result = null,
                                  Exception exception = null,
                                  IList<Reportable> children = null)
    {
      var actor = (INamed) context.Resolve(typeof(INamed));
      var category = SelectReportableCategory();

      return new Reportable {
        ActorName = actor.Name,
        Category = category,
        Error = exception?.ToString(),
        Type = reportableType,
        Report = (string) context.Resolve(typeof(string)),
        Reportables = children,
        Result = result?.ToString(),
      };
    }

    ReportableCategory SelectReportableCategory()
    {
      var randomNumber = ScenarioCustomisation.Randomiser.Next(0, 3);
      if(randomNumber == 0) return ReportableCategory.Given;
      if(randomNumber == 1) return ReportableCategory.When;
      return ReportableCategory.Then;
    }

    PerformanceCategory SelectPerformanceCategory()
    {
      var randomNumber = ScenarioCustomisation.Randomiser.Next(0, 10);

      if(randomNumber < 4) return PerformanceCategory.Success;
      if(randomNumber < 5) return PerformanceCategory.GainAbility;
      if(randomNumber < 7) return PerformanceCategory.SuccessWithResult;
      if(randomNumber < 9) return PerformanceCategory.SuccessWithChildren;
      return PerformanceCategory.FailureWithException;
    }

    enum PerformanceCategory
    {
      Success,
      SuccessWithChildren,
      SuccessWithResult,
      FailureWithException,
      GainAbility
    }
  }
}
