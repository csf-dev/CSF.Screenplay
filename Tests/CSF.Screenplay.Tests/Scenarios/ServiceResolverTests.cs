using System;
using NUnit.Framework;
using CSF.Screenplay.Actors;
using Moq;
using CSF.Screenplay.Scenarios;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Tests.Scenarios
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class ServiceResolverTests
  {
    [Test]
    public void GetService_returns_instance_when_service_is_registered()
    {
      // Arrange
      var sut = GetSutWithSampleRegistration();

      // Act
      var result = sut.GetService<ISampleService>();

      // Assert
      Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GetService_throws_exception_when_service_is_not_registered()
    {
      // Arrange
      var sut = GetSut();

      // Act & assert
      Assert.That(() => sut.GetService<ISampleService>(), Throws.TypeOf<ServiceNotRegisteredException>());
    }

    [Test]
    public void GetOptionalService_returns_instance_when_service_is_registered()
    {
      // Arrange
      var sut = GetSutWithSampleRegistration();

      // Act
      var result = sut.GetOptionalService<ISampleService>();

      // Assert
      Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GetOptionalService_returns_null_when_service_is_not_registered()
    {
      // Arrange
      var sut = GetSut();

      // Act
      var result = sut.GetOptionalService<ISampleService>();

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void ReleasePerScenarioServices_disposes_disposable_service_which_is_initialised()
    {
      // Arrange
      var service = Mock.Of<DisposableService>();
      var sut = GetSutWithDisposableRegistration(service);

      // Trigger the initialisation
      sut.GetService<ISampleService>();

      // Act
      sut.ReleasePerScenarioServices();

      // Assert
      Mock.Get(service).Verify(x => x.Dispose(), Times.Once());
    }

    [Test]
    public void ReleasePerScenarioServices_does_not_dispose_disposable_service_which_is_not_initialised()
    {
      // Arrange
      var service = Mock.Of<DisposableService>();
      var sut = GetSutWithDisposableRegistration(service);

      // Act
      sut.ReleasePerScenarioServices();

      // Assert
      Mock.Get(service).Verify(x => x.Dispose(), Times.Never());
    }

    [Test]
    public void ReleasePerScenarioServices_does_not_dispose_singleton_services()
    {
      // Arrange
      var service = Mock.Of<DisposableService>();
      var sut = GetSutWithDisposableLazySingletonRegistration(service);

      // Trigger the initialisation
      sut.GetService<ISampleService>();

      // Act
      sut.ReleasePerScenarioServices();

      // Assert
      Mock.Get(service).Verify(x => x.Dispose(), Times.Never());
    }

    [Test]
    public void ReleaseLazySingletonServices_disposes_lazy_singleton_services()
    {
      // Arrange
      var service = Mock.Of<DisposableService>();
      var sut = GetSutWithDisposableLazySingletonRegistration(service);

      // Trigger the initialisation
      sut.GetService<ISampleService>();

      // Act
      sut.ReleaseLazySingletonServices();

      // Assert
      Mock.Get(service).Verify(x => x.Dispose(), Times.Once());
    }

    [Test]
    public void ReleaseLazySingletonServices_does_not_dispose_non_lazy_singleton_services()
    {
      // Arrange
      var service = Mock.Of<DisposableService>();
      var sut = GetSutWithDisposableNonLazySingletonRegistration(service);

      // Trigger the initialisation
      sut.GetService<ISampleService>();

      // Act
      sut.ReleaseLazySingletonServices();

      // Assert
      Mock.Get(service).Verify(x => x.Dispose(), Times.Never());
    }

    public interface ISampleService {}

    public class SampleService : ISampleService {}

    public class DisposableService : ISampleService, IDisposable
    {
      public virtual void Dispose() {}
    }

    IServiceResolver GetSutWithSampleRegistration()
    {
      var metadata = new ServiceMetadata(typeof(ISampleService), null, ServiceLifetime.Singleton);
      var registration = new SingletonRegistration(metadata, new SampleService());

      var registrations = new IServiceRegistration[] { registration };
      return GetSut(registrations);
    }

    IServiceResolver GetSutWithDisposableRegistration(DisposableService service)
    {
      var metadata = new ServiceMetadata(typeof(ISampleService), null, ServiceLifetime.PerScenario);
      var registration = new PerScenarioRegistration(metadata, s => service);

      var registrations = new IServiceRegistration[] { registration };
      return GetSut(registrations);
    }

    IServiceResolver GetSutWithDisposableLazySingletonRegistration(DisposableService service)
    {
      var metadata = new ServiceMetadata(typeof(ISampleService), null, ServiceLifetime.Singleton);
      var registration = new LazySingletonRegistration(metadata, () => service);

      var registrations = new IServiceRegistration[] { registration };
      return GetSut(registrations);
    }

    IServiceResolver GetSutWithDisposableNonLazySingletonRegistration(DisposableService service)
    {
      var metadata = new ServiceMetadata(typeof(ISampleService), null, ServiceLifetime.Singleton, isResolverOwned: false);
      var registration = new SingletonRegistration(metadata, service);

      var registrations = new IServiceRegistration[] { registration };
      return GetSut(registrations);
    }

    IServiceResolver GetSut(IReadOnlyCollection<IServiceRegistration> registrations)
      => new ServiceResolver(registrations);

    IServiceResolver GetSut()
    => new ServiceResolver(Enumerable.Empty<IServiceRegistration>().ToArray());
  }
}
