using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Exception which is raised when an attempt is made to get a Screenplay service, but that service is not registered.
  /// </summary>
  [System.Serializable]
  public class ServiceNotRegisteredException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:ServiceNotRegisteredException"/> class
    /// </summary>
    public ServiceNotRegisteredException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public ServiceNotRegisteredException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public ServiceNotRegisteredException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:MyException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected ServiceNotRegisteredException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
