using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// An exception which is raised when an action is attempted, but it cannot be performed because
  /// the target of that action could not be found/does not exist.
  /// </summary>
  [System.Serializable]
  public class TargetNotFoundException : Exception, IReportable
  {
    /// <summary>
    /// Gets or sets the target which was not found (leading to this exception).
    /// </summary>
    /// <value>The target.</value>
    public IHasTargetName Target { get; set; }

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    public string GetReport(INamed actor)
    {
      var targetName = (Target != null)? Target.GetName() : "the required element";
      return $"{actor.Name} cannot see {targetName} on the screen.";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TargetNotFoundException"/> class
    /// </summary>
    public TargetNotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public TargetNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public TargetNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected TargetNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
