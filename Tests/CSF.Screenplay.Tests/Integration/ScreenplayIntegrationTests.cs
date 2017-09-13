using NUnit.Framework;
using System;
using CSF.Screenplay.Integration;
using Moq;
using CSF.Screenplay.Scenarios;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Tests.Integration
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class ScreenplayIntegrationTests
  {
    [Test]
    public void BeforeExecutingFirstScenario_executes_before_callbacks()
    {
      // Arrange
      var called = false;
      Action<IProvidesTestRunEvents,IServiceResolver> callback = (arg1, arg2) => called = true;

      var builder = GetBuilder();
      Mock.Get(builder)
          .SetupGet(x => x.BeforeFirstScenario)
          .Returns(new [] {callback});
      var sut = GetSut(builder: builder);

      // Act
      sut.BeforeExecutingFirstScenario();

      // Assert
      Assert.That(called, Is.True, "Callback should have been invoked");
    }

    [Test]
    public void BeforeExecutingFirstScenario_raises_begin_test_run_event()
    {
      var eventReceived = false;
      Action<IProvidesTestRunEvents,IServiceResolver> callback = (arg1, arg2) => {
        arg1.BeginTestRun += (sender, e) => eventReceived = true;
      };

      var builder = GetBuilder();
      Mock.Get(builder)
          .SetupGet(x => x.BeforeFirstScenario)
          .Returns(new [] {callback});
      var sut = GetSut(builder: builder);

      // Act
      sut.BeforeExecutingFirstScenario();

      // Assert
      Assert.That(eventReceived, Is.True, "Event should have been raised");
    }

    [Test]
    public void BeforeScenario_executes_before_callbacks()
    {
      // Arrange
      var called = false;
      Action<IScreenplayScenario> callback = (arg1) => called = true;

      var builder = GetBuilder();
      Mock.Get(builder)
          .SetupGet(x => x.BeforeScenario)
          .Returns(new [] {callback});
      var sut = GetSut(builder: builder);
      var scenario = GetScenario();

      // Act
      sut.BeforeScenario(scenario);

      // Assert
      Assert.That(called, Is.True, "Callback should have been invoked");
    }

    [Test]
    public void BeforeScenario_executes_begin_scenario_method()
    {
      // Arrange
      var sut = GetSut();
      var scenario = GetScenario();

      // Act
      sut.BeforeScenario(scenario);

      // Assert
      Mock.Get(scenario)
          .As<ICanBeginAndEndScenario>()
          .Verify(x => x.Begin(), Times.Once());
    }

    [Test]
    public void AfterScenario_executes_after_callbacks()
    {
      // Arrange
      var called = false;
      Action<IScreenplayScenario> callback = (arg1) => called = true;

      var builder = GetBuilder();
      Mock.Get(builder)
          .SetupGet(x => x.AfterScenario)
          .Returns(new [] {callback});
      var sut = GetSut(builder: builder);
      var scenario = GetScenario();

      // Act
      sut.AfterScenario(scenario, true);

      // Assert
      Assert.That(called, Is.True, "Callback should have been invoked");
    }

    [Test]
    public void AfterScenario_releases_scenario_services()
    {
      // Arrange
      var sut = GetSut();
      var scenario = GetScenario();

      // Act
      sut.AfterScenario(scenario, true);

      // Assert
      Mock.Get(scenario).Verify(x => x.ReleasePerScenarioServices(), Times.Once());
    }

    [Theory]
    public void AfterScenario_executes_end_scenario_method_with_success(bool success)
    {
      // Arrange
      var sut = GetSut();
      var scenario = GetScenario();

      // Act
      sut.AfterScenario(scenario, success);

      // Assert
      Mock.Get(scenario)
          .As<ICanBeginAndEndScenario>()
          .Verify(x => x.End(success), Times.Once());
    }

    [Test]
    public void AfterExecutedLastScenario_executes_after_callbacks()
    {
      // Arrange
      var called = false;
      Action<IServiceResolver> callback = (arg1) => called = true;

      var builder = GetBuilder();
      Mock.Get(builder)
          .SetupGet(x => x.AfterLastScenario)
          .Returns(new [] {callback});
      var sut = GetSut(builder: builder);

      // Act
      sut.AfterExecutedLastScenario();

      // Assert
      Assert.That(called, Is.True, "Callback should have been invoked");
    }

    [Test]
    public void AfterExecutedLastScenario_raises_end_test_run_event()
    {
      var eventReceived = false;
      Action<IProvidesTestRunEvents,IServiceResolver> callback = (arg1, arg2) => {
        arg1.CompleteTestRun += (sender, e) => eventReceived = true;
      };

      var builder = GetBuilder();
      Mock.Get(builder)
          .SetupGet(x => x.BeforeFirstScenario)
          .Returns(new [] {callback});
      var sut = GetSut(builder: builder);
      sut.BeforeExecutingFirstScenario();

      // Act
      sut.AfterExecutedLastScenario();

      // Assert
      Assert.That(eventReceived, Is.True, "Event should have been raised");
    }

    [Test]
    public void AfterExecutedLastScenario_releases_singleton_services()
    {
      // Arrange
      var resolver = Mock.Of<IServiceResolver>();
      var registry = GetRegistry(singletonResolver: resolver);
      var sut = GetSut(registry: registry);

      // Act
      sut.AfterExecutedLastScenario();

      // Assert
      Mock.Get(resolver).Verify(x => x.ReleaseLazySingletonServices(), Times.Once());
    }

    IScreenplayIntegration GetSut(IIntegrationConfigBuilder builder = null, IServiceRegistry registry = null)
    {
      builder = builder?? GetBuilder();
      registry = registry?? GetRegistry();

      return new ScreenplayIntegration(builder, new Lazy<IServiceRegistry>(() => registry));
    }

    IIntegrationConfigBuilder GetBuilder()
    {
      var builder = new Mock<IIntegrationConfigBuilder>();

      builder
        .SetupGet(x => x.BeforeFirstScenario)
        .Returns(Enumerable.Empty<Action<IProvidesTestRunEvents,IServiceResolver>>().ToArray());

      builder
        .SetupGet(x => x.BeforeScenario)
        .Returns(Enumerable.Empty<Action<IScreenplayScenario>>().ToArray());

      builder
        .SetupGet(x => x.AfterLastScenario)
        .Returns(Enumerable.Empty<Action<IServiceResolver>>().ToArray());

      builder
        .SetupGet(x => x.AfterScenario)
        .Returns(Enumerable.Empty<Action<IScreenplayScenario>>().ToArray());

      return builder.Object;
    }

    IServiceRegistry GetRegistry(IEnumerable<IServiceRegistration> registrations = null,
                                 IServiceResolver singletonResolver = null)
    {
      singletonResolver = singletonResolver?? Mock.Of<IServiceResolver>();

      var registry = new Mock<IServiceRegistry>();
      registry
        .Setup(x => x.GetSingletonResolver())
        .Returns(singletonResolver);

      var reg = registrations?? Enumerable.Empty<IServiceRegistration>();
      registry.SetupGet(x => x.Registrations).Returns(() => reg.ToArray());

      return registry.Object;
    }

    IScreenplayScenario GetScenario()
    {
      var scenario = new Mock<IScreenplayScenario>();
      scenario.As<ICanBeginAndEndScenario>();
      return scenario.Object;
    }
  }
}
