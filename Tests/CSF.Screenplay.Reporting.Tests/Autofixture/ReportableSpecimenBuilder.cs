using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Models;
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
      return CreatePerformance(context,
                               ReportableType.Success);
    }

    Reportable CreateSuccessPerformanceWithChildren(ISpecimenContext context)
    {
      var howManyChildren = ScenarioCustomisation.Randomiser.Next(1, 5);
      var children = Enumerable.Range(0, howManyChildren)
                               .Select(x => context.Resolve(typeof(Reportable)))
                               .Cast<Reportable>()
                               .ToArray();
      return CreatePerformance(context,
                               ReportableType.Success,
                               children: children);
    }

    Reportable CreateSuccessPerformanceWithResult(ISpecimenContext context)
    {
      return CreatePerformance(context,
                               ReportableType.SuccessWithResult,
                               context.Resolve(typeof(Guid)));
    }

    Reportable CreateFailurePerformance(ISpecimenContext context)
    {
      return CreatePerformance(context,
                               ReportableType.FailureWithError,
                               exception: (Exception) context.Resolve(typeof(Exception)));
    }

    Reportable CreatePerformance(ISpecimenContext context,
                                  ReportableType outcome,
                                  object result = null,
                                  Exception exception = null,
                                  IList<Reportable> children = null)
    {
      var actor = (INamed) context.Resolve(typeof(INamed));
      var performable = (Reportable) context.Resolve(typeof(Reportable));
      var performanceType = SelectPerformanceType();

      return new Performance(actor, outcome, performable, performanceType, result, exception, children);
    }

    ReportableCategory SelectPerformanceType()
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
