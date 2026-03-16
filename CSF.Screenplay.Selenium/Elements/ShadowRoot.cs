using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Implementation of <see cref="IHasSearchContext"/> which represents an HTML shadow-root node.
    /// </summary>
    /// <seealso cref="Questions.GetShadowRootNatively"/>
    /// <seealso cref="Questions.GetShadowRootWithJavaScript"/>
    /// <seealso cref="Tasks.GetShadowRoot"/>
    public class ShadowRoot : IHasSearchContext
    {
        readonly ISearchContext shadowRoot;

        /// <inheritdoc/>
        public ISearchContext SearchContext => shadowRoot;

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ShadowRoot"/>.
        /// </summary>
        /// <param name="shadowRoot">The wrapped shadow root element</param>
        /// <param name="name">The name of this Shadow Root object</param>
        /// <exception cref="ArgumentNullException">If <paramref name="shadowRoot"/> is <see langword="null"/></exception>
        public ShadowRoot(ISearchContext shadowRoot, string name = null)
        {
            this.shadowRoot = shadowRoot ?? throw new ArgumentNullException(nameof(shadowRoot));
            Name = name ?? "Shadow root";
        }
    }
}