using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;
using AsyncPerformanceLogic = System.Func<System.IServiceProvider, System.Threading.CancellationToken, System.Threading.Tasks.Task<bool?>>;
using SyncPerformanceLogic = System.Func<System.IServiceProvider, bool?>;

namespace CSF.Screenplay
{
    /// <summary>
    /// Extension methods for the <see cref="Screenplay"/> type.
    /// </summary>
    public static class ScreenplayExtensions
    {
        // Three cached task instances to avoid allocating lots of tasks when converting async to sync.
        static readonly Task<bool?>
            trueTask = Task.FromResult<bool?>(true),
            falseTask = Task.FromResult<bool?>(false),
            nullTask = Task.FromResult<bool?>(null);

        /// <summary>
        /// Executes the specified logic as a <see cref="Performance"/>, synchronously.
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
        /// Use this method only if <see cref="Screenplay.ExecuteAsPerformanceAsync(AsyncPerformanceLogic, IList{IdentifierAndName}, CancellationToken)"/> is not viable.
        /// This method executes the logic asynchronously, as is the architecture of Screenplay, but then uses <see cref="Task.Wait(CancellationToken)"/> to convert
        /// the asynchronous result into a synchronous one.
        /// </para>
        /// <para>
        /// If <paramref name="timeoutMiliseconds"/> is not specified, or specified with a zero value then there will be no timeout
        /// applied to the performance's logic.  If specified with a positive integer then the performance logic will be aborted if
        /// the specified timeout (in milliseconds) is exceeded.
        /// Please be aware that - as with <see cref="Task.Wait(CancellationToken)"/> - if the timeout duration is exceded, the synchronous performance
        /// logic is not actually aborted.  The thread on which this method is executed will stop waiting for the thread on which the performance
        /// logic is running, but the performance logic thread will still continue, typically to completion.
        /// All this means is that when the performance logic eventually does complete, its results are discarded because the Screenplay 'gave up waiting'
        /// for it.
        /// </para>
        /// </remarks>
        /// <param name="screenplay">The screenplay with which to execute the logic.</param>
        /// <param name="performanceLogic">The logic to be executed by the performance.</param>
        /// <param name="namingHierarchy">An optional naming hierarchy used to identify the performance.</param>
        /// <param name="timeoutMiliseconds">If set to a non-zero positive value, then the performance logic will be aborted after the specified timeout in milliseconds.</param>
        /// <exception cref="ArgumentNullException">If either <paramref name="screenplay"/> or <paramref name="performanceLogic"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="timeoutMiliseconds"/> is a negative number.</exception>
        /// <seealso cref="ExecuteAsPerformance(Screenplay, SyncPerformanceLogic, IList{IdentifierAndName}, CancellationToken)"/>
        public static void ExecuteAsPerformance(this Screenplay screenplay,
                                                SyncPerformanceLogic performanceLogic,
                                                IList<IdentifierAndName> namingHierarchy = default,
                                                int timeoutMiliseconds = default)
        {
            var token = GetCancellationToken(timeoutMiliseconds);
            screenplay.ExecuteAsPerformance(performanceLogic, namingHierarchy, token);
        }

        /// <summary>
        /// Executes the specified logic as a <see cref="Performance"/>, synchronously.
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
        /// Use this method only if <see cref="Screenplay.ExecuteAsPerformanceAsync(AsyncPerformanceLogic, IList{IdentifierAndName}, CancellationToken)"/> is not viable.
        /// This method executes the logic asynchronously, as is the architecture of Screenplay, but then uses <see cref="Task.Wait(CancellationToken)"/> to convert
        /// the asynchronous result into a synchronous one.
        /// </para>
        /// <para>
        /// If <paramref name="cancellationToken"/> is not specified then no cancellation/abort logic will be applied.
        /// If specified, and the token is cancelled, then the Screenplay will abort waiting for the performance logic to complete.
        /// Please be aware that - as with <see cref="Task.Wait(CancellationToken)"/> - if the token is cancelled, the synchronous performance
        /// logic is not actually aborted.  The thread on which this method is executed will stop waiting for the thread on which the performance
        /// logic is running, but the performance logic thread will still continue, typically to completion.
        /// All this means is that when the performance logic eventually does complete, its results are discarded because the Screenplay 'gave up waiting'
        /// for it.
        /// </para>
        /// </remarks>
        /// <param name="screenplay">The screenplay with which to execute the logic.</param>
        /// <param name="performanceLogic">The logic to be executed by the performance.</param>
        /// <param name="namingHierarchy">A naming hierarchy used to identify the performance; if <see langword="null" /> then an empty name will be used.</param>
        /// <param name="cancellationToken">A cancellation token, which if cancelled will abort waiting for <paramref name="performanceLogic"/> to complete.</param>
        /// <exception cref="ArgumentNullException">If either <paramref name="screenplay"/> or <paramref name="performanceLogic"/> is <see langword="null" />.</exception>
        public static void ExecuteAsPerformance(this Screenplay screenplay,
                                                SyncPerformanceLogic performanceLogic,
                                                IList<IdentifierAndName> namingHierarchy,
                                                CancellationToken cancellationToken)
        {
            if (screenplay is null)
                throw new ArgumentNullException(nameof(screenplay));
            if (performanceLogic is null)
                throw new ArgumentNullException(nameof(performanceLogic));

            screenplay.ExecuteAsPerformanceAsync(GetAsyncPerformanceLogic(performanceLogic),
                                                 namingHierarchy,
                                                 cancellationToken)
                .Wait(cancellationToken);
        }

        /// <summary>
        /// Executes the logic with a specified imlpementation of <see cref="IHostsPerformance"/> as a <see cref="Performance"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is a convenient way to execute performances, where the logic for the performance is contained within a concrete type.
        /// This method begins a new Dependency Injection Scope, and within that scope starts the performance.
        /// It resolves an instance of the specified <typeparamref name="T"/> from the DI container, and executes its
        /// <see cref="IHostsPerformance.ExecutePerformanceAsync(CancellationToken)"/> method to get the performance result.
        /// </para>
        /// <para>
        /// An advantage of using this method is that the performance logic is encapsulated within a class, and that the service provider is
        /// used to resolve only a single object instance, thus avoiding the service locator anti-pattern.
        /// </para>
        /// <para>
        /// The <paramref name="namingHierarchy"/> may be used to give the performance a name, so that its results (and subsequent report)
        /// may be identified. This parameter has the same semantics as <see cref="Performance.NamingHierarchy"/>.
        /// </para>
        /// <para>
        /// Note that if the specified implementation of <see cref="IHostsPerformance"/> raises a <see cref="PerformableException"/> then
        /// this method will catch that exception and not rethrow.  In that case:
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
        /// <param name="screenplay">The screenplay with which to execute the logic.</param>
        /// <param name="namingHierarchy">An optional naming hierarchy used to identify the performance.</param>
        /// <param name="cancellationToken">An optional cancellation token to abort the performance logic.</param>
        /// <returns>A task which completes when the performance's logic has completed.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="screenplay"/> is <see langword="null" />.</exception>
        /// <typeparam name="T">The concrete type of an implementation of <see cref="IHostsPerformance"/> which contains the performance logic.</typeparam>
        /// <seealso cref="IHostsPerformance"/>
        public static Task ExecuteAsPerformanceAsync<T>(this Screenplay screenplay,
                                                        IList<IdentifierAndName> namingHierarchy = default,
                                                        CancellationToken cancellationToken = default) where T : IHostsPerformance
        {
            if (screenplay is null)
                throw new ArgumentNullException(nameof(screenplay));
            
            return screenplay.ExecuteAsPerformanceAsync((s, c) => s.GetRequiredService<T>().ExecutePerformanceAsync(c), namingHierarchy, cancellationToken);
        }

        /// <summary>
        /// Creates a new <see cref="IPerformance"/> within its own newly-created Dependency Injection scope.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method includes the consequence/side-effect of creating a new dependency injection scope from the
        /// <see cref="Screenplay.ServiceProvider"/> associated with the specified <see cref="Screenplay"/>.
        /// That scope will be associated with the created performance and will be returned as part of the return of this method.
        /// Please use the <see cref="IDisposable.Dispose"/> method the returned object when you are finished with the performance.
        /// This ensures that the DI scope and all associated resources (including the performance) will also be properly disposed-of.
        /// </para>
        /// </remarks>
        /// <param name="screenplay">The Screenplay from which to create the performance</param>
        /// <param name="namingHierarchy">An optional collection of identifiers and names providing the hierarchical name of this
        /// performance; see <see cref="IPerformance.NamingHierarchy"/> for more information.</param>
        /// <returns>A <see cref="ScopeAndPerformance"/> containing the newly-created performance as well as the newly-started DI scope.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="screenplay"/> is <see langword="null" />.</exception>
        public static ScopeAndPerformance CreateScopedPerformance(this Screenplay screenplay, IList<IdentifierAndName> namingHierarchy = null)
        {
            if (screenplay is null)
                throw new ArgumentNullException(nameof(screenplay));

            var scope = screenplay.ServiceProvider.CreateScope();
            var performance = scope.ServiceProvider.GetRequiredService<IPerformance>();
            if(namingHierarchy != null)
            {
                performance.NamingHierarchy.Clear();
                performance.NamingHierarchy.AddRange(namingHierarchy);
            }
            return new ScopeAndPerformance(performance, scope);
        }

        static AsyncPerformanceLogic GetAsyncPerformanceLogic(SyncPerformanceLogic syncPerformanceLogic)
        {
            return (services, token) =>
            {
                var task = new Task<bool?>(() => syncPerformanceLogic(services), token);
                task.Start();
                task.Wait(token);
                var result = task.Result;
                switch (result)
                {
                    case true: return trueTask;
                    case false: return falseTask;
                    default: return nullTask;
                }
            };
        }

        static CancellationToken GetCancellationToken(int timeoutMiliseconds)
        {
            if (timeoutMiliseconds < 0)
                throw new ArgumentOutOfRangeException(nameof(timeoutMiliseconds), "The timeout must not be negative.");

            return timeoutMiliseconds > 0 ? new CancellationTokenSource(TimeSpan.FromMilliseconds(timeoutMiliseconds)).Token : default;
        }
    }
}