using System;

namespace CSF.Screenplay
{
    /// <summary>An object which has an associated <see cref="IServiceProvider"/>, which resolves
    /// services from dependency injection.</summary>
    public interface IHasServiceProvider
    {
        /// <summary>Gets a service provider/resolver instance associated with this object.</summary>
        IServiceProvider ServiceProvider { get; }
    }
}