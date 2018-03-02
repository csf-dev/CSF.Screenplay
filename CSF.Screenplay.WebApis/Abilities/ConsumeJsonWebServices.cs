using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.JsonApis.Abilities
{
  /// <summary>
  /// A Screenplay <see cref="Ability"/> by which actors may consume JSON web APIs (RESTful or otherwise).
  /// </summary>
  public class ConsumeJsonWebServices : Ability
  {
    #region fields

    static readonly TimeSpan SystemDefaultTimeout = new TimeSpan(0, 0, 30);

    readonly TimeSpan defaultTimeout;
    readonly SynchronousJsonAdapter jsonAdapter;

    #endregion

    #region public API

    /// <summary>
    /// Executes a web API using the specified invocation details.
    /// </summary>
    /// <param name="invocationDetails">Invocation details which describe how the API should be called.</param>
    public virtual void Execute(IProvidesInvocationDetails invocationDetails)
      => jsonAdapter.GetResponse(invocationDetails.GetRequestMessage(), GetTimeout(invocationDetails));

    /// <summary>
    /// Executes a web API using the specified invocation details and returns the result.
    /// </summary>
    /// <returns>The API result.</returns>
    /// <param name="invocationDetails">Invocation details which describe how the API should be called.</param>
    /// <typeparam name="T">The expected type of the result object.</typeparam>
    public virtual T GetResult<T>(IProvidesInvocationDetails invocationDetails)
      => jsonAdapter.GetResponse<T>(invocationDetails.GetRequestMessage(), GetTimeout(invocationDetails));

    #endregion

    #region private methods

    TimeSpan GetTimeout(IProvidesInvocationDetails invocationDetails)
      => invocationDetails.GetTimeout().GetValueOrDefault(defaultTimeout);

    #endregion

    #region boilerplate Ability overrides

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(Actors.INamed actor)
      => $"{actor.Name} can consume JSON web services.";

    /// <summary>
    /// Performs disposal of the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> then we are explicitly disposing.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      if(disposing)
        jsonAdapter.Dispose();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.JsonApis.Abilities.ConsumeJsonWebServices"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <paramref name="baseUriString"/> parameter is used to qualify any API invocations in which the API
    /// endpoint URI is a relative URI.  This allows for injection of the 'web API root URL'.
    /// </para>
    /// </remarks>
    /// <param name="baseUriString">A string which indicates a base URI.</param>
    /// <param name="defaultTimeout">The default service timeout.</param>
    public ConsumeJsonWebServices(string baseUriString, TimeSpan? defaultTimeout = null)
      : this(new Uri(baseUriString, UriKind.Absolute), defaultTimeout) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.JsonApis.Abilities.ConsumeJsonWebServices"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <paramref name="baseUri"/> parameter is used to qualify any API invocations in which the API
    /// endpoint URI is a relative URI.  This allows for injection of the 'web API root URL'.
    /// </para>
    /// </remarks>
    /// <param name="baseUri">A base URI.</param>
    /// <param name="defaultTimeout">The default service timeout.</param>
    public ConsumeJsonWebServices(Uri baseUri = null, TimeSpan? defaultTimeout = null)
    {
      this.defaultTimeout = defaultTimeout.GetValueOrDefault(SystemDefaultTimeout);
      jsonAdapter = new SynchronousJsonAdapter(baseUri);
    }

    #endregion
  }
}
