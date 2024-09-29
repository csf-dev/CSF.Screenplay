using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Base type for a Web API endpoint which accepts a request payload in the form of a strongly typed object serialized to JSON string,
    /// but which is not expected to return a response body.
    /// </summary>
    /// <remarks>
    /// <para>
    /// There are several concrete types of endpoint available, all of which derive from <see cref="EndpointBase"/>, for more information
    /// about the purpose of endpoints and how they are used, see the documentation for that base type.
    /// </para>
    /// <para>
    /// Developers are welcome to create specialized derived types based upon this or other subclasses of <see cref="EndpointBase"/> if they
    /// have specific needs. Derived classes should <see langword="override"/> <see cref="GetHttpRequestMessageBuilder"/> with a method that
    /// calls the base implementation and then further manipulates the message builder before returning it.
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
    /// <seealso cref="ParameterizedEndpoint{TParameters, TResponse}"/>
    /// <seealso cref="ParameterizedEndpoint{TParameters}"/>
    /// <seealso cref="JsonEndpoint{TParameters, TResult}"/>
    /// <typeparam name="TParameters">The type of the parameters object which is required to create an HTTP request message</typeparam>
    public class JsonEndpoint<TParameters> : ParameterizedEndpoint<TParameters>
    {
        /// <summary>
        /// Gets a <see cref="HttpRequestMessageBuilder"/> from the state of the current instance
        /// and the specified parameters value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method serializes the <paramref name="parameters"/> value into a JSON string and sets it into the HTTP request content:
        /// <see cref="HttpRequestMessageBuilder.Content"/>.
        /// </para>
        /// </remarks>
        /// <param name="parameters">The parameters required to create an HTTP request builder</param>
        /// <returns>An HTTP request message builder</returns>
        public override HttpRequestMessageBuilder GetHttpRequestMessageBuilder(TParameters parameters)
            => CommonHttpRequestLogic.GetMessageBuilderWithJsonContent(parameters, GetBaseHttpRequestMessageBuilder());

        /// <summary>
        /// Initializes a new instance of <see cref="JsonEndpoint{TParameters}"/> with a relative URI and an optional HTTP method.
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
        public JsonEndpoint(string relativeUri, HttpMethod method = null) : base(relativeUri, method) {}
        
        /// <summary>
        /// Initializes a new instance of <see cref="JsonEndpoint{TParameters}"/> with a URI and an optional HTTP method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you omit the HTTP method, then the created builder will also not specify an HTTP method, which
        /// (if used to generate a request) will result in an HTTP <c>GET</c> request.  See <see cref="HttpRequestMessageBuilder.CreateRequestMessage()"/>.
        /// </para>
        /// </remarks>
        /// <param name="uri">A URI for the current endpoint; this may be relative or absolute.</param>
        /// <param name="method">An optional HTTP method.</param>
        public JsonEndpoint(Uri uri, HttpMethod method = null) : base(uri, method) {}
    }
}