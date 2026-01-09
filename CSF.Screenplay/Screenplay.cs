using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;
using AsyncPerformanceLogic = System.Func<System.IServiceProvider, System.Threading.CancellationToken, System.Threading.Tasks.Task<bool?>>;

namespace CSF.Screenplay
{
    /// <summary>An object which represents a complete execution of Screenplay logic, which should include one or
    /// more <see cref="Performance"/> instances.</summary>
    /// <remarks>
    /// <para>
    /// A Screenplay, when used as a noun (an instance of this class), refers to a complete execution of the Screenplay
    /// software.
    /// A Screenplay is composed of at least one <see cref="Performance"/> and typically contains many performances.
    /// </para>
    /// <para>
    /// When the Screenplay architecture is applied to automated testing, an instance of this class corresponds to a
    /// complete test run, where each test corresponds to a performance.
    /// End-user logic, such as test logic, rarely interacts directly with this class.
    /// That is because the Screenplay object is generally consumed only by <xref href="IntegrationGlossaryItem?text=integration+logic"/>.
    /// </para>
    /// <para>
    /// It is recommended to create instances of this type by adding Screenplay to a dependency injection
    /// <see cref="IServiceCollection"/> via the extension method <see cref="ScreenplayServiceCollectionExtensions.AddScreenplay(IServiceCollection, Action{ScreenplayOptions})"/>
    /// and then resolving an instance of this class from the service provider.
    /// Alternatively, if you do not wish to configure a service collection manually and just want an instance of this type then
    /// use the static <see cref="Create(Action{IServiceCollection}, Action{ScreenplayOptions})"/> method.
    /// </para>
    /// <para>
    /// The Screenplay object is used to create instances of <see cref="Performance"/> via its <see cref="ServiceProvider"/>.
    /// You may wish to read a <xref href="HowScreenplayAndPerformanceRelateArticle?text=diagram+showing+how+screenplays,+performances,+actors+and+performables+relate+to+one+another" />.
    /// </para>
    /// </remarks>
    /// <seealso cref="Performance"/>
    /// <seealso cref="ScreenplayServiceCollectionExtensions"/>
    public sealed class Screenplay : IHasServiceProvider
    {
        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay has begun.</summary>
        public void BeginScreenplay()
        {
            var config = ServiceProvider.GetRequiredService<ScreenplayOptions>();
            foreach (var callback in config.OnBeginScreenplayActions)
                callback(ServiceProvider);

            ServiceProvider.GetRequiredService<IRelaysPerformanceEvents>().InvokeScreenplayStarted();
        }

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay is now complete.</summary>
        public void CompleteScreenplay()
        {
            ServiceProvider.GetRequiredService<IRelaysPerformanceEvents>().InvokeScreenplayEnded();

            var config = ServiceProvider.GetRequiredService<ScreenplayOptions>();
            foreach (var callback in config.OnEndScreenplayActions)
                callback(ServiceProvider);
        }

        /// <summary>
        /// Executes the specified logic as a <see cref="Performance"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is the primary entry point for beginning a Screenplay <see cref="Performance"/>.
        /// This method begins a new Dependency Injection Scope, and within that scope starts the performance, which executes the specified
        /// performance logic: <paramref name="performanceLogic"/>.
        /// The return value from the performance logic should conform to the semantics of the parameter value passed to
        /// <see cref="Performance.FinishPerformance(bool?)"/>.
        /// </para>
        /// <para>
        /// The <paramref name="namingHierarchy"/> may be used to give the performance a name, so that its results (and subsequent report)
        /// may be identified. This parameter has the same semantics as <see cref="Performance.NamingHierarchy"/>.
        /// </para>
        /// <para>
        /// Note that if the <paramref name="performanceLogic"/> raises a <see cref="PerformableException"/> then this method will catch
        /// that exception and not rethrow.  In that case:
        /// </para>
        /// <list type="bullet">
        /// <item><description>
        /// An event will be raised with the event bus: <see cref="IHasPerformanceEvents"/>, specifically <see cref="IHasPerformanceEvents.PerformableFailed"/>.
        /// This will contain details of the exception which occurred; subscribers to be informed that the performance has failed.
        /// </description></item>
        /// <item><description>The performance will be immediately terminated and placed into the <see cref="PerformanceState.Failed"/> state.</description></item>
        /// </list>
        /// <para>
        /// Any other exception, which does not derive from <see cref="PerformableException"/>, will not be caught by this method and will propagate outward.
        /// These exceptions will be interpreted as an error within the Screenplay architecture, since the <see cref="Actor"/> class will always catch and rethrow
        /// any exception encountered from any overload of <c>PerformAsync</c>, wrapped as the inner exception of a <see cref="PerformableException"/>.
        /// </para>
        /// </remarks>
        /// <param name="performanceLogic">The logic to be executed by the performance.</param>
        /// <param name="namingHierarchy">An optional naming hierarchy used to identify the performance.</param>
        /// <param name="cancellationToken">An optional cancellation token to abort the performance logic.</param>
        /// <returns>A task which completes when the performance's logic has completed.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="performanceLogic"/> is <see langword="null" />.</exception>
        public async Task ExecuteAsPerformanceAsync(AsyncPerformanceLogic performanceLogic,
                                                    IList<IdentifierAndName> namingHierarchy = default,
                                                    CancellationToken cancellationToken = default)
        {
            if (performanceLogic is null)
                throw new ArgumentNullException(nameof(performanceLogic));
            namingHierarchy = namingHierarchy ?? new List<IdentifierAndName>();

            using(var scopeAndPerformance = this.CreateScopedPerformance(namingHierarchy))
            {
                scopeAndPerformance.Performance.BeginPerformance();

                try
                {
                    var result = await performanceLogic(scopeAndPerformance.Performance.ServiceProvider, cancellationToken).ConfigureAwait(false);
                    scopeAndPerformance.Performance.FinishPerformance(result);
                }
                // Only catch performable exceptions; if it's anything else then the root-level logic of the performance logic is at fault,
                // and that's unrelated to Screenplay, so shouldn't be caught here.
                catch(PerformableException)
                {
                    scopeAndPerformance.Performance.FinishPerformance(false);
                }
            }
        }

        /// <summary>
        /// Initialises a new instance of <see cref="Screenplay"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is unlikely that developers should be executing this constructor directly. Consider using the static factory method
        /// <see cref="Create(Action{IServiceCollection}, Action{ScreenplayOptions})"/>.  Alternatively, add Screenplay to an <see cref="IServiceCollection"/> using
        /// <see cref="ScreenplayServiceCollectionExtensions.AddScreenplay(IServiceCollection, Action{ScreenplayOptions})"/> and then resolve an instance of
        /// this class from the service provider built from that service collection.
        /// </para>
        /// </remarks>
        /// <param name="serviceProvider">A service provider</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" />.</exception>
        public Screenplay(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Creates and returns a <see cref="Screenplay"/>, optionally including some dependency injection service customisations.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method to create an instance of <see cref="Screenplay"/> when you are not already using an <see cref="IServiceCollection"/>.
        /// This method creates a new service collection instance, adds Screenplay to it and then creates &amp; returns the Screenplay object
        /// instance.
        /// </para>
        /// <para>
        /// If you already have an <see cref="IServiceCollection"/> and you wish to integrate Screenplay into it, then use the extension method
        /// <see cref="ScreenplayServiceCollectionExtensions.AddScreenplay(IServiceCollection, Action{ScreenplayOptions})"/> instead.
        /// </para>
        /// </remarks>
        /// <param name="serviceCollectionCustomisations">An optional action which permits further customization of the service collection that is implicitly created by this method.</param>
        /// <param name="options">An optional action to configure the <see cref="Screenplay"/> which is created by this method.</param>
        /// <returns>A Screenplay instance created from a new service collection.</returns>
        public static Screenplay Create(Action<IServiceCollection> serviceCollectionCustomisations = null,
                                        Action<ScreenplayOptions> options = null)
        {
            var services = new ServiceCollection();
            services.AddScreenplay(options);
            serviceCollectionCustomisations?.Invoke(services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<Screenplay>();
        }
    }
}