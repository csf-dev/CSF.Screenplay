using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An object which is similar to <see cref="IPerformable"/> but provides access to a Selenium WebDriver and a Selenium element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is specialised for performables which interact with a single Selenium element.
    /// It allows for the provision of some of the boilerplate to set up such a performable.
    /// </para>
    /// <para>
    /// See <see cref="IPerformable"/> for more information about how this interface would be used in general terms.
    /// In order to actually use implementations of this interface as performables, they should be wrapped in an
    /// <see cref="SingleElementPerformableAdapter"/>.
    /// </para>
    /// </remarks>
    public interface ISingleElementPerformable : ICanReportForElement
    {
        /// <summary>
        /// Counterpart to <see cref="IPerformable.PerformAsAsync(ICanPerform, CancellationToken)"/> except that this method also offers a Selenium WebDriver and element.
        /// </summary>
        /// <param name="actor">The actor that is performing.</param>
        /// <param name="webDriver">The Selenium WebDriver provided from the actor's abilities.</param>
        /// <param name="element">The single Selenium Element upon which this method should operate.</param>
        /// <param name="cancellationToken">An optional cancellation token by which to abort the performable.</param>
        /// <returns>A task which completes when the performable represented by the current instance is complete.</returns>
        ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default);
    }
}