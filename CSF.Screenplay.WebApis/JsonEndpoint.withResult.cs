using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Represents a Web API endpoint; a URI and HTTP request method and an expected result type for a strongly-typed JSON response, which
    /// accepts parameters in the format of a serialized JSON object in order to send a request.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type may be used to get an HTTP request message builder.
    /// It is intended to describe an endpoint to a web API.
    /// At its simplest this is a URL and optionally an HTTP method (aka "verb") and information about the expected type of the response.
    /// </para>
    /// <para>
    /// This type is very similar to <see cref="ParameterizedEndpoint{TParameters,TResult}"/>, except that both the parameters and
    /// result values are expected to be in JSON format.  The parameters value is typically serialized into the HTTP request body.
    /// </para>
    /// <para>
    /// Developers are free to create derived classes based upon this type, for endpoints which are more complex and require
    /// manipulation of other aspects of the <see cref="HttpRequestMessageBuilder"/> which is returned.
    /// Simply <see langword="override" /> the <see cref="Endpoint.GetHttpRequestMessageBuilder"/> method and configure the message builder
    /// as desired.
    /// </para>
    /// <para>
    /// When using or deriving from this class, developers are strongly encouraged to set the <see cref="EndpointBase.Name"/> property to a human-readable
    /// name for this endpoint. This will improve the readability of reports.
    /// </para>
    /// </remarks>
    /// <typeparam name="TParameters">The type of parameters value which should be sent with the request body.</typeparam>
    /// <typeparam name="TResult">The type of response that the endpoint is expected to return.</typeparam>
    /// <seealso cref="Endpoint"/>
    /// <seealso cref="JsonEndpoint{TResult}"/>
    public class JsonEndpoint<TParameters,TResult> : ParameterizedEndpoint<TParameters,TResult>
    {
        /// <inheritdoc/>
        public override HttpRequestMessageBuilder<TResult> GetHttpRequestMessageBuilder(TParameters parameters)
            => CommonHttpRequestLogic.GetMessageBuilderWithJsonContent(parameters, Builder);

        /// <summary>
        /// Initializes a new instance of <see cref="JsonEndpoint{TParameters,TResult}"/> with a relative URI and an optional HTTP method.
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
        /// Initializes a new instance of <see cref="JsonEndpoint{TParameters,TResult}"/> with a URI and an optional HTTP method.
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