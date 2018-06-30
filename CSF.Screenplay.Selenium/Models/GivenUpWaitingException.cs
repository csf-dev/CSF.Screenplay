using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// Exception raised when an actor gives up on a wait performance.
  /// </summary>
  [System.Serializable]
  public class GivenUpWaitingException : Exception, IProvidesReport
  {
    string IProvidesReport.GetReport(INamed actor) => $"{actor.Name} gives up on waiting because it has taken too long.";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:GivenUpWaitingException"/> class
    /// </summary>
    public GivenUpWaitingException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:GivenUpWaitingException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public GivenUpWaitingException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:GivenUpWaitingException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public GivenUpWaitingException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:GivenUpWaitingException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected GivenUpWaitingException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
