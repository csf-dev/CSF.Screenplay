using System;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Matchers
{
  public abstract class ElementMatcher<TElementData> : IElementMatcher, IElementDataProvider<TElementData>
  {
    readonly Func<TElementData,bool> predicate;

    protected Func<TElementData,bool> Predicate => predicate;

    public abstract string GetDescription();

    public virtual bool IsMatch(IWebElement element)
    {
      var adapter = GetAdapter(element);
      return IsMatch(adapter);
    }

    public virtual bool IsMatch(IWebElementAdapter adapter)
    {
      var data = GetElementData(adapter);
      return Predicate(data);
    }

    protected IWebElementAdapter GetAdapter(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      return new WebElementAdapter(element);
    }

    protected abstract TElementData GetElementData(IWebElementAdapter adapter);

    TElementData IElementDataProvider<TElementData>.GetElementData(IWebElement element)
    {
      var adapter = GetAdapter(element);
      return GetElementData(adapter);
    }

    TElementData IElementDataProvider<TElementData>.GetElementData(IWebElementAdapter adapter)
    {
      if(adapter == null)
        throw new ArgumentNullException(nameof(adapter));

      return GetElementData(adapter);
    }

    public ElementMatcher() : this(x => true) {}

    public ElementMatcher(Func<TElementData,bool> predicate)
    {
      if(predicate == null)
        throw new ArgumentNullException(nameof(predicate));

      this.predicate = predicate;
    }
  }
}
