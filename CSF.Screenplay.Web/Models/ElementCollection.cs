using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
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
  public class ElementCollection : IHasTargetName
  {
    readonly IReadOnlyList<IWebElement> elements;
    readonly string name;

    string IHasTargetName.GetName() => Name;

    /// <summary>
    /// Gets the elements.
    /// </summary>
    /// <value>The elements.</value>
    public IReadOnlyList<IWebElement> Elements => elements;

    /// <summary>
    /// Gets the logical/human-readable name for these elements.
    /// </summary>
    /// <value>The name.</value>
    public string Name => name;

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementCollection"/> class.
    /// </summary>
    /// <param name="elements">The collection of elements.</param>
    /// <param name="name">The human-readable name for this collection.</param>
    public ElementCollection(IReadOnlyList<IWebElement> elements, string name = null)
    {
      if(elements == null)
        throw new ArgumentNullException(nameof(elements));

      this.elements = elements;
      this.name = name?? "the elements";
    }
  }
}
