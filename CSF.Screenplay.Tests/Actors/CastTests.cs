using AutoFixture.NUnit3;
using CSF.Screenplay.Performances;
using Moq;
using NUnit.Framework.Internal;

namespace CSF.Screenplay.Actors;

[TestFixture, Parallelizable]
public class CastTests
{
    [Test, AutoMoqData]
    public void GetActorByNameShouldReturnAnActorOfTheSpecifiedName(string name,
                                                                    Guid performanceIdentity,
                                                                    [Frozen] IRelaysPerformanceEvents eventBus,
                                                                    [AutofixtureServices] IServiceProvider services)
    {
        var sut = new Cast(services, performanceIdentity);
        var actor = sut.GetActor(name);
        Assert.That(((IHasName)actor)?.Name, Is.EqualTo(name));
    }

    [Test, AutoMoqData]
    public void GetActorByNameShouldReturnTheSameActorIfUsedTwice(string name,
                                                                  Guid performanceIdentity,
                                                                  [Frozen] IRelaysPerformanceEvents eventBus,
                                                                  [AutofixtureServices] IServiceProvider services)
    {
        var sut = new Cast(services, performanceIdentity);
        var actor1 = sut.GetActor(name);
        var actor2 = sut.GetActor(name);

        Assert.Multiple(() =>
        {
            Assert.That(actor1, Is.Not.Null, "Actors aren't null");
            Assert.That(actor1, Is.SameAs(actor2), "Same actor for second usage of the method");
        });
    }

    [Test, AutoMoqData]
    public void GetActorByNameShouldAddActorToEventBus(string name,
                                                       Guid performanceIdentity,
                                                       [Frozen] IRelaysPerformanceEvents eventBus,
                                                       [AutofixtureServices] IServiceProvider services)
    {
        var sut = new Cast(services, performanceIdentity);
        var actor = sut.GetActor(name);
        Mock.Get(eventBus).Verify(x => x.SubscribeTo(actor), Times.Once);
    }

    [Test, AutoMoqData]
    public void GetActorByNameShouldAddActorToEventBusOnlyOnceEvenIfCalledThreeTimes(string name,
                                                                                     Guid performanceIdentity,
                                                                                     [Frozen] IRelaysPerformanceEvents eventBus,
                                                                                     [AutofixtureServices] IServiceProvider services)
    {
        var sut = new Cast(services, performanceIdentity);
        var actor = sut.GetActor(name);
        sut.GetActor(name);
        sut.GetActor(name);
        Mock.Get(eventBus).Verify(x => x.SubscribeTo(It.IsAny<Actor>()), Times.Once);
    }

    [Test, AutoMoqData]
    public void GetActorByPersonaShouldExecuteThePersona(string name,
                                                         Guid performanceIdentity,
                                                         [Frozen] IRelaysPerformanceEvents eventBus,
                                                         IPersona persona,
                                                         [AutofixtureServices] IServiceProvider services)
    {
        var sut = new Cast(services, performanceIdentity);
        Mock.Get(persona).Setup(x => x.GetActor(It.IsAny<Guid>())).Returns((Guid pId) => new Actor(name, pId));
        Mock.Get(persona).SetupGet(x => x.Name).Returns(name);
        sut.GetActor(persona);
        Mock.Get(persona).Verify(x => x.GetActor(performanceIdentity), Times.Once);
    }

    [Test, AutoMoqData]
    public void GetActorByPersonaShouldExecuteThePersonaOnlyOnceEvenIfUsedThreeTimes(string name,
                                                                                     Guid performanceIdentity,
                                                                                     [Frozen] IRelaysPerformanceEvents eventBus,
                                                                                     IPersona persona,
                                                                                     [AutofixtureServices] IServiceProvider services)
    {
        var sut = new Cast(services, performanceIdentity);
        Mock.Get(persona).Setup(x => x.GetActor(It.IsAny<Guid>())).Returns((Guid pId) => new Actor(name, pId));
        Mock.Get(persona).SetupGet(x => x.Name).Returns(name);
        sut.GetActor(persona);
        sut.GetActor(persona);
        sut.GetActor(persona);
        Mock.Get(persona).Verify(x => x.GetActor(It.IsAny<Guid>()), Times.Once);
    }
}