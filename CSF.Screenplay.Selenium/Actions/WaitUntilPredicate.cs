using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Represents a predicate that can be used to wait until a condition is true.
    /// </summary>
    public class WaitUntilPredicate<T> : IHasName
    {
        const string defaultName = "a predicate is satisfied";

        /// <summary>
        /// Gets the predicate function to evaluate.
        /// </summary>
        public Func<IWebDriver, T> Predicate { get; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitUntilPredicate{T}"/> class.
        /// </summary>
        /// <param name="predicate">The predicate function to evaluate.</param>
        /// <param name="name">An optional name for the predicate.</param>
        /// <exception cref="ArgumentException">Thrown if the result type of the predicate is a value type other than boolean.</exception>
        public WaitUntilPredicate(Func<IWebDriver, T> predicate, string name = null)
        {

            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            Name = name ?? defaultName;
        }
    }
}