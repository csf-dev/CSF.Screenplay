using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ActorTests
{
    #region Abilities

    [Test,AutoMoqData]
    public void IsAbleToShouldThrowIfTheAbilityIsNull(Actor sut)
    {
        Assert.That(() => sut.IsAbleTo(null), Throws.ArgumentNullException);
    }

    [Test,AutoMoqData]
    public void IsAbleToShouldNotThrowForAStringAbility(Actor sut, string aString)
    {
        Assert.That(() => sut.IsAbleTo(aString), Throws.Nothing);
    }

    [Test,AutoMoqData]
    public void IsAbleToShouldAddAnAbilityToTheActor(Actor sut, string aString)
    {
        sut.IsAbleTo(aString);
        Assert.That(sut.GetAbility<string>(), Is.SameAs(aString));
    }

    [Test,AutoMoqData]
    public void IsAbleToShouldThrowIfTheSameAbilityTypeIsAddedTwice(Actor sut, string aString)
    {
        sut.IsAbleTo(aString);
        Assert.That(() => sut.IsAbleTo(aString), Throws.InvalidOperationException);
    }

    [Test,AutoMoqData]
    public void IsAbleToShouldInvokeGainedAbilityEvent(Actor sut, string aString)
    {
        object? eventAbility = null;
        void OnGainedAbility(object? sender, GainAbilityEventArgs args)
        {
            eventAbility = args.Ability;
        }

        sut.GainedAbility += OnGainedAbility;
        sut.IsAbleTo(aString);
        sut.GainedAbility -= OnGainedAbility;

        Assert.That(eventAbility, Is.SameAs(aString));
    }

    #endregion

    #region Disposable

    [Test,AutoMoqData]
    public void DisposeShouldDisposeDisposableAbilities(Mock<DisposableAbility1> ability1, Mock<DisposableAbility2> ability2, Actor sut, string stringAbility)
    {
        sut.IsAbleTo(ability1.Object);
        sut.IsAbleTo(ability2.Object);
        sut.IsAbleTo(stringAbility);

        sut.Dispose();

        Assert.Multiple(() =>
        {
            ability1.Verify(x => x.Dispose(), Times.Once, "Ability 1 has been disposed");
            ability2.Verify(x => x.Dispose(), Times.Once, "Ability 2 has been disposed");
        });
    }

    [Test,AutoMoqData]
    public void DisposeShouldNotDisposeTwiceIfCalledTwice(Mock<DisposableAbility1> ability1, Actor sut)
    {
        sut.IsAbleTo(ability1.Object);

        sut.Dispose();
        sut.Dispose();

        ability1.Verify(x => x.Dispose(), Times.Once);
    }

    [Test,AutoMoqData]
    public void IsAbleToShouldThrowIfCalledAfterActorIsDisposed(Actor sut, string stringAbility)
    {
        sut.Dispose();
        Assert.That(() => sut.IsAbleTo(stringAbility), Throws.InstanceOf<ObjectDisposedException>());
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithoutResultShouldThrowIfCalledAfterActorIsDisposed(Actor sut, IPerformable performable)
    {
        sut.Dispose();
        Assert.That(() => ((ICanPerform) sut).PerformAsync(performable), Throws.InstanceOf<ObjectDisposedException>());
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithNongenericResultShouldThrowIfCalledAfterActorIsDisposed(Actor sut, IPerformableWithResult performable)
    {
        sut.Dispose();
        Assert.That(() => ((ICanPerform) sut).PerformAsync(performable), Throws.InstanceOf<ObjectDisposedException>());
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithGenericResultShouldThrowIfCalledAfterActorIsDisposed(Actor sut, IPerformableWithResult<string> performable)
    {
        sut.Dispose();
        Assert.That(() => ((ICanPerform) sut).PerformAsync(performable), Throws.InstanceOf<ObjectDisposedException>());
    }

    public sealed class DisposableAbility1 : IDisposable
    {
        public virtual void Dispose() {}
    }

    public sealed class DisposableAbility2 : IDisposable
    {
        public virtual void Dispose() {}
    }

    #endregion

    #region Performables

    [Test,AutoMoqData]
    public async Task PerformAsyncWithoutResultShouldExecuteThePerformable(Actor sut, IPerformable performable)
    {
        await ((ICanPerform) sut).PerformAsync(performable);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task PerformAsyncWithoutResultShouldRaiseTwoEvents(Actor sut, IPerformable performable)
    {
        bool beginPerformable = false, endPerformable = false;
        void OnBeginPerformable(object? sender, EventArgs ev) => beginPerformable = true;
        void OnEndPerformable(object? sender, EventArgs ev) => endPerformable = true;
        sut.BeginPerformable += OnBeginPerformable;
        sut.EndPerformable += OnEndPerformable;

        await ((ICanPerform) sut).PerformAsync(performable);

        sut.BeginPerformable -= OnBeginPerformable;
        sut.EndPerformable -= OnEndPerformable;

        Assert.Multiple(() =>
        {
            Assert.That(beginPerformable, Is.True, "BeginPerformable was triggered");
            Assert.That(endPerformable, Is.True, "EndPerformable was triggered");
        });
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithoutResultShouldRaisePerformableFailedEventIfPerformableThrows(Actor sut, IPerformable performable)
    {
        Exception? exceptionCaught = null;
        void OnPerformableFailed(object? sender, PerformableFailureEventArgs ev) => exceptionCaught = ev.Exception;
        sut.PerformableFailed += OnPerformableFailed;

        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Throws<InvalidOperationException>();

        Assert.Multiple(() =>
        {
            Assert.That(async () => await ((ICanPerform)sut).PerformAsync(performable), Throws.InstanceOf<PerformableException>(), "PerformAsync throws an exception");
            sut.PerformableFailed -= OnPerformableFailed;
            Assert.That(exceptionCaught, Is.InstanceOf<InvalidOperationException>(), "The correct exception was caught");
        });
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithoutResultShouldRethrowTheSameExceptionIfItIsPerformableException(Actor sut, IPerformable performable)
    {
        var exception = new PerformableException();
        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Throws(exception);

        Assert.That(async () => await ((ICanPerform)sut).PerformAsync(performable), Throws.Exception.SameAs(exception));
    }

    [Test,AutoMoqData]
    public async Task PerformAsyncWithNongenericResultShouldExecuteThePerformable(Actor sut, IPerformableWithResult performable)
    {
        await ((ICanPerform) sut).PerformAsync(performable);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task PerformAsyncWithNongenericResultShouldRaiseThreeEvents(Actor sut, IPerformableWithResult performable, object expectedResult)
    {
        bool beginPerformable = false, endPerformable = false;
        object? result = null;
        void OnBeginPerformable(object? sender, EventArgs ev) => beginPerformable = true;
        void OnPerformableResult(object? sender, PerformableResultEventArgs ev) => result = ev.Result;
        void OnEndPerformable(object? sender, EventArgs ev) => endPerformable = true;
        sut.BeginPerformable += OnBeginPerformable;
        sut.PerformableResult += OnPerformableResult;
        sut.EndPerformable += OnEndPerformable;
#pragma warning disable CA2012 // Use ValueTasks correctly
        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Returns(ValueTask.FromResult(expectedResult));
#pragma warning restore CA2012 // Use ValueTasks correctly

        await ((ICanPerform) sut).PerformAsync(performable);

        sut.BeginPerformable -= OnBeginPerformable;
        sut.PerformableResult -= OnPerformableResult;
        sut.EndPerformable -= OnEndPerformable;

        Assert.Multiple(() =>
        {
            Assert.That(beginPerformable, Is.True, "BeginPerformable was triggered");
            Assert.That(result, Is.SameAs(expectedResult), "PerformableResult was triggered");
            Assert.That(endPerformable, Is.True, "EndPerformable was triggered");
        });
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithNongenericResultShouldRaisePerformableFailedEventIfPerformableThrows(Actor sut, IPerformableWithResult performable)
    {
        Exception? exceptionCaught = null;
        void OnPerformableFailed(object? sender, PerformableFailureEventArgs ev) => exceptionCaught = ev.Exception;
        sut.PerformableFailed += OnPerformableFailed;

        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Throws<InvalidOperationException>();

        Assert.Multiple(() =>
        {
            Assert.That(async () => await ((ICanPerform)sut).PerformAsync(performable), Throws.InstanceOf<PerformableException>(), "PerformAsync throws an exception");
            sut.PerformableFailed -= OnPerformableFailed;
            Assert.That(exceptionCaught, Is.InstanceOf<InvalidOperationException>(), "The correct exception was caught");
        });
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithNongenericResultShouldRethrowTheSameExceptionIfItIsPerformableException(Actor sut, IPerformableWithResult performable)
    {
        var exception = new PerformableException();
        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Throws(exception);

        Assert.That(async () => await ((ICanPerform)sut).PerformAsync(performable), Throws.Exception.SameAs(exception));
    }

    [Test,AutoMoqData]
    public async Task PerformAsyncWithGenericResultShouldExecuteThePerformable(Actor sut, IPerformableWithResult<string> performable)
    {
        await ((ICanPerform) sut).PerformAsync(performable);
        Mock.Get(performable).Verify(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test,AutoMoqData]
    public async Task PerformAsyncWithGenericResultShouldRaiseThreeEvents(Actor sut, IPerformableWithResult<string> performable, string expectedResult)
    {
        bool beginPerformable = false, endPerformable = false;
        object? result = null;
        void OnBeginPerformable(object? sender, EventArgs ev) => beginPerformable = true;
        void OnPerformableResult(object? sender, PerformableResultEventArgs ev) => result = ev.Result;
        void OnEndPerformable(object? sender, EventArgs ev) => endPerformable = true;
        sut.BeginPerformable += OnBeginPerformable;
        sut.PerformableResult += OnPerformableResult;
        sut.EndPerformable += OnEndPerformable;
#pragma warning disable CA2012 // Use ValueTasks correctly
        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Returns(ValueTask.FromResult(expectedResult));
#pragma warning restore CA2012 // Use ValueTasks correctly

        await ((ICanPerform) sut).PerformAsync(performable);

        sut.BeginPerformable -= OnBeginPerformable;
        sut.PerformableResult -= OnPerformableResult;
        sut.EndPerformable -= OnEndPerformable;

        Assert.Multiple(() =>
        {
            Assert.That(beginPerformable, Is.True, "BeginPerformable was triggered");
            Assert.That(result, Is.SameAs(expectedResult), "PerformableResult was triggered");
            Assert.That(endPerformable, Is.True, "EndPerformable was triggered");
        });
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithGenericResultShouldRaisePerformableFailedEventIfPerformableThrows(Actor sut, IPerformableWithResult<string> performable)
    {
        Exception? exceptionCaught = null;
        void OnPerformableFailed(object? sender, PerformableFailureEventArgs ev) => exceptionCaught = ev.Exception;
        sut.PerformableFailed += OnPerformableFailed;

        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Throws<InvalidOperationException>();

        Assert.Multiple(() =>
        {
            Assert.That(async () => await ((ICanPerform)sut).PerformAsync(performable), Throws.InstanceOf<PerformableException>(), "PerformAsync throws an exception");
            sut.PerformableFailed -= OnPerformableFailed;
            Assert.That(exceptionCaught, Is.InstanceOf<InvalidOperationException>(), "The correct exception was caught");
        });
    }

    [Test,AutoMoqData]
    public void PerformAsyncWithGenericResultShouldRethrowTheSameExceptionIfItIsPerformableException(Actor sut, IPerformableWithResult<string> performable)
    {
        var exception = new PerformableException();
        Mock.Get(performable).Setup(x => x.PerformAsAsync(sut, It.IsAny<CancellationToken>())).Throws(exception);

        Assert.That(async () => await ((ICanPerform)sut).PerformAsync(performable), Throws.Exception.SameAs(exception));
    }

    #endregion
}