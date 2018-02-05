using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Selenium.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// Represents a collection of HTML elements, with a logical/human-readable name.
  /// </summary>
  /// <remarks>
  /// <para>
  /// When dealing with collections of elements in this manner, the name may serve as a useful indicator as to
  /// what the underlying collection represents.
  /// For example, imagine that the user gets a collection using the CSS selector <c>ul.products .name</c>.  The
  /// human-readable name for the collection which is exposed might be <c>the product names</c>.
  /// </para>
  /// </remarks>
  public class ElementCollection : ITarget
  {
    readonly IReadOnlyList<IWebElementAdapter> elements;
    readonly string name;

    /// <summary>
    /// Gets the human-readable target name.
    /// </summary>
    /// <returns>The name.</returns>
    public string GetName() => name ?? "the elements";

    /// <summary>
    /// Gets a web element adapter from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="ability">Ability.</param>
    public IWebElementAdapter GetWebElementAdapter(BrowseTheWeb ability) => elements.FirstOrDefault();

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="ability">Ability.</param>
    public ElementCollection GetWebElementAdapters(BrowseTheWeb ability) => this;

    /// <summary>
    /// Gets a web element adapter from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="driver">The web driver.</param>
    public IWebElementAdapter GetWebElementAdapter(IWebDriver driver) => elements.FirstOrDefault();

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="driver">The web driver.</param>
    public ElementCollection GetWebElementAdapters(IWebDriver driver) => this;

    /// <summary>
    /// Gets the elements.
    /// </summary>
    /// <value>The elements.</value>
    public IReadOnlyList<IWebElementAdapter> Elements => elements;

    /// <summary>
    /// Gets the logical/human-readable name for these elements.
    /// </summary>
    /// <value>The name.</value>
    public string Name => name;

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="ElementCollection"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="ElementCollection"/>.</returns>
    public override string ToString() => $"[{nameof(ElementCollection)}:{GetName()}]";

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementCollection"/> class.
    /// </summary>
    /// <param name="elements">The collection of elements.</param>
    /// <param name="name">The human-readable name for this collection.</param>
    public ElementCollection(IReadOnlyList<IWebElementAdapter> elements, string name = null)
    {
      if(elements == null)
        throw new ArgumentNullException(nameof(elements));

      this.elements = elements;
      this.name = name;
    }
  }
}
