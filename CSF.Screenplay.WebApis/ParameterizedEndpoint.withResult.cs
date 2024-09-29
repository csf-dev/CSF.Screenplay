using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Base type for a Web API endpoint which has a strongly-typed request payload and which is expected to return a response body that exposes a strongly-typed object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// There are several concrete types of endpoint available, all of which derive from <see cref="EndpointBase"/>, for more information
    /// about the purpose of endpoints and how they are used, see the documentation for that base type.
    /// </para>
    /// <para>
    /// The manner in which this endpoint exposes the strongly typed response object is undefined within the endpoint itself.  It is down to
    /// a <xref href="PerformableGlossaryItem?text=performable+implementation"/> to deserialize the result object from the HTTP response content.
    /// </para>
    /// <para>
    /// Developers are welcome to create specialized derived types based upon this or other subclasses of <see cref="EndpointBase"/> if they
    /// have specific needs. Derived classes should <see langword="override"/> <see cref="GetHttpRequestMessageBuilder"/> with a method that
    /// calls the base implementation and then further manipulates the message builder before returning it.
    /// This particular class may only be used as a base type for specialized implementations.
    /// Implementing types must provide an implementation of <see cref="GetHttpRequestMessageBuilder(TParameters)"/> which includes whatever
    /// logic is required to serialize the parameters value into the HTTP request.
    /// For an example implementation which serializes the parameters as a JSON string, see <see cref="JsonEndpoint{TParameters, TResult}"/>.
    /// </para>
    /// <para>
    /// When deriving from this class, developers are strongly encouraged to set the <see cref="EndpointBase.Name"/> property to a human-readable
    /// name for this endpoint. This will improve the readability of reports.
    /// </para>
    /// <para>
    /// For more information, see the documentation article for <xref href="WebApisArticle?text=using+web+APIs"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="EndpointBase"/>
    /// <seealso cref="Endpoint"/>
    /// <seealso cref="Endpoint{TResult}"/>
    /// <seealso cref="ParameterizedEndpoint{TParameters}"/>
    /// <seealso cref="JsonEndpoint{TParameters}"/>
    /// <seealso cref="JsonEndpoint{TParameters, TResult}"/>
    /// <typeparam name="TParameters">The type of the parameters object which is required to create an HTTP request message</typeparam>
    /// <typeparam name="TResponse">The type of response that the endpoint is expected to return.</typeparam>
    public abstract class ParameterizedEndpoint<TParameters,TResponse> : EndpointBase
    {
        /// <summary>
        /// Gets a <see cref="HttpRequestMessageBuilder{TResponse}"/> from the state of the current instance
        /// and the specified parameters value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When overriding/implementing this method use <see cref="EndpointBase.GetBaseHttpRequestMessageBuilder{TResponse}()"/> to get a builder from the base class.
        /// You should then further customize that builder instance according to the appropriate logic for this implementation, such as to add the
        /// parameter value.
        /// </para>
        /// </remarks>
        /// <param name="parameters">The parameters required to create an HTTP request builder</param>
        /// <returns>An HTTP request message builder</returns>
        public abstract HttpRequestMessageBuilder<TResponse> GetHttpRequestMessageBuilder(TParameters parameters);
        
        /// <summary>
        /// Initializes a new instance of <see cref="ParameterizedEndpoint{TParameters,TResponse}"/> with a relative URI and an optional HTTP method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When setting the relative URI, avoid a leading forward-slash.  Prefer <c>myApp/doSomething</c> over <c>/myApp/doSomething</c>.
        /// </para>
        /// <para>
        /// If you omit the HTTP method, then the created builder will also not specify an HTTP method, which
        /// (if used to generate a request) will result in an HTTP <c>GET</c> request.  See <see cref="HttpRequestMessageBuilder.CreateRequestMessage()"/>.
        /// </para>
        /// </remarks>
        /// <param name="relativeUri">A relative URI string for the current endpoint.</param>
        /// <param name="method">An optional HTTP method.</param>
        protected ParameterizedEndpoint(string relativeUri, HttpMethod method = null) : base(relativeUri, method) {}
        
        /// <summary>
        /// Initializes a new instance of <see cref="ParameterizedEndpoint{TParameters,TResult}"/> with a URI and an optional HTTP method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you omit the HTTP method, then the created builder will also not specify an HTTP method, which
        /// (if used to generate a request) will result in an HTTP <c>GET</c> request.  See <see cref="HttpRequestMessageBuilder.CreateRequestMessage()"/>.
        /// </para>
        /// </remarks>
        /// <param name="uri">A URI for the current endpoint; this may be relative or absolute.</param>
        /// <param name="method">An optional HTTP method.</param>
        protected ParameterizedEndpoint(Uri uri, HttpMethod method = null) : base(uri, method) {}
    }
}