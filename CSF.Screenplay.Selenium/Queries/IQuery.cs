using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// An object which can get a value from a Selenium element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each implementation of <see cref="IQuery{T}"/> is responsible for getting a specific kind of value from a Selenium element.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The type of the value returned.</typeparam>
    public interface IQuery<out T> : IHasName
    {
        /// <summary>
        /// Gets the current value from a Selenium element.
        /// </summary>
        /// <param name="element">The Selenium element.</param>
        /// <returns>The value of type <typeparamref name="T"/>.</returns>
        T GetValue(SeleniumElement element);
    }
}