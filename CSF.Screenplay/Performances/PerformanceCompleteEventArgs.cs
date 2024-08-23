using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// A model for event arguments which relate to a completion of a <see cref="Performance"/>.
    /// </summary>
    /// <seealso cref="Performance"/>
    /// <seealso cref="PerformanceScopeEventArgs"/>
    /// <seealso cref="PerformanceEventArgs"/>
    public class PerformanceCompleteEventArgs : PerformanceEventArgs
    {
        /// <summary>Gets a value indicating whether the completion of the <see cref="Performance"/> was a success or not</summary>
        /// <remarks>
        /// <para>
        /// The three possible values for this property correspond to three possible values of <see cref="PerformanceState"/>
        /// which represent a performance that has finished.
        /// </para>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <description>Corresponding state</description>
        /// </listheader>
        /// <item><term><see langword="true" /></term><description><see cref="PerformanceState.Success"/></description></item>
        /// <item><term><see langword="false" /></term><description><see cref="PerformanceState.Failed"/></description></item>
        /// <item><term><see langword="null" /></term><description><see cref="PerformanceState.Completed"/></description></item>
        /// </list>
        /// <para>
        /// When using Screenplay with <xref href="IntegrationGlossaryItem?an+automated+testing+integration"/>, these three performance
        /// states may go on to correspond to a test pass, failure or skipped/ignored test, respectively.
        /// </para>
        /// </remarks>
        public bool? Success { get; }

        /// <summary>Initialises a new instance of <see cref="PerformanceEventArgs"/></summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="namingHierarchy">The scenario hierarchy</param>
        /// <param name="success">A value indicating whether or not the scenario completed with a succeess result</param>
        /// <exception cref="ArgumentNullException">If the scenario hierarchy is <see langword="null" /></exception>
        public PerformanceCompleteEventArgs(Guid performanceIdentity,
                                         IReadOnlyList<IdentifierAndName> namingHierarchy,
                                         bool? success) : base(performanceIdentity, namingHierarchy)
        {
            Success = success;
        }
    }
}