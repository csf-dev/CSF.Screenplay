using CSF.Screenplay.Performables;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ActorExtensionsTests
{
    #region Abilities

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
    public void GetAbilityGenericShouldThrowIfTheActorIsOfTheWrongInterface(ICanPerform sut)
    {
        Assert.That(() => sut.GetAbility<string>(), Throws.ArgumentException);
    }

    [Test,AutoMoqData]
    public void GetAbilityGenericShouldThrowIfTheActorDoesNotHaveAnAbilityOfTheMatchingType(Actor sut)
    {
        Assert.That(() => sut.GetAbility<string>(), Throws.InvalidOperationException);
    }

    [Test,AutoMoqData]
    public void GetAbilityGenericShouldReturnTheAbilityIfTheActorHasIt(Actor sut, string stringAbility)
    {
        sut.IsAbleTo(stringAbility);

        Assert.That(() => sut.GetAbility<string>(), Is.SameAs(stringAbility));
    }

    #endregion

    #region Performable builders

    [Test,AutoMoqData]
    public async Task PerformAsyncShouldExecutePerformable(IPerformable performable, IGetsPerformable builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.PerformAsync(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task PerformAsyncWithResultShouldExecutePerformable(IPerformableWithResult performable, IGetsPerformableWithResult builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.PerformAsync(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task PerformAsyncWithGenericResultShouldExecutePerformable(IPerformableWithResult<object> performable, IGetsPerformableWithResult<object> builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.PerformAsync(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task WasAbleToShouldExecutePerformable(IPerformable performable, IGetsPerformable builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.WasAbleTo(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task WasAbleToWithResultShouldExecutePerformable(IPerformableWithResult performable, IGetsPerformableWithResult builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.WasAbleTo(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task WasAbleToWithGenericResultShouldExecutePerformable(IPerformableWithResult<object> performable, IGetsPerformableWithResult<object> builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.WasAbleTo(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task AttemptsToShouldExecutePerformable(IPerformable performable, IGetsPerformable builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.AttemptsTo(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task AttemptsToWithResultShouldExecutePerformable(IPerformableWithResult performable, IGetsPerformableWithResult builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.AttemptsTo(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task AttemptsToWithGenericResultShouldExecutePerformable(IPerformableWithResult<object> performable, IGetsPerformableWithResult<object> builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.AttemptsTo(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task ShouldShouldExecutePerformable(IPerformable performable, IGetsPerformable builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.Should(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task ShouldWithResultShouldExecutePerformable(IPerformableWithResult performable, IGetsPerformableWithResult builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.Should(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task ShouldWithGenericResultShouldExecutePerformable(IPerformableWithResult<object> performable, IGetsPerformableWithResult<object> builder, Actor actor)
    {
        Mock.Get(builder).Setup(x => x.GetPerformable()).Returns(performable);
        await actor.Should(builder);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(actor, default), Times.Once);
    }

    #endregion
}
