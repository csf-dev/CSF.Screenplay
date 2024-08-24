using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// A model for event arguments which relate to a scope of a <see cref="Performance"/>.
    /// </summary>
    /// <seealso cref="Performance"/>
    /// <seealso cref="PerformanceScopeEventArgs"/>
    public class PerformanceEventArgs : PerformanceScopeEventArgs
    {
        /// <summary>Gets an ordered list of identifiers which indicate the <see cref="Performance"/>'s name within an organisational hierarchy.</summary>
        /// <remarks>
        /// <para>
        /// This hierarchical name has the exact same meaning and corresponds directly to <see cref="Performance.NamingHierarchy"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="Performance"/>
        /// <seealso cref="Performance.NamingHierarchy"/>
        public IReadOnlyList<IdentifierAndName> NamingHierarchy { get; }

        /// <summary>Initialises a new instance of <see cref="PerformanceEventArgs"/></summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="namingHierarchy">The screenplay naming hierarchy</param>
        /// <exception cref="ArgumentNullException">If the scenario hierarchy is <see langword="null" /></exception>
        public PerformanceEventArgs(Guid performanceIdentity, IReadOnlyList<IdentifierAndName> namingHierarchy) : base(performanceIdentity)
        {
            NamingHierarchy = namingHierarchy ?? throw new ArgumentNullException(nameof(namingHierarchy));
        }
    }
}