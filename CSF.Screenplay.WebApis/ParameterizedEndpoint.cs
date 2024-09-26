using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Represents a Web API endpoint; a URI and HTTP request method, which requires parameters in order to send a request.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type is intended to describe an endpoint to a web API.
    /// This could be just a URL, optionally an HTTP method (aka "verb") and some mechanism by which parameter values are serialized into
    /// the request.
    /// </para>
    /// <para>
    /// This kind of endpoint cannot generate an HTTP request without some strongly-typed per-invocation parameters.
    /// For an endpoint which does not require parameters, use or derive-from the simpler <see cref="Endpoint"/> class instead.
    /// </para>
    /// <para>
    /// Developers are free to create derived classes based upon this type, for endpoints which are more complex and require
    /// manipulation of other aspects of the <see cref="HttpRequestMessageBuilder"/> which is returned.
    /// Simply <see langword="override" /> the <see cref="GetHttpRequestMessageBuilder"/> method and configure the message builder
    /// as desired.
    /// </para>
    /// </remarks>
    /// <typeparam name="TParameters">The type of the parameters object which is required to create an HTTP request message</typeparam>
    public abstract class ParameterizedEndpoint<TParameters>
    {
        /// <summary>
        /// Gets the HTTP request message builder for the current instance, which has been set up by the constructor.
        /// </summary>
        protected HttpRequestMessageBuilder Builder { get; }

        /// <summary>
        /// Gets a <see cref="HttpRequestMessageBuilder"/> from the state of the current instance
        /// and the specified parameters value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When overriding/implementing this method, do not mutate the <see cref="Builder"/> which is stored within the current instance,
        /// instead copy it using non-destructive mutation and return the mutated copy.
        /// </para>
        /// </remarks>
        /// <param name="parameters">The parameters required to create an HTTP request builder</param>
        /// <returns>An HTTP request message builder</returns>
        public abstract HttpRequestMessageBuilder GetHttpRequestMessageBuilder(TParameters parameters);
        
        /// <summary>
        /// Initializes a new instance of <see cref="ParameterizedEndpoint{TParameters}"/> with a relative URI and an optional HTTP method.
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
        /// Initializes a new instance of <see cref="ParameterizedEndpoint{TParameters}"/> with a URI and an optional HTTP method.
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
            Builder = new HttpRequestMessageBuilder
            {
                RequestUri = uri,
                Method = method,
            };
        }
    }
}