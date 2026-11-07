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
    public class ServiceProviderAdapter : IServiceProvider
    {
        readonly IObjectContainer wrapped;

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// The design of BoDi means that the <see cref="ObjectContainer.IsRegistered(Type, string)"/> method (and variants) are unsafe to use here.
        /// This means that we cannot reliably check whether a service type is already available in the container in a side-effect free manner.
        /// This is because when using <c>IsRegistered</c> or its other overloads, can return <see langword="false"/> for types which BoDi was capable
        /// of resolving, due to its <em>registration-free resolution</em> functionality.  That is - when resolving concrete types which are not registered, it
        /// will do its best to create an instance regardless.
        /// Thus, whilst <c>IsRegistered</c> is correctly named, it is not what we want here.  What we wanted is "Can you resolve type X?"
        /// </para>
        /// <para>
        /// The alternative, used here, is to attempt to resolve the type, but contained within a <c>try/catch</c> which catched BoDi's
        /// <see cref="ObjectContainerException"/>, which is thrown if BoDi cannot resolve the specified type. At that point, if it can't be resolved,
        /// then we use the <c>catch</c> to revert to the <see cref="IServiceProvider"/> behaviour, which is to return <see langword="null"/> for a service
        /// which cannot be resolved.
        /// </para>
        /// <para>
        /// <b>Beware when debugging</b> that this comes with an unfortunate trade-off!  If BoDi encounters an error whilst resolving something, then we can't tell at this
        /// point whether that was an expected or unexpected error.  An example of an expected error would be that we're trying to resolve an interface which
        /// is not registered.  An unexpected error might be an exception encountered within the constructor of something we are resolving.
        /// Because the contract of <see cref="IServiceProvider"/> is to return null when attempting to resolve things which are not registered, then we must not
        /// throw for those expected errors. The dilemma this offers us (because we can't tell expected and unexpected apart) is that we either have to throw for
        /// every problem, or never throw.
        /// I have picked the "never throw" route, but the trade-off is that this logic masks unexpected errors.  That will make debugging more difficult, because unexpected
        /// exceptions from BoDi will be squashed and replaced with null returns. What I have done to make that less painful is to integrate with MS Logging Abstractions here.
        /// If you install and register a logging abstraction within DI, then an implementation of <see cref="ILogger{T}"/> will be resolved and the original error
        /// recorded in the log.
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
                    logger.LogError(ex, "An error occured within BoDi's DI resolution. As we cannot differentiate between expected and unexpected errors, the service provider adapter logs it and returns null.");
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