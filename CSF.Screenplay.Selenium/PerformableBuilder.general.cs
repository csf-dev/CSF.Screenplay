using System;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Questions;
using CSF.Screenplay.Selenium.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Builder type for creating performables which interact with Selenium WebDriver via Screenplay.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Consume this class from your own Screenplay logic with <c>using static CSF.Screenplay.Selenium.PerformableBuilder;</c>.
    /// </para>
    /// </remarks>
    public static partial class PerformableBuilder
    {
        /// <summary>
        /// Gets a performable action which opens a URL.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the specified Uri is a relative Uri, then this task will use the actor's <see cref="UseABaseUri"/> ability (if present)
        /// to transform the relative Uri into an absolute one.  The specified Uri will be used directly if it is already absolute.
        /// </para>
        /// </remarks>
        /// <param name="uri">The uri at which to open the web browser.</param>
        /// <returns>A performable</returns>
        public static IPerformable OpenTheUrl(NamedUri uri) => new OpenUrlRespectingBase(uri);

        /// <summary>
        /// Gets a performable question which takes a screenshot of the current web page and returns it.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to take a screenshot and save it as an asset file, please consider <see cref="TakeAndSaveAScreenshot()"/>
        /// instead of this method.
        /// </para>
        /// <para>
        /// This method will raise an exception if the WebDriver is not capable of taking screenshots. If you want to avoid this, consider
        /// using <see cref="TakeAScreenshotIfSupported"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable</returns>
        public static IPerformableWithResult<Screenshot> TakeAScreenshot() => new TakeScreenshot();

        /// <summary>
        /// Gets a performable question which takes a screenshot of the current web page and returns it, if the WebDriver is capable of doing so.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to take a screenshot and save it as an asset file, please consider <see cref="TakeAndSaveAScreenshot()"/>
        /// instead of this method.
        /// </para>
        /// <para>
        /// This method will not raise an exception if the WebDriver is not capable of taking screenshots.  Instead, it will return a value task
        /// of <see langword="null" />. If you wish to use a performable which will raise an exception if the WebDriver is not capable of taking
        /// screenshots, consider using <see cref="TakeAScreenshot"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable</returns>
        public static IPerformableWithResult<Screenshot> TakeAScreenshotIfSupported() => new TakeScreenshot(false);

        /// <summary>
        /// Gets a performable action which saves a screenshot to a file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method returns a builder object which may be used to specify a short name for the screenshot.
        /// This allows it to be quickly identified in the report.
        /// </para>
        /// <para>
        /// If you only want to take a screenshot and save it as an asset file, please consider <see cref="TakeAndSaveAScreenshot()"/>
        /// instead of this method.
        /// </para>
        /// </remarks>
        /// <param name="screenshot">The Selenium screenshot instance</param>
        /// <returns>A performable</returns>
        public static SaveScreenshotBuilder SaveTheScreenshot(Screenshot screenshot) => new SaveScreenshotBuilder(screenshot);

        /// <summary>
        /// Gets a performable taek which takes a screenshot and saves it to a file, using an optional short name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may be used to specify a short name for the screenshot.
        /// This allows it to be quickly identified in the report.
        /// </para>
        /// <para>
        /// This method will raise an exception if the WebDriver is not capable of taking screenshots. If you want to avoid this, consider
        /// using <see cref="TakeAndSaveAScreenshotIfSupported"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable</returns>
        public static TakeAndSaveScreenshotBuilder TakeAndSaveAScreenshot() => new TakeAndSaveScreenshotBuilder(true);

        /// <summary>
        /// Gets a performable taek which takes a screenshot and saves it to a file, using an optional short name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may be used to specify a short name for the screenshot.
        /// This allows it to be quickly identified in the report.
        /// </para>
        /// <para>
        /// This method will not raise an exception if the WebDriver is not capable of taking screenshots.  Instead, it will do nothing.
        /// If you wish to use a performable which will raise an exception if the WebDriver is not capable of taking
        /// screenshots, consider using <see cref="TakeAndSaveAScreenshot"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable</returns>
        public static TakeAndSaveScreenshotBuilder TakeAndSaveAScreenshotIfSupported() => new TakeAndSaveScreenshotBuilder(false);

        /// <summary>
        /// Gets a performable action which clears all cookies for the current domain.
        /// </summary>
        /// <returns>A performable action</returns>
        public static IPerformable ClearAllDomainCookies() => new ClearCookies();

        /// <summary>
        /// Gets a performable action which deletes a single named cookie.
        /// </summary>
        /// <returns>A performable action</returns>
        public static IPerformable DeleteTheCookieNamed(string cookieName) => new DeleteTheCookie(cookieName);

        /// <summary>
        /// Gets a performable action which clears the local storage for the current domain.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method gets a performable which will throw an exception if the WebDriver does not support HTML5 Web Storage.
        /// To avoid this, consider using <see cref="ClearLocalStorageIfSupported"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable action</returns>
        public static IPerformable ClearLocalStorage() => new ClearLocalStorage();

        /// <summary>
        /// Gets a performable action which clears the local storage for the current domain if the WebDriver supports it.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method gets a performable which will do nothing if the WebDriver does not support HTML5 Web Storage.
        /// </para>
        /// </remarks>
        /// <returns>A performable action</returns>
        public static IPerformable ClearLocalStorageIfSupported() => new ClearLocalStorage(false);

        /// <summary>
        /// Gets a builder which will create a performable question that waits until a predicate returns a successful result.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The prupose of waiting in this manner is to wait for something to happen on the web page, such as an element being present, or
        /// having content that matches a specification.
        /// </para>
        /// <para>
        /// The builder object returned by this method has a number of optional configuration methods.
        /// </para>
        /// <para>
        /// Create the <paramref name="predicate"/> paramater value by using one of the following extension methods.  These extension methods
        /// provide a fluent interface to create a predicate for one or more elements (described by a single <see cref="ITarget"/>), as well
        /// as providing a human-readable name for the predicate.
        /// </para>
        /// <list type="bullet">
        /// <item><description><see cref="TargetExtensions.Has(ITarget)"/></description></item>
        /// <item><description><see cref="TargetExtensions.AllHave(ITarget)"/></description></item>
        /// </list>
        /// </remarks>
        /// <param name="predicate">A predicate which, when it returns a successful result, the wait is over.</param>
        /// <returns>A builder for creating a wait action.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the result type of the predicate is a value type other than boolean.</exception>
        public static NamedWaitBuilder WaitUntil(IBuildsElementPredicates predicate)
            => new NamedWaitBuilder(predicate.GetWaitUntilPredicate());

        /// <summary>
        /// Gets a builder which will create a performable question that waits until a predicate returns a <see langword="true"/> result.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The purpose of waiting in this manner is to wait for something to happen on the web page, such as an element being present, or
        /// having content that matches a specification.
        /// </para>
        /// <para>
        /// The builder object returned by this method has a number of optional configuration methods. Of these, consumers are
        /// strongly encouraged to use <see cref="UnnamedWaitBuilder.Named(string)"/> to give the wait action a short, descriptive name which will
        /// appear in reports.
        /// </para>
        /// <para>
        /// Take note that it is very normal for the <paramref name="predicate"/> to make use of closures to access elements from outside its own
        /// scope. For example, the predicate function may refer to an element which is referenced by a variable in the calling method.
        /// </para>
        /// <para>
        /// Where possible, consider using the other overload of this method: <see cref="WaitUntil(IBuildsElementPredicates)"/>, as it provides
        /// a more fluent interface for describing the predicate.
        /// This overload is provided only for scenarios in which the predicate to end the wait is too complex to be easily expressed
        /// using the fluent interface.
        /// </para>
        /// </remarks>
        /// <param name="predicate">A predicate which, when it returns a <see langword="true"/> result, the wait is over.</param>
        /// <returns>A builder for creating a wait action.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the result type of the predicate is a value type other than boolean.</exception>
        public static UnnamedWaitBuilder WaitUntil(Func<IWebDriver,bool> predicate) => new UnnamedWaitBuilder(predicate);

        /// <summary>
        /// Gets a performable action that waits for a specified amount of time.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This kind of wait waits for a specified time.  If you want to wait until a condition is met, consider using either
        /// <see cref="WaitUntil(Func{IWebDriver, bool})"/> or <see cref="WaitUntil(IBuildsElementPredicates)"/> instead.
        /// </para>
        /// </remarks>
        /// <param name="duration">The duration for which to wait.</param>
        /// <returns>A performable action.</returns>
        public static WaitForSomeTime WaitFor(TimeSpan duration) => new WaitForSomeTime(duration);

        /// <summary>
        /// Gets a performable question which reads the title of the current browser window.
        /// </summary>
        /// <returns>A performable question.</returns>
        public static GetWindowTitle ReadTheWindowTitle() => new GetWindowTitle();
    }
}