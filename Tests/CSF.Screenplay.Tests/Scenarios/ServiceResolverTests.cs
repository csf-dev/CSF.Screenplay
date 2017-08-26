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
      var sut = GetSutWithOneRegistration();

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
      var sut = GetSutWithOneRegistration();

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

    interface ISampleService {}

    class SampleService : ISampleService {}

    IServiceResolver GetSutWithOneRegistration()
    {
      var metadata = new ServiceMetadata(typeof(ISampleService), null, ServiceLifetime.Singleton);
      var registration = new SingletonRegistration(metadata, new SampleService());

      var registrations = new IServiceRegistration[] { registration };
      return GetSut(registrations);
    }

    IServiceResolver GetSut(IReadOnlyCollection<IServiceRegistration> registrations)
      => new ServiceResolver(registrations);

    IServiceResolver GetSut()
    => new ServiceResolver(Enumerable.Empty<IServiceRegistration>().ToArray());
  }
}
