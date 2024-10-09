using System;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A simple model for a value that is to be included in a formatted <see cref="ReportFragment"/>, which has an associated name.
    /// </summary>
    public class NameAndValue
    {
        /// <summary>
        /// Gets the name associated with this value.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public object Value { get; }
        
        /// <summary>
        /// Initializes a new instance of <see cref="NameAndValue"/>.
        /// </summary>
        /// <param name="name">The name for the value</param>
        /// <param name="value">The value</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null" />.</exception>
        public NameAndValue(string name, object value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
        }
    }
}
