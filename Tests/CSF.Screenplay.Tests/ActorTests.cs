using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting;
using Moq;
using NUnit.Framework;

namespace CSF.Screenplay.Tests
{
  [TestFixture]
  public class ActorTests
  {
    [Test]
    public void Perform_executes_void_action()
    {
      // Arrange
      var performer = CreateActor();
      var action = Mock.Of<IPerformable>();

      // Act
      performer.Perform(action);

      // Assert
      Mock.Get(action).Verify(x => x.PerformAs(performer), Times.Once());
    }

    [Test]
    public void Perform_executes_non_void_action()
    {
      // Arrange
      var performer = CreateActor();
      var action = Mock.Of<IPerformable<string>>();

      // Act
      performer.Perform(action);

      // Assert
      Mock.Get(action).Verify(x => x.PerformAs(performer), Times.Once());
    }

    [Test]
    public void Perform_void_action_reports_beginning()
    {
      // Arrange
      var triggered = false;
      var performer = CreateActor();
      performer.BeginPerformance += (sender, e) => triggered = true;
      var action = Mock.Of<IPerformable>();

      // Act
      performer.Perform(action);

      // Assert
      Assert.IsTrue(triggered);
    }

    [Test]
    public void Perform_void_action_reports_success_when_action_is_ok()
    {
      // Arrange
      var triggered = false;
      var performer = CreateActor();
      performer.EndPerformance += (sender, e) => triggered = true;
      var action = Mock.Of<IPerformable>();

      // Act
      performer.Perform(action);

      // Assert
      Assert.IsTrue(triggered);
    }

    [Test]
    public void Perform_void_action_reports_failure_when_action_raises_exception()
    {
      // Arrange
      var triggered = false;
      var performer = CreateActor();
      performer.PerformanceFailed += (sender, e) => triggered = true;
      var action = Mock.Of<IPerformable>();
      Mock.Get(action)
          .Setup(x => x.PerformAs(performer))
          .Throws<InvalidOperationException>();

      // Act
      try
      {
        performer.Perform(action);
      }
      catch(InvalidOperationException) { }


      // Assert
      Assert.IsTrue(triggered);
    }

    [Test]
    public void Perform_non_void_action_reports_beginning()
    {
      // Arrange
      var triggered = false;
      var performer = CreateActor();
      performer.BeginPerformance += (sender, e) => triggered = true;
      var action = Mock.Of<IPerformable<string>>();

      // Act
      performer.Perform(action);

      // Assert
      Assert.IsTrue(triggered);
    }

    [Test]
    public void Perform_non_void_action_reports_success_when_action_is_ok()
    {
      // Arrange
      var triggered = false;
      var performer = CreateActor();
      performer.EndPerformance += (sender, e) => triggered = true;
      var action = Mock.Of<IPerformable<string>>();

      // Act
      performer.Perform(action);

      // Assert
      Assert.IsTrue(triggered);
    }

    [Test]
    public void Perform_non_void_action_reports_result_when_action_is_ok()
    {
      // Arrange
      var triggered = false;
      object result = null;
      var performer = CreateActor();
      performer.PerformanceResult += (sender, e) => {
        triggered = true;
        result = e.Result;
      };
      var action = Mock.Of<IPerformable<string>>(x => x.PerformAs(performer) == "foo");

      // Act
      performer.Perform(action);

      // Assert
      Assert.IsTrue(triggered);
      Assert.AreEqual("foo", result);
    }

    [Test]
    public void Perform_non_void_action_reports_failure_when_action_raises_exception()
    {
      // Arrange
      var triggered = false;
      var performer = CreateActor();
      performer.PerformanceFailed += (sender, e) => triggered = true;
      var action = Mock.Of<IPerformable<string>>();
      Mock.Get(action)
          .Setup(x => x.PerformAs(performer))
          .Throws<InvalidOperationException>();

      // Act
      try
      {
        performer.Perform(action);
      }
      catch(InvalidOperationException) { }


      // Assert
      Assert.IsTrue(triggered);
    }

    [Test]
    public void HasAbility_returns_true_when_the_ability_is_possessed()
    {
      // Arrange
      var ability = Mock.Of<SampleAbility>();
      var performer = CreateActor(ability);

      // Act
      var result = performer.HasAbility<SampleAbility>();

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void GainedAbility_reports_gaining_an_ability()
    {
      // Arrange
      var triggered = false;
      var performer = CreateActor();
      performer.GainedAbility += (sender, e) => triggered = true;

      // Act
      performer.IsAbleTo(Mock.Of<SampleAbility>());

      // Assert
      Assert.IsTrue(triggered);
    }

    [Test]
    public void SupportsAction_returns_false_when_the_ability_is_not_possessed()
    {
      // Arrange
      var performer = CreateActor();

      // Act
      var result = performer.HasAbility<SampleAbility>();

      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void GetAbility_returns_instance_when_the_ability_is_possessed()
    {
      // Arrange
      var ability = Mock.Of<SampleAbility>();
      var performer = CreateActor(ability);

      // Act
      var result = performer.GetAbility<SampleAbility>();

      // Assert
      Assert.AreSame(ability, result);
    }

    [Test]
    public void GetAbility_throws_exception_when_the_ability_is_not_possessed()
    {
      // Arrange
      var performer = CreateActor();

      // Act & assert
      Assert.Throws<MissingAbilityException>(() => performer.GetAbility<SampleAbility>());
    }

    IActor CreateActor(IAbility ability, string name = null)
    {
      return CreateActor(new [] { ability }, name);
    }

    IActor CreateActor(IEnumerable<IAbility> abilities = null, string name = null)
    {
      abilities = abilities?? Enumerable.Empty<IAbility>();
      name = name?? "joe";

      var performer = new Actor(name);
      foreach(var ability in abilities)
      {
        performer.IsAbleTo(ability);
      }
      return performer;
    }

    public class SampleAbility : Ability {}
  }
}
