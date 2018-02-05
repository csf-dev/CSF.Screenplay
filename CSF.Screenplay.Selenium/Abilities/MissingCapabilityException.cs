using System;
namespace CSF.Screenplay.Selenium.Abilities
{
  /// <summary>
  /// Exception raised when a capability is demanded of a <see cref="BrowseTheWeb"/> ability but it is not available.
  /// </summary>
  [System.Serializable]
  public class MissingCapabilityException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:MissingCapabilityException"/> class
    /// </summary>
    public MissingCapabilityException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public MissingCapabilityException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public MissingCapabilityException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected MissingCapabilityException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
