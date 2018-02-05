using System;
namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// Exception raised when a provided target is not valid for the action/task.
  /// </summary>
  [System.Serializable]
  public class InvalidTargetException : System.Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:InvalidTargetException"/> class
    /// </summary>
    public InvalidTargetException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public InvalidTargetException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public InvalidTargetException(string message, System.Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected InvalidTargetException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
