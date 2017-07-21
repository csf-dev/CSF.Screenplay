using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.ElementMatching;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;

namespace CSF.Screenplay.Web.Builders
{
  public class ElementsWithin
  {
    ITarget innerTarget, target;
    IMatcher matcher;

    public ElementsWithin ThatAre(ITarget target)
    {
      this.innerTarget = target;
      return this;
    }

    public ElementsWithin That(IMatcher matcher)
    {
      this.matcher = matcher;
      return this;
    }

    public IQuestion<ElementCollection> Called(string name)
    {
      return new FindElements(target, innerTarget, matcher, name);
    }

    public IQuestion<ElementCollection> Get()
    {
      return new FindElements(target, innerTarget, matcher, target.GetName());
    }

    public ElementsWithin(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }
  }
}
