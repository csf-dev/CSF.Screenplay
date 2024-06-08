using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>An actor which has an associated <see cref="IServiceProvider"/></summary>
    public interface IHasServiceProvider
    {
        /// <summary>Gets a service resolver instance associated with this actor</summary>
        IServiceProvider ServiceProvider { get; }
    }
}