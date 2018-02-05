using System;
namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Exception raised when a flag is required of an <c>IHasFlags</c>web driver but it was not present.
  /// </summary>
  [System.Serializable]
  public class FlagRequiredException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:FlagRequiredException"/> class
    /// </summary>
    public FlagRequiredException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:FlagRequiredException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public FlagRequiredException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:FlagRequiredException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public FlagRequiredException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:FlagRequiredException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected FlagRequiredException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
