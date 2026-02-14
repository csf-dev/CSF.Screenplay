using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// An implementation of <see cref="ITarget"/> which represents a collection of native Selenium HTML elements.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type of <see cref="ITarget"/> contains a collection of <see cref="SeleniumElement"/>, each of which
    /// contains a native Selenium <see cref="IWebElement"/>.
    /// This class provides those elements under the ITarget interface, as well as allowing the developer to specify
    /// a <see cref="Name"/> for this collection of elements.  This optional, but recommended, technique facilitates
    /// human-readable reporting.
    /// </para>
    /// <para>
    /// It is perfectly acceptable for an element collection to contain zero elements.
    /// </para>
    /// </remarks>
    public class SeleniumElementCollection : ITarget, IReadOnlyList<SeleniumElement>
    {
        internal const string UnknownNameFormat = "a collection of {0} HTML element(s)";

        readonly IReadOnlyList<SeleniumElement> elements;

        /// <inheritdoc/>
        public string Name { get; }

        SeleniumElementCollection ITarget.GetElements(IWebDriver driver) => this;

        SeleniumElement ITarget.GetElement(IWebDriver driver) => elements.FirstOrDefault() ?? throw new TargetNotFoundException("No elements were found in the collection.", this);

        /// <inheritdoc/>
        public override string ToString() => Name;

#region IReadOnlyList<SeleniumElement> implementation

        /// <inheritdoc/>
        public int Count => elements.Count;

        /// <inheritdoc/>
        public SeleniumElement this[int index] => elements[index];

        /// <inheritdoc/>
        public IEnumerator<SeleniumElement> GetEnumerator() => elements.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumElementCollection"/> class.
        /// </summary>
        /// <param name="elements">The list of elements.</param>
        /// <param name="name">An optional human-readable name which describes the collection of elements.</param>
        public SeleniumElementCollection(IReadOnlyList<SeleniumElement> elements, string name = null)
        {
            if(elements is null) throw new ArgumentNullException(nameof(elements));
            if(elements.Any(x => x is null)) throw new ArgumentException("The collection of elements must not contain any null references.", nameof(elements));

            this.elements = elements;
            Name = name ?? string.Format(UnknownNameFormat, elements.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumElementCollection"/> class.
        /// </summary>
        /// <param name="elements">A collection of native Selenium elements.</param>
        /// <param name="name">An optional human-readable name which describes the collection of elements.</param>
        public SeleniumElementCollection(IReadOnlyCollection<IWebElement> elements, string name = null)
        {
            if(elements is null) throw new ArgumentNullException(nameof(elements));
            if(elements.Any(x => x is null)) throw new ArgumentException("The collection of elements must not contain any null references.", nameof(elements));

            this.elements = elements.Select(x => new SeleniumElement(x)).ToList();
            Name = name ?? string.Format(UnknownNameFormat, elements.Count);
        }
    }
}