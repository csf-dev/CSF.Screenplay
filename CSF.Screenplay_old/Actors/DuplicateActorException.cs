using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Exception raised when a duplicate actor is added to a <see cref="Cast"/> instance.
  /// </summary>
  [System.Serializable]
  public class DuplicateActorException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:DuplicateActorException"/> class
    /// </summary>
    public DuplicateActorException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public DuplicateActorException (string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public DuplicateActorException (string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected DuplicateActorException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
