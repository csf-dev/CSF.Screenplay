using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Queries
{
  /// <summary>
  /// Gets the value of the named HTML attribute.
  /// </summary>
  public class AttributeQuery : Query<string>
  {
    readonly string attributeName;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    /// <param name="targetName">A human-readable name for the target which is reading data.</param>
    protected override string GetQuestionReport(INamed actor, string targetName)
      => $"{actor.Name} reads the {attributeName} attribute from {targetName}.";

    /// <summary>
    /// Gets a description for a match on this value, suitable for an <see cref="ElementMatching.IMatcher"/>
    /// </summary>
    /// <returns>The match description.</returns>
    protected override string GetMatchDescription()
      => $"a matching {attributeName} attribute";

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Element.</param>
    protected override string GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetAttributeValue(attributeName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AttributeQuery"/> class.
    /// </summary>
    /// <param name="attributeName">Attribute name.</param>
    public AttributeQuery(string attributeName)
    {
      if(attributeName == null)
        throw new ArgumentNullException(nameof(attributeName));

      this.attributeName = attributeName;
    }
  }
}
