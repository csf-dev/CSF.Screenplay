using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actions;
using CSF.Screenplay.Actors;
using Moq;
using NUnit.Framework;

namespace CSF.Screenplay.Tests.Actors
{
  [TestFixture]
  public class PerformerTests
  {
    [Test]
    public void Perform_executes_void_action()
    {
      // Arrange
      var performer = CreatePerformer();
      var parameters = "params";
      var action = Mock.Of<VoidAction>();

      // Act
      performer.Perform(action, parameters);

      // Assert
      Mock.Get(action).Verify(x => x.Execute(performer, parameters), Times.Once());
    }

    [Test]
    public void Perform_executes_non_void_action()
    {
      // Arrange
      var performer = CreatePerformer();
      var parameters = "params";
      var action = Mock.Of<NonVoidAction>();

      // Act
      performer.Perform(action, parameters);

      // Assert
      Mock.Get(action).Verify(x => x.Execute(performer, parameters), Times.Once());
    }

    [Test]
    public void SupportsActionType_returns_true_when_an_ability_supports_the_type()
    {
      // Arrange
      var ability = Mock.Of<IAbility>(x => x.CanProvideAction<VoidAction>() == true);
      var performer = CreatePerformer(ability);

      // Act
      var result = performer.SupportsActionType<VoidAction>();

      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void SupportsActionType_returns_false_when_no_ability_supports_the_type()
    {
      // Arrange
      var ability = Mock.Of<IAbility>(x => x.CanProvideAction<VoidAction>() == false);
      var performer = CreatePerformer(ability);

      // Act
      var result = performer.SupportsActionType<VoidAction>();

      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void GetAction_returns_instance_when_an_ability_supports_the_type()
    {
      // Arrange
      var returnedAction = new VoidAction();
      var ability = Mock.Of<IAbility>(x => x.CanProvideAction<VoidAction>() == true
                                           && x.GetAction<VoidAction>() == returnedAction);
      var performer = CreatePerformer(ability);

      // Act
      var result = performer.GetAction<VoidAction>();

      // Assert
      Assert.AreSame(returnedAction, result);
    }

    [Test]
    public void GetAction_returns_null_when_no_ability_supports_the_type()
    {
      // Arrange
      var ability = Mock.Of<IAbility>(x => x.CanProvideAction<VoidAction>() == false);
      var performer = CreatePerformer(ability);

      // Act
      var result = performer.GetAction<VoidAction>();

      // Assert
      Assert.Null(result);
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

      return new Performer(abilities, name);
    }

    public class VoidAction : IAction<string>
    {
      public virtual void Execute(IPerformer performer, string parameters)
      {
        throw new NotSupportedException();
      }
    }

    public class NonVoidAction : IAction<string,string>
    {
      public virtual string Execute(IPerformer performer, string parameters)
      {
        throw new NotSupportedException();
      }

      object IActionWithResult<string>.Execute(IPerformer performer, string parameters)
      {
        return Execute(performer, parameters);
      }
    }
  }
}
