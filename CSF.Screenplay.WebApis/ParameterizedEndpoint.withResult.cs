using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Represents a Web API endpoint; a URI and HTTP request method, which requires parameters in order to send a request
    /// and which has a generic expected return type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type is intended to describe an endpoint to a web API.
    /// This could be just a URL, optionally an HTTP method (aka "verb"), mechanism by which parameter values are serialized into
    /// the request and information about the expected type of the response.
    /// </para>
    /// <para>
    /// This kind of endpoint cannot generate an HTTP request without some strongly-typed per-invocation parameters.
    /// For an endpoint which does not require parameters, use or derive-from the simpler <see cref="Endpoint"/> class instead.
    /// </para>
    /// <para>
    /// This type is very similar to <see cref="ParameterizedEndpoint{TParameters}"/>, except that it is designed to be used for
    /// an endpoint which returns a strongly-typed response of some kind, where <typeparamref name="TResponse"/> is the type of that
    /// response.
    /// </para>
    /// <para>
    /// Developers are free to create derived classes based upon this type, for endpoints which are more complex and require
    /// manipulation of other aspects of the <see cref="HttpRequestMessageBuilder"/> which is returned.
    /// Simply <see langword="override" /> the <see cref="GetHttpRequestMessageBuilder"/> method and configure the message builder
    /// as desired.
    /// </para>
    /// <para>
    /// When deriving from this class, developers are strongly encouraged to set the <see cref="EndpointBase.Name"/> property to a human-readable
    /// name for this endpoint. The same goes for overriding <see cref="GetHttpRequestMessageBuilder(TParameters)"/>; the
    /// <see cref="HttpRequestMessageBuilder"/> returned should use that same name as its own name. This will improve the readability of reports.
    /// </para>
    /// </remarks>
    /// <typeparam name="TParameters">The type of the parameters object which is required to create an HTTP request message</typeparam>
    /// <typeparam name="TResponse">The type of response that the endpoint is expected to return.</typeparam>
    public abstract class ParameterizedEndpoint<TParameters,TResponse> : EndpointBase
    {
        /// <summary>
        /// Gets the HTTP request message builder for the current instance, which has been set up by the constructor.
        /// </summary>
        protected HttpRequestMessageBuilder<TResponse> Builder { get; }

        /// <summary>
        /// Gets a <see cref="HttpRequestMessageBuilder{TResponse}"/> from the state of the current instance
        /// and the specified parameters value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When overriding/implementing this method, do not mutate the <see cref="Builder"/> which is stored within the current instance,
        /// instead copy it using non-destructive mutation and return the mutated copy.
        /// </para>
        /// <para>
        /// When implementing this method, do not forget to include the values of both the <see cref="EndpointBase.Name"/> and <see cref="EndpointBase.Timeout"/>
        /// properties in the request builder which is returned.
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
        protected ParameterizedEndpoint(string relativeUri, HttpMethod method = null) : this(new Uri(relativeUri, UriKind.Relative), method) {}
        
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
        protected ParameterizedEndpoint(Uri uri, HttpMethod method = null)
        {
            Builder = new HttpRequestMessageBuilder<TResponse>
            {
                RequestUri = uri,
                Method = method,
            };
        }
    }
}