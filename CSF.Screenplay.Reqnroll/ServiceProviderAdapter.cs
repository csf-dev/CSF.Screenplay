using System;
using Microsoft.Extensions.Logging;

#if SPECFLOW
using BoDi;
#else
using Reqnroll.BoDi;
#endif


namespace CSF.Screenplay
{
    /// <summary>
    /// Adapter class which allows a Reqnroll/BoDi <c>IObjectContainer</c> to be used as an <see cref="IServiceProvider"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This adapter class is limited and does not fully support the contract required by <see cref="IServiceProvider"/>, despite being a one-method interface.
    /// See the remarks on the <see cref="GetService(Type)"/> method for more information about this limitation.
    /// </para>
    /// <para>
    /// This is a <see href="https://en.wikipedia.org/wiki/Liskov_substitution_principle">Liskov Substitution Principle violation</see> and it is recognised that this adapter
    /// is of limited use. It is provided as a low-cost, low-effort workaround to making the BoDi DI container work alongside Microsoft Dependency Injection patterns.
    /// If you find this insufficient then <i>consider replacing Reqnroll's DI container</i>.
    /// <see href="https://docs.reqnroll.net/latest/integrations/autofac.html">An example of how to do this, using the Autofac container, is documented
    /// on Reqnroll's website.</see>
    /// </para>
    /// </remarks>
    public class ServiceProviderAdapter : IServiceProvider
    {
        readonly IObjectContainer wrapped;

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// This method includes two major departures from the contract which is expected from <see cref="IServiceProvider"/>.
        /// </para>
        /// <list type="bullet">
        /// <item><description>This method should return a non-null resolved instance of the type requested by <paramref name="serviceType"/>,
        /// if the service was registered with (added to) the container. On the other hand, if the service was not registered with (added to) the container then this
        /// method should return <see langword="null"/> without throwing an exception.</description></item>
        /// <item><description>This method should throw an exception if the service requested was registered with (added to) the container, but an exception
        /// was encountered whilst resolving it.</description></item>
        /// </list>
        /// <para>
        /// Unfortunately, this implementation does not quite behave that way.  The BoDi container includes <b>auto resolution</b> of types which have not been registered
        /// with (added to) the container.  That is - in fact - a desired &amp; intended feature for Reqnroll.  The most common expected kind of resolved object within
        /// a Reqnroll test is a simple model object which holds state for the duration of a scenario/feature.  These are simply constructed and do not require prior
        /// registration with the container.
        /// A side-effect of that functionality is that attempting to resolve an object which is not registered - if that object is not <see langword="abstract"/>
        /// (or an <see langword="interface"/>) - will result in an auto-resolution attempt.  Thus this method could return a non-null object regardless.
        /// </para>
        /// <para>
        /// Note to developers:  It has been considered to use <c>ObjectContainer.IsRegistered</c> here but unfortunately that does not tell us what we need to know.
        /// This tells us only if the type is registered within the current container instance, not whether it could be resolved successfully.
        /// Thus a process along the lines of "Is it registered ? If it is then resolve it, if not return null" would introduce false failures to resolve anything which
        /// is expected to be auto-resolved (the inverse problem).
        /// This means that this method <see cref="GetService(Type)"/> always goes directly to <see cref="IObjectContainer.Resolve(Type, string)"/>. The consequence being
        /// that it might cause an unwanted auto-resolution to occur where the developer would have wanted it to return null.
        /// </para>
        /// <para>
        /// The other limitation is the BoDi uses <b>the same exception type</b> for some very different scenarios.  <see cref="ObjectContainerException"/> is thrown if an attempt
        /// is made to resolve an abstract/interface type which is not registered.  It is thrown for circular dependencies and it is thrown when auto resolution fails
        /// because a class' constructors are incompatible with that technique.
        /// Notice that first scenario in which an attempt is made to resolve a service which is not registered.  This is the prime scenario in which a service provider should
        /// return null without throwing.  Thus, this method uses a <c>try/catch</c> to intercept <see cref="ObjectContainerException"/> and convert them to null returns.
        /// Unfortunately this means that <i>other scenarios in which an object container exception is thrown</i> will result in silently swallowing the exception and returning
        /// null.  This will make things like circular dependencies and auto-resolution failures difficult to diagnose, because they will not throw when the developer expects!
        /// </para>
        /// <para>
        /// Developers should <i>beware of this when debugging</i>!
        /// If you are able, add a logging package to DI which can provide an <see cref="ILogger{T}"/>.  If a logger is present in dependency injection and an
        /// <see cref="ObjectContainerException"/> is caught then the full caught exception will be recorded to the log at the warning level.
        /// This way, when attempting to diagnose unexpected behaviour, developers may at least read the log to find out what happened.
        /// </para>
        /// </remarks>
        public object GetService(Type serviceType)
        {
            try
            {
                return wrapped.Resolve(serviceType);
            }
            catch(ObjectContainerException ex)
            {
                if(TryGetLogger(out var logger))
                    logger.LogWarning(ex, "An error occured within BoDi's DI resolution. As we cannot differentiate between expected and unexpected errors, the service provider adapter logs it and returns null.");
                return null;
            }
        }

        bool TryGetLogger(out ILogger<ServiceProviderAdapter> logger)
        {
            try
            {
                logger = wrapped.Resolve<ILogger<ServiceProviderAdapter>>();
                return true;
            }
            catch(ObjectContainerException)
            {
                logger = null;
                return false;
            }
        }

        /// <summary>
        /// Initialises an instance of <see cref="ServiceProviderAdapter"/>.
        /// </summary>
        /// <param name="wrapped">The BoDi object container</param>
        /// <exception cref="ArgumentNullException">If <paramref name="wrapped"/> is <see langword="null" />.</exception>
        public ServiceProviderAdapter(IObjectContainer wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}