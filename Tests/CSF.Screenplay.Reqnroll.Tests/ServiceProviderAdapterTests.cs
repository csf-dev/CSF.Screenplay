using System;
using Microsoft.Extensions.Logging;
using Moq;
using Reqnroll.BoDi;

namespace CSF.Screenplay
{
    [TestFixture, Parallelizable]
    public class ServiceProviderAdapterTests
    {
        [Test]
        public void GetServiceShouldResolveFromTheWrappedContainer()
        {
            var expected = new object();
            var wrapped = new Mock<IObjectContainer>();
            wrapped.Setup(x => x.Resolve(typeof(object), null)).Returns(expected);
            var sut = new ServiceProviderAdapter(wrapped.Object);

            Assert.That(sut.GetService(typeof(object)), Is.SameAs(expected));
        }

        [Test]
        public void GetServiceShouldReturnNullIfWrappedContainerThrowsObjectContainerException()
        {
            var wrapped = new Mock<IObjectContainer>();
            var logger = new Mock<ILogger<ServiceProviderAdapter>>();
            wrapped.Setup(x => x.Resolve(typeof(object), null)).Throws(new ObjectContainerException("Cannot resolve", Array.Empty<Type>()));
            wrapped.Setup(x => x.Resolve<ILogger<ServiceProviderAdapter>>()).Returns(logger.Object);
            var sut = new ServiceProviderAdapter(wrapped.Object);

            using var scrope = Assert.EnterMultipleScope();
            Assert.That(() => sut.GetService(typeof(object)), Throws.Nothing, "Does not throw");
            Assert.That(sut.GetService(typeof(object)), Is.Null, "Returns null");
        }

        [Test]
        public void GetServiceShouldGetALoggerIfRegisteredAndTheWrappedContainerThrowsObjectContainerException()
        {
            var wrapped = new Mock<IObjectContainer>();
            var logger = new Mock<ILogger<ServiceProviderAdapter>>();
            wrapped.Setup(x => x.Resolve(typeof(object), null)).Throws(new ObjectContainerException("Cannot resolve", Array.Empty<Type>()));
            wrapped.Setup(x => x.Resolve<ILogger<ServiceProviderAdapter>>()).Returns(logger.Object);
            var sut = new ServiceProviderAdapter(wrapped.Object);

            sut.GetService(typeof(object));

            wrapped.Verify(x => x.Resolve<ILogger<ServiceProviderAdapter>>(), Times.Once);
        }

        [Test]
        public void GetServiceShouldNotThrowIfGettingALoggerThrowsObjectContainerException()
        {
            var wrapped = new Mock<IObjectContainer>();
            wrapped.Setup(x => x.Resolve(typeof(object), null)).Throws(new ObjectContainerException("Cannot resolve", Array.Empty<Type>()));
            wrapped.Setup(x => x.Resolve<ILogger<ServiceProviderAdapter>>()).Throws(new ObjectContainerException("Cannot resolve", Array.Empty<Type>()));
            var sut = new ServiceProviderAdapter(wrapped.Object);

            Assert.That(() => sut.GetService(typeof(object)), Throws.Nothing);
        }

        [Test]
        public void GetServiceShouldNotCatchAnyOtherKindOfException()
        {
            var wrapped = new Mock<IObjectContainer>();
            wrapped.Setup(x => x.Resolve(typeof(object), null)).Throws(new InvalidOperationException());
            var sut = new ServiceProviderAdapter(wrapped.Object);

            Assert.That(() => sut.GetService(typeof(object)), Throws.InvalidOperationException);
        }
    }
}