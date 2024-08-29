using System;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// Thrown when a Performable fails with an unexpected exception.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This exception type is rethrown from the <see cref="Actor"/> when they are executing the
    /// <xref href="PerformableGlossaryItem?text=performable"/>, in order to provide further context about where the error has occurred.
    /// </para>
    /// </remarks>
    public class PerformableException : Exception
    {
        /// <summary>
        /// Gets or sets a reference to <xref href="PerformableGlossaryItem?text=the+Performable+object"/> which was the cause of the exception.
        /// </summary>
        public object Performable { get; set; }

        /// <summary>
        /// Initialises a new instance of <see cref="PerformableException"/>.
        /// </summary>
        public PerformableException() {}

        /// <summary>
        /// Initialises a new instance of <see cref="PerformableException"/>.
        /// </summary>
        /// <param name="message">The exception message</param>
        public PerformableException(string message) : base(message) {}

        /// <summary>
        /// Initialises a new instance of <see cref="PerformableException"/>.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception</param>
        public PerformableException(string message, Exception inner) : base(message, inner) {}
    }
}