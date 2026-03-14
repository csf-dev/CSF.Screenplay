
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// An adapter for Shadow Root objects, to use them as if they were <see cref="IWebElement"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All functionality of this type throws exceptions, except for <see cref="FindElement(By)"/> and <see cref="FindElements(By)"/>.
    /// </para>
    /// </remarks>
    public class ShadowRootAdapter : IWebElement
    {
        readonly ISearchContext shadowRoot;


        /// <inheritdoc/>
        public IWebElement FindElement(By by) => shadowRoot.FindElement(by);

        /// <inheritdoc/>
        public ReadOnlyCollection<IWebElement> FindElements(By by) => shadowRoot.FindElements(by);

        /// <summary>
        /// Returns a false name indicating that it is a shadow root.
        /// </summary>
        public string TagName => "#shadow-root";

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public string Text => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public bool Enabled => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public bool Selected => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public Point Location => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public Size Size => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public bool Displayed => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public void Clear() => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public void Click() => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public string GetAttribute(string attributeName) => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public string GetCssValue(string propertyName) => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public string GetDomAttribute(string attributeName) => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public string GetDomProperty(string propertyName) => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public string GetProperty(string propertyName) => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public ISearchContext GetShadowRoot() => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public void SendKeys(string text) => throw new NotSupportedException();

        /// <summary>
        /// Unsupported functionality, always throws.
        /// </summary>
        public void Submit() => throw new NotSupportedException();

        /// <summary>
        /// Initializes a new instance of <see cref="ShadowRootAdapter"/>.
        /// </summary>
        /// <param name="shadowRoot">The wrapped shadow root element</param>
        /// <exception cref="ArgumentNullException">If <paramref name="shadowRoot"/> is <see langword="null"/></exception>
        public ShadowRootAdapter(ISearchContext shadowRoot)
        {
            this.shadowRoot = shadowRoot ?? throw new ArgumentNullException(nameof(shadowRoot));
        }
    }
}