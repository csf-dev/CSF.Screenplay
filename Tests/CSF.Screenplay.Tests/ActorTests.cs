using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
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
      var performer = CreatePerformer();
      var action = Mock.Of<VoidPerformable>();

      // Act
      performer.Perform(action);

      // Assert
      Mock.Get(action).Verify(x => x.PerformAs(performer), Times.Once());
    }

    [Test]
    public void Perform_executes_non_void_action()
    {
      // Arrange
      var performer = CreatePerformer();
      var action = Mock.Of<NonVoidPerformable>();

      // Act
      performer.Perform(action);

      // Assert
      Mock.Get(action).Verify(x => x.PerformAs(performer), Times.Once());
    }

    [Test]
    public void HasAbility_returns_true_when_the_ability_is_possessed()
    {
      // Arrange
      var ability = Mock.Of<SampleAbility>();
      var performer = CreatePerformer(ability);

      // Act
      var result = performer.HasAbility<SampleAbility>();

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void SupportsAction_returns_false_when_the_ability_is_not_possessed()
    {
      // Arrange
      var performer = CreatePerformer();

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
      var performer = CreatePerformer(ability);

      // Act
      var result = performer.GetAbility<SampleAbility>();

      // Assert
      Assert.AreSame(ability, result);
    }

    [Test]
    public void GetAbility_throws_exception_when_the_ability_is_not_possessed()
    {
      // Arrange
      var performer = CreatePerformer();

      // Act & assert
      Assert.Throws<MissingAbilityException>(() => performer.GetAbility<SampleAbility>());
    }

    IPerformer CreatePerformer(IAbility ability,
                               string name = null)
    {
      return CreatePerformer(new [] { ability }, name);
    }

    IPerformer CreatePerformer(IEnumerable<IAbility> abilities = null,
                               string name = null)
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

    public class VoidPerformable : Performable
    {
      public override void PerformAs(IPerformer actor)
      {
        throw new NotSupportedException();
      }
    }

    public class NonVoidPerformable : Performable<string>
    {
      public override string PerformAs(IPerformer actor)
      {
        throw new NotSupportedException();
      }
    }

    public class SampleAbility : Ability {}
  }
}
