namespace CSF.Screenplay.Actors;

[TestFixture, Parallelizable]
public class StageTests
{
    [Test, AutoMoqData]
    public void GetSpotlitActorShouldReturnNullWhenNoActorIsSpotlit(Stage sut)
    {
        Assert.That(() => sut.GetSpotlitActor(), Is.Null);
    }

    [Test, AutoMoqData]
    public void GetSpotlitActorShouldReturnAnActorAfterTheyAreSpotlit(Stage sut, Actor actor)
    {
        sut.Spotlight(actor);
        Assert.That(() => sut.GetSpotlitActor(), Is.SameAs(actor));
    }

    [Test, AutoMoqData]
    public void SpotlightForAnActorShouldReplaceTheActorInTheSpotlight(Stage sut, Actor actor1, Actor actor2)
    {
        sut.Spotlight(actor1);
        sut.Spotlight(actor2);
        Assert.That(() => sut.GetSpotlitActor(), Is.SameAs(actor2));
    }

    [Test, AutoMoqData]
    public void TurnSpotlightOffShouldRemoveAnActorFromTheSpotlight(Stage sut, Actor actor)
    {
        sut.Spotlight(actor);
        sut.TurnSpotlightOff();
        Assert.That(() => sut.GetSpotlitActor(), Is.Null);
    }

    [Test, AutoMoqData]
    public void TurnSpotlightOffShouldNotThrowIfNoActorIsSpotlit(Stage sut)
    {
        Assert.That(() => sut.TurnSpotlightOff(), Throws.Nothing);
    }

    [Test, AutoMoqData]
    public void SpotlightForAPersonaShouldSpotlightTheActorReturnedByThePersonaAndTheCast([Frozen] ICast cast,
                                                                                          Stage sut,
                                                                                          IPersona persona,
                                                                                          Actor actor)
    {
        Mock.Get(cast).Setup(x => x.GetActor(persona)).Returns(actor);
        sut.Spotlight(persona);
        Assert.That(() => sut.GetSpotlitActor(), Is.SameAs(actor));
    }
}