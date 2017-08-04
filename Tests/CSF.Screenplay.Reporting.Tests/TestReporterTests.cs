using System;
using NUnit.Framework;
using CSF.Screenplay.Reporting;
using System.Text;
using System.IO;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using Moq;

namespace CSF.Screenplay.Reporting.Tests
{
  [TestFixture]
  public class TestReporterTests
  {
    StringBuilder sb;
    IReporter sut;
    TextWriter writer;

    [SetUp]
    public void Setup()
    {
      sb = new StringBuilder();
      writer = new StringWriter(sb);
      sut = new TextReporter(writer);
    }

    string GetReport()
    {
      sut.CompleteTestRun();
      writer.Flush();
      writer.Dispose();
      return sb.ToString();
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_one_performance(IActor actor,
                                                       IPerformable performable,
                                                       string id,
                                                       string name,
                                                       string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
";

      sut.Subscribe(actor);
      sut.BeginNewTestRun();
      sut.BeginNewScenario(id, name, feature);

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      // Act
      Mock.Get(actor).Raise(x => x.BeginGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndGiven += null, new ActorEventArgs(actor));
      sut.CompleteScenario(true);

      // Assert
      var result = GetReport();
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_scenario_failure(IActor actor,
                                                        IPerformable performable,
                                                        string id,
                                                        string name,
                                                        string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Failure ****
Given Joe does a thing
";

      sut.Subscribe(actor);
      sut.BeginNewTestRun();
      sut.BeginNewScenario(id, name, feature);

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      // Act
      Mock.Get(actor).Raise(x => x.BeginGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndGiven += null, new ActorEventArgs(actor));
      sut.CompleteScenario(false);

      // Assert
      var result = GetReport();
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Feature_name_is_omitted_if_not_provided(IActor actor,
                                                        IPerformable performable,
                                                        string id,
                                                        string name)
    {
      // Arrange
      var expected = $@"
Scenario: {name}
**** Success ****
Given Joe does a thing
";

      sut.Subscribe(actor);
      sut.BeginNewTestRun();
      sut.BeginNewScenario(id, name, null);

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      // Act
      Mock.Get(actor).Raise(x => x.BeginGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndGiven += null, new ActorEventArgs(actor));
      sut.CompleteScenario(true);

      // Assert
      var result = GetReport();
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Scenario_id_is_used_if_name_if_omitted(IActor actor,
                                                       IPerformable performable,
                                                       string id,
                                                       string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {id}
**** Success ****
Given Joe does a thing
";

      sut.Subscribe(actor);
      sut.BeginNewTestRun();
      sut.BeginNewScenario(id, null, feature);

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      // Act
      Mock.Get(actor).Raise(x => x.BeginGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndGiven += null, new ActorEventArgs(actor));
      sut.CompleteScenario(true);

      // Assert
      var result = GetReport();
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_two_nested_performances(IActor actor,
                                                               IPerformable performable,
                                                               IPerformable childPerformable,
                                                               string id,
                                                               string name,
                                                               string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
          Joe does a different thing
";

      sut.Subscribe(actor);
      sut.BeginNewTestRun();
      sut.BeginNewScenario(id, name, feature);

      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns("Joe does a thing");
      Mock.Get(childPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a different thing");

      // Act
      Mock.Get(actor).Raise(x => x.BeginGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, childPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, childPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndGiven += null, new ActorEventArgs(actor));
      sut.CompleteScenario(true);

      // Assert
      var result = GetReport();
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_three_nested_performances(IActor actor,
                                                                 IPerformable performable,
                                                                 IPerformable childPerformable,
                                                                 IPerformable grandChildPerformable,
                                                                 string id,
                                                                 string name,
                                                                 string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
          Joe does a different thing
              Joe does a totally different thing
";

      sut.Subscribe(actor);
      sut.BeginNewTestRun();
      sut.BeginNewScenario(id, name, feature);

      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns("Joe does a thing");
      Mock.Get(childPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a different thing");
      Mock.Get(grandChildPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a totally different thing");

      // Act
      Mock.Get(actor).Raise(x => x.BeginGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, childPerformable));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, grandChildPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, grandChildPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, childPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndGiven += null, new ActorEventArgs(actor));
      sut.CompleteScenario(true);

      // Assert
      var result = GetReport();
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_complex_nested_performances(IActor actor,
                                                                   IPerformable performable,
                                                                   IPerformable childPerformable,
                                                                   IPerformable grandChildPerformable,
                                                                   IPerformable siblingPerformable,
                                                                   IPerformable secondPerformable,
                                                                   string id,
                                                                   string name,
                                                                   string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
          Joe does a different thing
              Joe does a totally different thing
          Joe does an unrelated thing
 When Joe takes some kind of action
";

      sut.Subscribe(actor);
      sut.BeginNewTestRun();
      sut.BeginNewScenario(id, name, feature);

      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns("Joe does a thing");
      Mock.Get(childPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a different thing");
      Mock.Get(grandChildPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a totally different thing");
      Mock.Get(siblingPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does an unrelated thing");
      Mock.Get(secondPerformable).Setup(x => x.GetReport(actor)).Returns("Joe takes some kind of action");

      // Act
      Mock.Get(actor).Raise(x => x.BeginGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, childPerformable));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, grandChildPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, grandChildPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, childPerformable));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, siblingPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, siblingPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, performable));
      Mock.Get(actor).Raise(x => x.EndGiven += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginWhen += null, new ActorEventArgs(actor));
      Mock.Get(actor).Raise(x => x.BeginPerformance += null, new BeginPerformanceEventArgs(actor, secondPerformable));
      Mock.Get(actor).Raise(x => x.EndPerformance += null, new EndSuccessfulPerformanceEventArgs(actor, secondPerformable));
      Mock.Get(actor).Raise(x => x.EndWhen += null, new ActorEventArgs(actor));
      sut.CompleteScenario(true);

      // Assert
      var result = GetReport();
      Assert.That(result, Is.EqualTo(expected));
    }
  }
}
