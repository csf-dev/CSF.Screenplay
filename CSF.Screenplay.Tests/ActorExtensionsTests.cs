namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ActorExtensionsTests
{
    [Test,AutoMoqData]
    public void HasAbilityGenericShouldReturnTrueIfTheActorHasAnAbilityOfTheMatchingType(Actor sut, string stringAbility)
    {
        sut.IsAbleTo(stringAbility);

        Assert.That(((ICanPerform)sut).HasAbility<string>(), Is.True);
    }

    [Test,AutoMoqData]
    public void HasAbilityGenericShouldReturnFalseIfTheActorDoesNotHaveAnAbilityOfTheMatchingType(Actor sut)
    {
        Assert.That(((ICanPerform)sut).HasAbility<string>(), Is.False);
    }

    [Test,AutoMoqData]
    public void GetGenericShouldThrowIfTheActorIsOfTheWrongInterface(ICanPerform sut)
    {
        Assert.That(() => sut.GetAbility<string>(), Throws.ArgumentException);
    }

    [Test,AutoMoqData]
    public void GetGenericShouldThrowIfTheActorDoesNotHaveAnAbilityOfTheMatchingType(Actor sut)
    {
        Assert.That(() => sut.GetAbility<string>(), Throws.InvalidOperationException);
    }

    [Test,AutoMoqData]
    public void GetGenericShouldReturnTheAbilityIfTheActorHasIt(Actor sut, string stringAbility)
    {
        sut.IsAbleTo(stringAbility);

        Assert.That(() => sut.GetAbility<string>(), Is.SameAs(stringAbility));
    }
}
