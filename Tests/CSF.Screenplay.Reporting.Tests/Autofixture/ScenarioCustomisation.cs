using System;
using CSF.Screenplay.ReportModel;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class ScenarioCustomisation : ICustomization
  {
    static readonly Random randomiser;

    public static Random Randomiser => randomiser;

    public void Customize(IFixture fixture)
    {
      new NamedCustomisation().Customize(fixture);
      new AbilityCustomisation().Customize(fixture);
      new PerformableCustomisation().Customize(fixture);
      new ReportableCustomisation().Customize(fixture);

      fixture.Customize<Scenario>(builder => {
        return builder
          .FromFactory<string,string>(CreateScenario)
          .Do(s => {
            ConfigureRandomOutcome(s);
            AddSomeReportables(s, fixture);
          });
      });
    }

    Scenario CreateScenario(string id, string name)
      => new Scenario { Name = new IdAndName { Id = $"ScenarioId {id}", Name = $"Scenario {name}" } };

    void ConfigureRandomOutcome(Scenario scenario)
    {
      var random = Randomiser.Next(1, 4);

      switch(random)
      {
      case 1:
        scenario.Outcome = ScenarioOutcome.Success;
        break;

      case 2:
        scenario.Outcome = ScenarioOutcome.Failure;
        break;

      default:
        scenario.Outcome = ScenarioOutcome.Inconclusive;
        break;
      }
    }

    void AddSomeReportables(Scenario scenario, IFixture fixture)
    {
      int
        howManyReportables = Randomiser.Next(1, 10);

      var reportables = fixture.CreateMany<Reportable>(howManyReportables);

      foreach(var reportable in reportables)
        scenario.Reportables.Add(reportable);
    }

    static ScenarioCustomisation()
    {
      randomiser = new Random();
    }
  }
}
