using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// A target is an object which represents one or more HTML elements and which may serve as parameters for performables
    /// which interact with elements on the web page.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See <xref href="SeleniumTargetsAndElementsArticle?text=the+targets+and+elements+article"/> for more information.
    /// The <c>ITarget</c> interface unifies two Selenium concepts: <see cref="IWebElement"/> instances and the
    /// <see cref="By"/> class.
    /// Targets correspond to the concept of HTML elements on a web page and may be used as the parameters for
    /// <xref href="PerformableGlossaryItem?text=performable+classes"/> which interact with those elements.
    /// </para>
    /// <para>
    /// The <see cref="SeleniumElement"/> and <see cref="SeleniumElementCollection"/> classes wrap native Selenium
    /// web element instances. They are concrete references to elements which have been retrieved from the WebDriver.
    /// In both of these cases, the <see cref="GetElement(IWebDriver)"/> &amp; <see cref="GetElements(IWebDriver)"/>
    /// methods will often return references to themselves without making any use of the WebDriver parameter.
    /// </para>
    /// <para>
    /// On the other hand, targets which derive from <see cref="Locator"/>, such as <see cref="ElementId"/>, <see cref="ClassName"/>,
    /// <see cref="CssSelector"/> and <see cref="XPath"/> identify HTML element(s) without being a concrete reference to them.
    /// A Locator is conceptually 'a specification for finding elements'.
    /// Locator-based targets will use the WebDriver parameter in their <see cref="GetElement(IWebDriver)"/> and
    /// <see cref="GetElements(IWebDriver)"/> methods, in order to retrieve concrete element references.
    /// </para>
    /// </remarks>
    public interface ITarget : IHasName
    {
        /// <summary>
        /// Gets a single Selenium element which matches the current target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may invoke the specified Selenium <c>IWebDriver</c> to get the element which matches a specification/selector.
        /// Alternatively if the target implementation already represents one or more concrete element instances then it will
        /// be returned directly.
        /// Crucially, if the target represents multiple elements then this method will return only the first element.
        /// </para>
        /// </remarks>
        /// <param name="driver">A Selenium Web Driver</param>
        /// <returns>A <see cref="SeleniumElement"/> which matches the target</returns>
        /// <exception cref="TargetNotFoundException">If the target does not yield an HTML element</exception>
        SeleniumElement GetElement(IWebDriver driver);

        /// <summary>
        /// Gets a collection of Selenium elements which match the current target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may invoke the specified Selenium <c>IWebDriver</c> to find elements which match a specification/selector.
        /// Alternatively if the target implementation already represents one or more concrete element instances then these will
        /// be returned directly.
        /// </para>
        /// </remarks>
        /// <param name="driver">A Selenium Web Driver</param>
        /// <returns>A collection of zero or more Selenium elements</returns>
        SeleniumElementCollection GetElements(IWebDriver driver);
    }
}