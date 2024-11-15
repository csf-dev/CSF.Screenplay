using System;
using System.Runtime.Serialization;
using CSF.Screenplay.Reporting;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Thrown when <see cref="ITarget.GetElement(IWebDriver)"/> is used, but no element can be found.
    /// </summary>
    public class TargetNotFoundException : Exception, IFormattableValue
    {
        /// <summary>
        /// Gets the target for which no element could not be found.
        /// </summary>
        public ITarget Target { get; }

        /// <inheritdoc/>
        public string FormatForReport()
        {
            if (Target is IHasName namedTarget)
                return $"The {namedTarget.Name} could not be found";
            return "A target HTML element could not be found";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetNotFoundException"/> class.
        /// </summary>
        public TargetNotFoundException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">An exception message</param>
        public TargetNotFoundException(string message) : base(message) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">An exception message</param>
        /// <param name="target">The target for which no element could be found</param>
        public TargetNotFoundException(string message, ITarget target) : base(message)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">An exception message</param>
        /// <param name="inner">The inner exception</param>
        public TargetNotFoundException(string message, Exception inner) : base(message, inner) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">An exception message</param>
        /// <param name="inner">The inner exception</param>
        /// <param name="target">The target for which no element could be found</param>
        public TargetNotFoundException(string message, Exception inner, ITarget target) : base(message, inner)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetNotFoundException"/> class.
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        public TargetNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}