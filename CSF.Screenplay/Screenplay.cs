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
    /// The Screenplay object is used to create instances of <see cref="Performance"/> via the <see cref="PerformanceFactory"/>.
    /// You may wish to read a <xref href="HowScreenplayAndPerformanceRelateArticle?text=diagram+showing+how+screenplays,+performances,+actors+and+performables+relate+to+one+another" />.
    /// </para>
    /// </remarks>
    /// <seealso cref="Performance"/>
    public sealed class Screenplay : IHasServiceProvider
    {
        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay has begun.</summary>
        public void BeginScreenplay() => ServiceProvider.GetRequiredService<IRelaysPerformanceEvents>().InvokeScreenplayStarted();

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay is now complete.</summary>
        public void CompleteScreenplay() => ServiceProvider.GetRequiredService<IRelaysPerformanceEvents>().InvokeScreenplayEnded();

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
        /// Note that if the <paramref name="performanceLogic"/> raises a <see cref="PerformableException"/> then this method will 'swallow'
        /// that exception and not rethrow.  That's not particularly bad though because:
        /// </para>
        /// <list type="bullet">
        /// <item><description>
        /// An event will be raised with the event bus: <see cref="IHasPerformanceEvents"/>, specifically
        /// <see cref="IHasPerformanceEvents.PerformableFailed"/>.
        /// This will contain details of the exception which occurred.
        /// </description></item>
        /// <item><description>The performance will be immediately terminated and placed into the <see cref="PerformanceState.Failed"/> state.</description></item>
        /// </list>
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

            using(var scope = ServiceProvider.CreateScope())
            using(var performance = scope.ServiceProvider.GetRequiredService<IPerformance>())
            {
                performance.NamingHierarchy.Clear();
                performance.NamingHierarchy.AddRange(namingHierarchy);
                performance.BeginPerformance();

                try
                {
                    var result = await performanceLogic(scope.ServiceProvider, cancellationToken).ConfigureAwait(false);
                    performance.FinishPerformance(result);
                }
                // Only catch performable exceptions; if it's anything else then the root-level logic of the performance logic is at fault,
                // and that's unrelated to Screenplay, so shouldn't be caught here.
                catch(PerformableException)
                {
                    performance.FinishPerformance(false);
                }
            }
        }

        /// <summary>Initialises a new instance of <see cref="Screenplay"/></summary>
        /// <param name="services">An optional dependency injection service collection</param>
        public Screenplay(IServiceCollection services = default)
        {
            ServiceProvider = DependencyInjectionSetup.SetupDependencyInjection(services, this);
        }
    }
}