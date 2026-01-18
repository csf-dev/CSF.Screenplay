using System;
using System.Collections;
using Reqnroll.BoDi;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ServiceCollectionAdapterTests
{
    [Test]
    public void UnsupportedFunctionalityShouldThrowNotSupportedException()
    {
        var container = new ObjectContainer();
        var sut = new ServiceCollectionAdapter(container);

        Assert.Multiple(() =>
        {
            Assert.That(() => sut[0], Throws.InstanceOf<NotSupportedException>(), "Indexer get");
            Assert.That(() => sut[0] = null, Throws.InstanceOf<NotSupportedException>(), "Indexer set");
            Assert.That(() => sut.Clear(), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.Clear));
            Assert.That(() => sut.Contains(null), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.Contains));
            Assert.That(() => sut.CopyTo(null, default), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.CopyTo));
            Assert.That(() => sut.GetEnumerator(), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.GetEnumerator));
            Assert.That(() => ((IEnumerable) sut).GetEnumerator(), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.GetEnumerator) + ": " + nameof(IEnumerable));
            Assert.That(() => sut.IndexOf(default), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.IndexOf));
            Assert.That(() => sut.Insert(default, null), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.Insert));
            Assert.That(() => sut.Remove(null), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.Remove));
            Assert.That(() => sut.RemoveAt(default), Throws.InstanceOf<NotSupportedException>(), nameof(IServiceCollection.RemoveAt));
        });
    }

    [Test]
    public void AddTypeShouldAddToObjectContainer()
    {
        var container = new ObjectContainer();
        var sut = new ServiceCollectionAdapter(container);

        sut.AddSingleton<ISampleInterface, SampleType>();
        Assert.That(container.Resolve<ISampleInterface>, Is.InstanceOf<SampleType>());
    }

    [Test]
    public void AddInstanceShouldAddToObjectContainer()
    {
        var container = new ObjectContainer();
        var sut = new ServiceCollectionAdapter(container);

        var instance = new SampleType();
        sut.AddSingleton<ISampleInterface>(instance);
        Assert.That(container.Resolve<ISampleInterface>(), Is.SameAs(instance));
    }

    [Test]
    public void AddFactoryShouldAddToObjectContainer()
    {
        var container = new ObjectContainer();
        var sut = new ServiceCollectionAdapter(container);

        var instance = new SampleType();
        sut.AddSingleton<ISampleInterface>(_ => instance);
        Assert.That(container.Resolve<ISampleInterface>(), Is.SameAs(instance));
    }

    interface ISampleInterface {}

    class SampleType : ISampleInterface {}
}