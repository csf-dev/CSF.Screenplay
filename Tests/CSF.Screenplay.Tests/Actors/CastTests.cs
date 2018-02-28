using System;
using NUnit.Framework;
using CSF.Screenplay.Actors;
using Moq;
using CSF.Screenplay.Scenarios;
using CSF.FlexDi;

namespace CSF.Screenplay.Tests.Actors
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class CastTests
  {
    [Test]
    public void Add_using_name_sets_actors_name()
    {
      // Arrange
      var cast = GetSut();
      var name = "joe";

      // Act
      cast.Add(name);
      var joe = cast.GetExisting(name);

      // Assert
      Assert.AreEqual(name, joe.Name);
    }

    [Test]
    public void Add_using_guid_sets_scenario_identity()
    {
      // Arrange
      var identity = Guid.NewGuid();
      var cast = GetSut(identity);
      var name = "joe";

      // Act
      cast.Add(name);
      var joe = cast.GetExisting(name);

      // Assert
      Assert.AreEqual(identity, joe.ScenarioIdentity);
    }

    [Test]
    public void Add_using_name_raises_exception_if_used_twice_with_same_name()
    {
      // Arrange
      var cast = GetSut();
      var name = "joe";

      // Act
      cast.Add(name);

      // Act and assert
      Assert.Throws<DuplicateActorException>(() => cast.Add(name));
    }

    [Test]
    public void Add_using_name_does_not_raise_exception_if_used_twice_with_different_names()
    {
      // Arrange
      var cast = GetSut();

      // Act
      cast.Add("joe");
      cast.Add("davina");

      // Act and assert
      Assert.Pass();
    }

    [Test]
    public void GetExisting_returns_null_for_nonexistent_actors()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var joe = cast.GetExisting("joe");

      // Assert
      Assert.IsNull(joe);
    }

    [Test]
    public void GetExisting_returns_instance_for_created_actor()
    {
      // Arrange
      var cast = GetSut();
      var name = "joe";
      cast.Add(name);

      // Act
      var joe = cast.GetExisting(name);

      // Assert
      Assert.NotNull(joe);
    }

    [Test]
    public void Get_creates_new_actor_if_they_do_not_exist()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var joe = cast.Get("joe", (actor) => {});

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
      var joe = cast.Get("joe", (a) => {});

      // Assert
      Assert.That(joe.ScenarioIdentity, Is.EqualTo(identity));
    }

    [Test]
    public void Get_returns_same_actor_if_called_twice()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var joe = cast.Get("joe", (actor) => {});
      var joeAgain = cast.Get("joe", (actor) => {});

      // Assert
      Assert.That(joe, Is.SameAs(joeAgain));
    }

    [Test]
    public void Get_returns_existing_actor_if_they_already_exist()
    {
      // Arrange
      var cast = GetSut();
      cast.Add("joe");
      var joe = cast.GetExisting("joe");

      // Act
      var joeAgain = cast.Get("joe", (actor) => {});

      // Assert
      Assert.That(joeAgain, Is.SameAs(joe));
    }

    [Test]
    public void Get_applies_scenario_customisation_if_creating_a_new_actor()
    {
      // Arrange
      var cast = GetSut();
      var customisationCallCount = 0;

      // Act
      cast.Get("joe", (a) => customisationCallCount++);

      // Assert
      Assert.That(customisationCallCount, Is.EqualTo(1));
    }

    [Test]
    public void Get_does_not_apply_scenario_customisation_if_actor_already_exists()
    {
      // Arrange
      var cast = GetSut();
      cast.Add("joe");
      var customisationCallCount = 0;

      // Act
      cast.Get("joe", (a) => customisationCallCount++);

      // Assert
      Assert.That(customisationCallCount, Is.EqualTo(0));
    }

    [Test]
    public void Get_applies_scenario_customisation_with_resolver_if_creating_a_new_actor()
    {
      // Arrange
      var resolver = Mock.Of<IResolvesServices>();
      var cast = GetSut(resolver: resolver);

      // Act
      cast.Get("joe", (r,a) => r.Resolve<string>());

      // Assert
      Mock.Get(resolver).Verify(x => x.Resolve<string>(), Times.Once());
    }

    [Test]
    public void Get_does_not_apply_scenario_customisation_with_resolver_if_actor_already_exists()
    {
      // Arrange
      var resolver = Mock.Of<IResolvesServices>();
      var cast = GetSut(resolver: resolver);
      cast.Add("joe");

      // Act
      cast.Get("joe", (r,a) => r.Resolve<string>());

      // Assert
      Mock.Get(resolver).Verify(x => x.Resolve<string>(), Times.Never());
    }

    [Test]
    public void GetAll_returns_all_created_actors()
    {
      // Arrange
      var cast = GetSut();
      cast.Add("joe");
      cast.Add("davina");

      var joe = cast.GetExisting("joe");
      var davina = cast.GetExisting("davina");

      // Act
      var all = cast.GetAll();

      // Assert
      Assert.That(all, Is.EquivalentTo(new [] { joe, davina }));
    }

    [Test]
    public void Dismiss_removes_all_actors()
    {
      // Arrange
      var cast = GetSut();
      cast.Add("joe");
      cast.Add("davina");

      // Act
      cast.Dismiss();

      // Assert
      var all = cast.GetAll();
      Assert.That(all, Is.Empty);
    }

    [Test]
    public void Add_adds_the_actor()
    {
      // Arrange
      var cast = GetSut();
      var name = "joe";
      var joe = new Actor(name, Guid.NewGuid());

      // Act
      cast.Add(joe);
      var joeClone = cast.GetExisting(name);

      // Assert
      Assert.NotNull(joeClone);
      Assert.AreSame(joe, joeClone);
    }

    [Test]
    public void Add_raises_exception_if_used_twice_with_same_name()
    {
      // Arrange
      var cast = GetSut();
      var name = "joe";
      var joe = new Actor(name, Guid.NewGuid());

      // Act
      cast.Add(joe);

      // Act and assert
      Assert.Throws<DuplicateActorException>(() => cast.Add(joe));
    }

    [Test]
    public void Add_does_not_raise_exception_if_used_twice_with_different_names()
    {
      // Arrange
      var cast = GetSut();
      var joe = new Actor("joe", Guid.NewGuid());
      var davina = new Actor("davina", Guid.NewGuid());

      // Act
      cast.Add(joe);
      Assert.That(() => cast.Add(davina), Throws.Nothing);
    }

    ICast GetSut(Guid? scenarioGuid = null, IResolvesServices resolver = null)
      => new Cast(scenarioGuid.GetValueOrDefault(Guid.NewGuid()), resolver ?? Mock.Of<IResolvesServices>());
  }
}
