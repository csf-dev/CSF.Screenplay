using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.ElementMatching;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// A builder type which returns a collection of matching elements from within a parent.
  /// </summary>
  public class ElementsWithin
  {
    ILocatorBasedTarget innerTarget;
    ITarget target;
    IMatcher matcher;

    /// <summary>
    /// Indicates that only elements which match the given target specification are to be returned.
    /// </summary>
    /// <returns>The builder instance.</returns>
    /// <param name="target">A target specification.</param>
    public ElementsWithin ThatAre(ILocatorBasedTarget target)
    {
      this.innerTarget = target;
      return this;
    }

    /// <summary>
    /// Indicates that only elements which match a given <see cref="IMatcher"/> are to be returned.
    /// </summary>
    /// <returns>The builder instance.</returns>
    /// <param name="matcher">A matcher instance.</param>
    public ElementsWithin That(IMatcher matcher)
    {
      this.matcher = matcher;
      return this;
    }

    /// <summary>
    /// Assigns a name to the collection of elements which are returned and gets the performable question.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="name">A name for the returned collection of elements.</param>
    public IQuestion<ElementCollection> Called(string name)
    {
      return new FindElements(target, innerTarget, matcher, name);
    }

    /// <summary>
    /// Gets the performable question.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    public IQuestion<ElementCollection> Get()
    {
      var nameTarget = innerTarget?? target;
      return new FindElements(target, innerTarget, matcher, nameTarget.GetName());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Builders.ElementsWithin"/> class.
    /// </summary>
    /// <param name="target">Target.</param>
    public ElementsWithin(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }
  }
}
