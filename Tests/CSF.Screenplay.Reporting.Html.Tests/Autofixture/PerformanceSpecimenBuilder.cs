using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Models;
using Ploeh.AutoFixture.Kernel;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class PerformanceSpecimenBuilder : ISpecimenBuilder
  {
    public object Create(object request, ISpecimenContext context)
    {
      var type = request as Type;
      if(type == null) return new NoSpecimen();
      if(!typeof(Performance).IsAssignableFrom(type)) return new NoSpecimen();
        
      return CreatePerformance(context);
    }

    Performance CreatePerformance(ISpecimenContext context)
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

      default:
        return CreateSuccessPerformance(context);
      }
    }

    Performance CreateSuccessPerformance(ISpecimenContext context)
    {
      return CreatePerformance(context,
                               PerformanceOutcome.Success);
    }

    Performance CreateSuccessPerformanceWithChildren(ISpecimenContext context)
    {
      var howManyChildren = ScenarioCustomisation.Randomiser.Next(1, 5);
      var children = Enumerable.Range(0, howManyChildren)
                               .Select(x => context.Resolve(typeof(Performance)))
                               .Cast<Performance>()
                               .ToArray();
      return CreatePerformance(context,
                               PerformanceOutcome.Success,
                               children: children);
    }

    Performance CreateSuccessPerformanceWithResult(ISpecimenContext context)
    {
      return CreatePerformance(context,
                               PerformanceOutcome.SuccessWithResult,
                               context.Resolve(typeof(Guid)));
    }

    Performance CreateFailurePerformance(ISpecimenContext context)
    {
      return CreatePerformance(context,
                               PerformanceOutcome.FailureWithException,
                               exception: (Exception) context.Resolve(typeof(Exception)));
    }

    Performance CreatePerformance(ISpecimenContext context,
                                  PerformanceOutcome outcome,
                                  object result = null,
                                  Exception exception = null,
                                  IList<Reportable> children = null)
    {
      var actor = (INamed) context.Resolve(typeof(INamed));
      var performable = (IPerformable) context.Resolve(typeof(IPerformable));
      var performanceType = SelectPerformanceType();

      return new Performance(actor, outcome, performable, performanceType, result, exception, children);
    }

    PerformanceType SelectPerformanceType()
    {
      var randomNumber = ScenarioCustomisation.Randomiser.Next(0, 3);
      if(randomNumber == 0) return PerformanceType.Given;
      if(randomNumber == 1) return PerformanceType.When;
      return PerformanceType.Then;
    }

    PerformanceCategory SelectPerformanceCategory()
    {
      var randomNumber = ScenarioCustomisation.Randomiser.Next(0, 10);

      if(randomNumber < 5) return PerformanceCategory.Success;
      if(randomNumber < 7) return PerformanceCategory.SuccessWithResult;
      if(randomNumber < 9) return PerformanceCategory.SuccessWithChildren;
      return PerformanceCategory.FailureWithException;
    }

    enum PerformanceCategory
    {
      Success,
      SuccessWithChildren,
      SuccessWithResult,
      FailureWithException
    }
  }
}
