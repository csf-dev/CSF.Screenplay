using System;
namespace CSF.Screenplay.Scenarios
{
  public class PerScenarioRegistration : IServiceRegistration
  {
    public ServiceMetadata Metadata { get; private set; }

    public Func<IServiceResolver,object> ServiceFactory { get; private set; }

    public Lazy<object> GetService(IServiceResolver resolver)
    {
      return new Lazy<object>(() => ServiceFactory(resolver));
    }

    public PerScenarioRegistration(ServiceMetadata metadata, Func<IServiceResolver,object> factory)
    {
      if(metadata == null)
        throw new ArgumentNullException(nameof(metadata));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      Metadata = metadata;
      ServiceFactory = factory;
    }
  }
}
