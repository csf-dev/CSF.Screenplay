using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CSF.Screenplay;

public class AutofixtureServicesCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<IServiceProvider>(c => c.FromFactory(new ServiceProviderBuilder()));
    }

    class ServiceProviderBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(It.IsAny<Type>()))
                .Returns((Type t) => context.Resolve(t));
            serviceProvider.As<ISupportRequiredService>()
                .Setup(x => x.GetRequiredService(It.IsAny<Type>()))
                .Returns((Type t) => context.Resolve(t));
            return serviceProvider.Object;
        }
    }
}
