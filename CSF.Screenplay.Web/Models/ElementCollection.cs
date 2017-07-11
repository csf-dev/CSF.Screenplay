using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class ElementCollection : IHasTargetName
  {
    readonly IReadOnlyList<IWebElement> elements;
    readonly string name;

    string IHasTargetName.GetName() => Name;

    public IReadOnlyList<IWebElement> Elements => elements;

    public string Name => name;

    public ElementCollection(IReadOnlyList<IWebElement> elements, string name)
    {
      if(elements == null)
        throw new ArgumentNullException(nameof(elements));

      this.elements = elements;
      this.name = name?? "the elements";
    }
  }
}
