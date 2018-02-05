using System;
namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Enumerates the mechanisms by which an actor may pick an HTML <c>option</c> element from within a
  /// <c>select</c> element.
  /// </summary>
  enum SelectStrategy
  {
    /// <summary>
    /// By the options's numeric (zero-based) position.
    /// </summary>
    Index,

    /// <summary>
    /// By the option's displayed human-readable text.
    /// </summary>
    Text,

    /// <summary>
    /// By the option's underlying value.
    /// </summary>
    Value
  }
}
