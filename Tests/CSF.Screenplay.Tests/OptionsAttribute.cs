using System.Linq;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.Extensions.Options;

namespace CSF.Screenplay;

public class OptionsAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
        if(!parameter.ParameterType.IsGenericType || parameter.ParameterType.GetGenericTypeDefinition() != typeof(IOptions<>))
            throw new ArgumentException($"The parameter type must be a generic type of {nameof(IOptions<object>)}<T>", nameof(parameter));
        
        var genericType = parameter.ParameterType.GetGenericArguments().Single();
        var customizationType = typeof(OptionsCustomization<>).MakeGenericType(genericType);
        return (ICustomization) Activator.CreateInstance(customizationType, parameter)!;
    }
}

public class OptionsCustomization<T>(ParameterInfo param) : ICustomization where T : class, new()
{
    public void Customize(IFixture fixture)
    {
        var builder = new FilteringSpecimenBuilder(new OptionsSpecimenBuilder<T>(),
                                                   new ParameterSpecification(param.ParameterType, param.Name));
        fixture.Customizations.Insert(0, builder);
    }
}

public class OptionsSpecimenBuilder<T> : ISpecimenBuilder where T : class, new()
{
    public object Create(object request, ISpecimenContext context)
    {
        if(request is not ParameterInfo paramRequest || paramRequest.ParameterType != typeof(IOptions<T>))
            return new NoSpecimen();

        var options = new T();
        return Mock.Of<IOptions<T>>(x => x.Value == options);
    }
}