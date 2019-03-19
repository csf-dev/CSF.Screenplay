using System;
using NUnit.Framework;
using CSF.Screenplay.Actors;
using Moq;
using CSF.FlexDi;

namespace CSF.Screenplay.Tests.Actors
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class CastTests
  {
    const string Joe = "Joe";

    [Test]
    public void Get_creates_new_actor_if_they_do_not_exist()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var joe = cast.Get(Joe);

      // Assert
      Assert.That(joe, Is.Not.Null);
    }

    [Test]
    public void Get_assigns_scenario_identity_to_created_actor()
    {
      // Arrange
      var identity = Guid.NewGuid();
      var cast = GetSut(identity);

      // Act
      var joe = cast.Get(Joe);

      // Assert
      Assert.That(joe.ScenarioIdentity, Is.EqualTo(identity));
    }

    [Test]
    public void Get_returns_same_actor_if_called_twice()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var joe = cast.Get(Joe);
      var joeAgain = cast.Get(Joe);

      // Assert
      Assert.That(joeAgain, Is.SameAs(joe));
    }

    [Test]
    public void Get_can_get_an_actor_from_a_persona_type()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var sarah = cast.Get<Sarah>();

      // Assert
      Assert.That(sarah, Is.Not.Null);
    }

    [Test]
    public void Get_can_get_an_actor_from_a_persona_instance()
    {
      // Arrange
      var cast = GetSut();
      var persona = new Sarah();

      // Act
      var sarah = cast.Get(persona);

      // Assert
      Assert.That(sarah, Is.Not.Null);
    }

    [Test]
    public void Get_returns_the_same_actor_if_used_twice_with_the_same_persona_type()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var sarah = cast.Get<Sarah>();
      var sarahAgain = cast.Get<Sarah>();

      // Assert
      Assert.That(sarahAgain, Is.SameAs(sarah));
    }

    [Test]
    public void Get_returns_the_same_actor_if_used_twice_with_the_same_persona_instance()
    {
      // Arrange
      var cast = GetSut();
      var persona = new Sarah();

      // Act
      var sarah = cast.Get(persona);
      var sarahAgain = cast.Get(persona);

      // Assert
      Assert.That(sarahAgain, Is.SameAs(sarah));
    }

    [Test]
    public void Get_does_not_re_apply_actor_customisation_if_using_the_same_persona_type_twice()
    {
      // Arrange
      Sarah.StaticGrantCount = 0;
      var cast = GetSut();

      // Act
      var sarah = cast.Get<Sarah>();
      var sarahAgain = cast.Get<Sarah>();

      // Assert
      Assert.That(Sarah.StaticGrantCount, Is.EqualTo(1));
    }

    [Test]
    public void Get_does_not_re_apply_actor_customisation_if_using_the_same_persona_instance_twice()
    {
      // Arrange
      var cast = GetSut();
      var persona = new Sarah();

      // Act
      var sarah = cast.Get(persona);
      var sarahAgain = cast.Get(persona);

      // Assert
      Assert.That(persona.GrantCount, Is.EqualTo(1));
    }

    ICast GetSut(Guid? scenarioGuid = null, IResolvesServices resolver = null)
      => new Cast(scenarioGuid.GetValueOrDefault(Guid.NewGuid()), resolver ?? Mock.Of<IResolvesServices>());

    public class Sarah : Persona
    {
      public static int StaticGrantCount { get; set; }

      public int GrantCount { get; private set; }

      public override string Name => "Sarah";

			public override void GrantAbilities(ICanReceiveAbilities actor, IResolvesServices resolver)
			{
        GrantCount ++;
        StaticGrantCount ++;
			}

      public Sarah()
      {
        GrantCount = 0;
      }
    }
  }
}
