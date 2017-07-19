using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;

namespace CSF.Screenplay.Web.Builders
{
  public class ElementsOnPage
  {
    ITarget innerTarget;
    IElementMatcher matcher;

    public ElementsOnPage ThatAre(ITarget target)
    {
      this.innerTarget = target;
      return this;
    }

    public ElementsOnPage That(IElementMatcher matcher)
    {
      this.matcher = matcher;
      return this;
    }

    public IQuestion<ElementCollection> Called(string name)
    {
      return new FindElementsOnPage(innerTarget, matcher, name);
    }
  }
}
