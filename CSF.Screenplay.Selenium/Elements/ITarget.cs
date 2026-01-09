using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// A target is an object which may be interacted with by Selenium-based Screenplay performables.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Targets roughly correspond to the concept of 'elements' in Selenium WebDriver; the targets abstraction may
    /// by implemented by objects which contain one or more Selenium <c>IWebElement</c> instances.
    /// </para>
    /// <para>
    /// Targets might alternatively be implemented by objects which contain selector information, such as a CSS selector, XPath
    /// an id attribute.  In this scenario the target is conceptually 'a specification for finding elements'.
    /// A selector/specification-based target may be used to find elements at runtime, via the <see cref="GetElements"/> method.
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