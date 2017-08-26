using System;
using NUnit.Framework;
using CSF.Screenplay.Actors;
using Moq;
using CSF.Screenplay.Scenarios;

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
      var joe = cast.Get("joe");

      // Assert
      Assert.That(joe, Is.Not.Null);
    }

    [Test]
    public void Get_returns_same_actor_if_called_twice()
    {
      // Arrange
      var cast = GetSut();

      // Act
      var joe = cast.Get("joe");
      var joeAgain = cast.Get("joe");

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
      var joeAgain = cast.Get("joe");

      // Assert
      Assert.That(joeAgain, Is.SameAs(joe));
    }

    [Test]
    public void Get_applies_customisation_if_creating_a_new_actor()
    {
      // Arrange
      var cast = GetSut();
      var customisationCallCount = 0;

      // Act
      cast.Get("joe", a => customisationCallCount++);

      // Assert
      Assert.That(customisationCallCount, Is.EqualTo(1));
    }

    [Test]
    public void Get_does_not_apply_customisation_if_actor_already_exists()
    {
      // Arrange
      var cast = GetSut();
      cast.Add("joe");
      var customisationCallCount = 0;

      // Act
      cast.Get("joe", a => customisationCallCount++);

      // Assert
      Assert.That(customisationCallCount, Is.EqualTo(0));
    }

    [Test]
    public void Get_applies_scenario_customisation_if_creating_a_new_actor()
    {
      // Arrange
      var cast = GetSut();
      var customisationCallCount = 0;

      // Act
      cast.Get("joe", (a, s) => customisationCallCount++, Mock.Of<IScreenplayScenario>());

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
      cast.Get("joe", (a, s) => customisationCallCount++, Mock.Of<IScreenplayScenario>());

      // Assert
      Assert.That(customisationCallCount, Is.EqualTo(0));
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
      var joe = new Actor(name);

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
      var joe = new Actor(name);

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
      var joe = new Actor("joe");
      var davina = new Actor("davina");

      // Act
      cast.Add(joe);
      cast.Add(davina);

      // Act and assert
      Assert.Pass();
    }

    ICast GetSut() => new Cast();
  }
}
