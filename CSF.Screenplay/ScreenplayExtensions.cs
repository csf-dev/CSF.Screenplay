using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Performances;

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
        /// Use this method only if <see cref="Screenplay.ExecuteAsPerformanceAsync(Func{CancellationToken, Task{bool?}}, IList{IdentifierAndName}, CancellationToken)"/> is not viable.
        /// This method executes the logic asynchronously, as is the architecture of Screenplay, but then uses <see cref="Task.Wait()"/> to convert
        /// the asynchronous result into a synchronous one.
        /// </para>
        /// <para>
        /// If <paramref name="timeoutMiliseconds"/> is not specified, or specified with a zero value then there will be no timeout
        /// applied to the performance's logic.  If specified with a positive integer then the performance logic will be aborted if
        /// the specified timeout (in milliseconds) is exceeded.
        /// </para>
        /// </remarks>
        /// <param name="screenplay">The screenplay with which to execute the logic.</param>
        /// <param name="performanceLogic">The logic to be executed by the performance.</param>
        /// <param name="namingHierarchy">An optional naming hierarchy used to identify the performance.</param>
        /// <param name="timeoutMiliseconds">If set to a non-zero positive value, then the performance logic will be aborted after the specified timeout in milliseconds.</param>
        /// <exception cref="ArgumentNullException">If either <paramref name="screenplay"/> or <paramref name="performanceLogic"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="timeoutMiliseconds"/> is a negative number.</exception>
        public static void ExecuteAsPerformance(this Screenplay screenplay,
                                                Func<bool?> performanceLogic,
                                                IList<IdentifierAndName> namingHierarchy = default,
                                                int timeoutMiliseconds = default)
        {
            if (screenplay is null)
                throw new ArgumentNullException(nameof(screenplay));
            if (performanceLogic is null)
                throw new ArgumentNullException(nameof(performanceLogic));
            if (timeoutMiliseconds < 0)
                throw new ArgumentOutOfRangeException(nameof(timeoutMiliseconds), "The timeout must not be negative.");

            var token = GetCancellationToken(timeoutMiliseconds);
            screenplay.ExecuteAsPerformanceAsync(GetAsyncPerformanceLogic(performanceLogic),
                                                 namingHierarchy,
                                                 token)
                .Wait();
        }

        static Func<CancellationToken,Task<bool?>> GetAsyncPerformanceLogic(Func<bool?> syncPerformanceLogic)
        {
            return token =>
            {
                var task = new Task<bool?>(syncPerformanceLogic);
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
            => timeoutMiliseconds > 0 ? new CancellationTokenSource(TimeSpan.FromMilliseconds(timeoutMiliseconds)).Token : default;
    }
}