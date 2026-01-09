using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Similar to <see cref="IPerformableWithResult{TResult}"/> but provides access to a Selenium WebDriver and a Selenium element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is specialised for performables which interact with a single Selenium element.
    /// It allows for the provision of some of the boilerplate to set up such a performable.
    /// </para>
    /// <para>
    /// See <see cref="IPerformableWithResult{TResult}"/> for more information about how this interface would be used in general terms.
    /// In order to actually use implementations of this interface as performables, they should be wrapped in an
    /// <see cref="SingleElementPerformableWithResultAdapter{TResult}"/>.
    /// </para>
    /// </remarks>
    public interface ISingleElementPerformableWithResult<TResult> : ICanReportForElement
    {
        /// <summary>
        /// Counterpart to <see cref="IPerformableWithResult{TResult}.PerformAsAsync(ICanPerform, CancellationToken)"/> except that this method also offers
        /// a Selenium WebDriver and element.
        /// </summary>
        /// <param name="actor">The actor that is performing.</param>
        /// <param name="webDriver">The Selenium WebDriver provided from the actor's abilities.</param>
        /// <param name="element">The single Selenium Element upon which this method should operate.</param>
        /// <param name="cancellationToken">An optional cancellation token by which to abort the performable.</param>
        /// <returns>A task which exposes a strongly-typed 'result' value when the performable represented by the current instance is complete.</returns>
        ValueTask<TResult> PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default);
    }
}