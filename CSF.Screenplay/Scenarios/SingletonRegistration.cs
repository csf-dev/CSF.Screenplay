using System;
namespace CSF.Screenplay.Scenarios
{
  public class SingletonRegistration : IServiceRegistration
  {
    public ServiceMetadata Metadata { get; private set; }

    public object Service { get; private set; }

    public Lazy<object> GetService(IServiceResolver resolver)
    {
      return new Lazy<object>(() => Service);
    }

    public SingletonRegistration(ServiceMetadata metadata, object service)
    {
      if(metadata == null)
        throw new ArgumentNullException(nameof(metadata));

      Metadata = metadata;
      Service = service;
    }
  }
}
