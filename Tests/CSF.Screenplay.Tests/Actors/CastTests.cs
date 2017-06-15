using System;
using NUnit.Framework;
using CSF.Screenplay.Actors;
using Moq;

namespace CSF.Screenplay.Tests.Actors
{
  [TestFixture]
  public class CastTests
  {
    [Test]
    public void Add_using_name_returns_new_actor()
    {
      // Arrange
      var cast = CreateCast();

      // Act
      var joe = cast.Add("joe");

      // Assert
      Assert.NotNull(joe);
    }

    [Test]
    public void Add_using_name_sets_actors_name()
    {
      // Arrange
      var cast = CreateCast();
      var name = "joe";

      // Act
      var joe = cast.Add(name);

      // Assert
      Assert.AreEqual(name, joe.Name);
    }

    [Test]
    public void Add_using_name_raises_exception_if_used_twice_with_same_name()
    {
      // Arrange
      var cast = CreateCast();
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
      var cast = CreateCast();

      // Act
      cast.Add("joe");
      cast.Add("davina");

      // Act and assert
      Assert.Pass();
    }

    [Test]
    public void GetActor_returns_null_for_nonexistent_actors()
    {
      // Arrange
      var cast = CreateCast();

      // Act
      var joe = cast.GetActor("joe");

      // Assert
      Assert.IsNull(joe);
    }

    [Test]
    public void GetActor_returns_instance_for_created_actor()
    {
      // Arrange
      var cast = CreateCast();
      var name = "joe";
      var joe = cast.Add(name);

      // Act
      var joeClone = cast.GetActor(name);

      // Assert
      Assert.AreSame(joe, joeClone);
    }

    [Test]
    public void GetAll_returns_all_created_actors()
    {
      // Arrange
      var cast = CreateCast();
      var joe = cast.Add("joe");
      var davina = cast.Add("davina");

      // Act
      var all = cast.GetAll();

      // Act and assert
      CollectionAssert.AreEquivalent(new [] { joe, davina }, all);
    }

    [Test]
    public void Add_adds_the_actor()
    {
      // Arrange
      var cast = CreateCast();
      var name = "joe";
      var joe = new Actor(name);

      // Act
      cast.Add(joe);
      var joeClone = cast.GetActor(name);

      // Assert
      Assert.NotNull(joeClone);
      Assert.AreSame(joe, joeClone);
    }

    [Test]
    public void Add_raises_exception_if_used_twice_with_same_name()
    {
      // Arrange
      var cast = CreateCast();
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
      var cast = CreateCast();
      var joe = new Actor("joe");
      var davina = new Actor("davina");

      // Act
      cast.Add(joe);
      cast.Add(davina);

      // Act and assert
      Assert.Pass();
    }

    [Test]
    public void Dispose_disposes_all_actors()
    {
      // Arrange
      var cast = CreateCast();
      var joe = Mock.Of<IActor>(x => x.Name == "joe");
      cast.Add(joe);
      var davina = Mock.Of<IActor>(x => x.Name == "davina");
      cast.Add(davina);

      // Act
      ((IDisposable) cast).Dispose();

      // Assert
      Mock.Get(joe).Verify(x => x.Dispose(), Times.Once());
      Mock.Get(davina).Verify(x => x.Dispose(), Times.Once());
    }

    Cast CreateCast()
    {
      return new Cast();
    }
  }
}
