using System;
namespace CSF.Screenplay.Scenarios
{
  public interface IServiceRegistration
  {
    ServiceMetadata Metadata { get; }

    Lazy<object> GetService(IServiceResolver resolver);
  }
}
