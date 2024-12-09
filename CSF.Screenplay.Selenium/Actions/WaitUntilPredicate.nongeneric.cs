using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Provides methods to create wait predicates for Selenium WebDriver.
    /// </summary>
    public static class WaitUntilPredicate
    {
        /// <summary>
        /// Creates a <see cref="WaitUntilPredicate{T}"/> from a given predicate function.
        /// </summary>
        /// <typeparam name="T">The result type of the predicate function.</typeparam>
        /// <param name="predicate">The predicate function to evaluate.</param>
        /// <param name="name">An optional name for the predicate.</param>
        /// <returns>A new instance of <see cref="WaitUntilPredicate"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if the result type of the predicate is a value type other than boolean.</exception>
        public static WaitUntilPredicate<T> From<T>(Func<IWebDriver,T> predicate, string name = null)
        {
            var resultType = typeof(T);
            if(resultType.IsValueType && resultType != typeof(bool))
                throw new ArgumentException("The result type of the predicate must be a boolean or a reference type.", nameof(predicate));
            
            return new WaitUntilPredicate<T>(predicate, name);
        }
    }
}