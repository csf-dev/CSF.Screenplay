using System;
namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Exception raised when an <c>IHasFlags</c> web driver cannot provide the correct flags(s) for a given operation.
  /// </summary>
  [System.Serializable]
  public class BrowserFlagException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:BrowserFlagException"/> class
    /// </summary>
    public BrowserFlagException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:BrowserFlagException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public BrowserFlagException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:BrowserFlagException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public BrowserFlagException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:BrowserFlagException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected BrowserFlagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
