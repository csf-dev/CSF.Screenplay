using System;
using NUnit.Framework;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Tests.Abilities
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class AbilityStoreTests
  {
    [Test]
    public void Add_ability_by_type_successfully_adds_an_ability()
    {
      // Arrange
      var sut = GetSut();

      // Act
      sut.Add(typeof(SampleAbility));

      // Assert
      Assert.That(() => sut.HasAbility<SampleAbility>());
    }

    [Test]
    public void Add_ability_by_type_which_is_not_an_ability_raises_an_exception()
    {
      // Arrange
      var sut = GetSut();

      // Act & assert
      Assert.That(() => sut.Add(typeof(NotAnAbility)), Throws.ArgumentException);
    }

    [Test]
    public void Add_ability_by_instance_successfully_adds_an_ability()
    {
      // Arrange
      var sut = GetSut();

      // Act
      sut.Add(new SampleAbility());

      // Assert
      Assert.That(() => sut.HasAbility<SampleAbility>());
    }

    [Test]
    public void GetAbility_can_get_ability_added_by_type()
    {
      // Arrange
      var sut = GetSut();
      sut.Add(typeof(SampleAbility));

      // Act
      var result = sut.GetAbility<SampleAbility>();

      // Assert
      Assert.That(result, Is.Not.Null, "Result is not null");
      Assert.That(result, Is.InstanceOf<SampleAbility>(), "Result is an instance of sample ability");
    }

    [Test]
    public void GetAbility_can_get_ability_added_by_instance()
    {
      // Arrange
      var sut = GetSut();
      var ability = new SampleAbility();
      sut.Add(ability);

      // Act
      var result = sut.GetAbility<SampleAbility>();

      // Assert
      Assert.That(result, Is.SameAs(ability));
    }

    [Test]
    public void GetAbility_returns_null_for_an_ability_which_was_not_added()
    {
      // Arrange
      var sut = GetSut();

      // Act
      var result = sut.GetAbility<SampleAbility>();

      // Assert
      Assert.That(result, Is.Null);
    }

    IAbilityStore GetSut() => new AbilityStore();

    class SampleAbility : Ability {}

    class NotAnAbility {}
  }
}
