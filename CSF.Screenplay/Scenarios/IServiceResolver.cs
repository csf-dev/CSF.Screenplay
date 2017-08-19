using System;
namespace CSF.Screenplay.Scenarios
{
  public interface IServiceResolver
  {
    TService GetService<TService>(string name = null) where TService : class;
    object GetService(ServiceMetadata metadata);

    TService GetOptionalService<TService>(string name = null) where TService : class;
    object GetOptionalService(ServiceMetadata metadata);
  }
}
