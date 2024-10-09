using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Abstract base class for types which represent web API endpoints.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An endpoint is broadly a description of a Web API URL and the corresponding HTTP method (aka "verb") such as GET or POST.
    /// Instances of endpoints (types which derive from this base class) are typically stored as static readonly values in Screenplay-based
    /// logic. Here, they can be reused by <xref href="TaskGlossaryItem?text=tasks"/> or other <xref href="PerformableGlossaryItem?text=performables"/>.
    /// </para>
    /// <para>
    /// Choose an appropriate implementation of this type based upon your intended use case.
    /// Endpoints which expect the API to return a strongly-typed result within the response body include a generic type parameter
    /// for the type of that result object.
    /// Also, endpoints which expect a request body: "parameterized endpoints" include a generic type parameter for the type of that request payload.
    /// There are also specialisations of these parameterized endpoint classes for those which expect an object serialized to JSON as their
    /// request payload.
    /// </para>
    /// <para>
    /// When writing custom endpoints, do not derive directly from this type.
    /// Usually one of the following pre-written endpoint types will be suitable for your use case.
    /// If not, pick the one which most closely matches your use-case and derive from that.
    /// </para>
    /// <list type="bullet">
    /// <item><description><see cref="Endpoint"/></description></item>
    /// <item><description><see cref="Endpoint{TResult}"/></description></item>
    /// <item><description><see cref="ParameterizedEndpoint{TParameters}"/></description></item>
    /// <item><description><see cref="ParameterizedEndpoint{TParameters, TResponse}"/></description></item>
    /// <item><description><see cref="JsonEndpoint{TParameters}"/></description></item>
    /// <item><description><see cref="JsonEndpoint{TParameters, TResult}"/></description></item>
    /// </list>
    /// <para>
    /// For more information, see the documentation article for <xref href="WebApisArticle?text=using+web+APIs"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="Endpoint"/>
    /// <seealso cref="Endpoint{TResult}"/>
    /// <seealso cref="ParameterizedEndpoint{TParameters}"/>
    /// <seealso cref="ParameterizedEndpoint{TParameters, TResponse}"/>
    /// <seealso cref="JsonEndpoint{TParameters}"/>
    /// <seealso cref="JsonEndpoint{TParameters, TResult}"/>
    public abstract class EndpointBase : IHasName
    {
        readonly HttpRequestMessageBuilder requestMessageBuilder;

        /// <summary>
        /// Gets the human-readable name of the current object.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see langword="null"/> is strongly discouraged here.  All types which implement <see cref="IHasName"/>
        /// should return a non-null response from this property.
        /// </para>
        /// <para>
        /// Where it comes to endpoints, it is normal that the human-readable name might be influenced by state which is held within
        /// the specific endpoint implementation.
        /// In these cases, developers are encouraged to override this property, providing a name which includes the relevant values.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// For an endpoint implementation which represents a GET request to a user profile API, this property could be overridden
        /// so that it includes the user ID of the user which is requested.
        /// </para>
        /// </example>
        public virtual string Name
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }

        /// <summary>
        /// Gets or sets an optional timeout duration for requests built from this endpoint.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this set to a non-<see langword="null" /> value, then the HTTP client used to make the request will include cancellation
        /// after an amount of time (equal to this timespan) has passed.
        /// This logic is handled within the <see cref="MakeWebApiRequests"/> action.  If this action is not used then this timeout might
        /// not be honoured.
        /// </para>
        /// <para>
        /// The logic for honouring this timeout is contained within the performables which are shipped with this library:
        /// </para>
        /// <list type="bullet">
        /// <item><description><see cref="SendTheHttpRequest"/></description></item>
        /// <item><description><see cref="SendTheHttpRequestAndGetTheResponse{TResponse}"/></description></item>
        /// <item><description><see cref="SendTheHttpRequestAndGetJsonResponse{TResponse}"/></description></item>
        /// </list>
        /// <para>
        /// If different performables are used to interact with the current endpoint then they must implement any
        /// timeout-related logic themselves, or else this value will not be honoured.
        /// </para>
        /// </remarks>
        public TimeSpan? Timeout
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }
        
        /// <summary>
        /// Gets a <see cref="HttpRequestMessageBuilder"/> from the state of the current instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Derived types should make use of this method to get the request message builder from the state of this base class.
        /// They may then further-customize the message builder according to their own logic.
        /// The message builder returned from this method will respect the following state from the endpoint.
        /// </para>
        /// <list type="bullet">
        /// <item><description>The request URI specified via the constructor</description></item>
        /// <item><description>The request method, if specified via the constructor</description></item>
        /// <item><description>The <see cref="Name"/> of this endpoint</description></item>
        /// <item><description>The <see cref="Timeout"/> for requests to this endpoint, if specified</description></item>
        /// </list>
        /// </remarks>
        /// <returns>An HTTP request message builder</returns>
        protected HttpRequestMessageBuilder GetBaseHttpRequestMessageBuilder()
#if NET5_0_OR_GREATER
            => requestMessageBuilder with { Name = Name, Timeout = Timeout };
#else
        {
            var output = requestMessageBuilder.Clone();
            output.Name = Name;
            output.Timeout = Timeout;
            return output;
        }
#endif

        /// <summary>
        /// Gets a <see cref="HttpRequestMessageBuilder{TResponse}"/> from the state of the current instance, with information about
        /// the expected response type.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Derived types which expect a strongly-typed response from the API should make use of this method to get the request message
        /// builder from the state of this base class.
        /// They may then further-customize the message builder according to their own logic.
        /// The message builder returned from this method will respect the following state from the endpoint.
        /// </para>
        /// <list type="bullet">
        /// <item><description>The request URI specified via the constructor</description></item>
        /// <item><description>The request method, if specified via the constructor</description></item>
        /// <item><description>The <see cref="Name"/> of this endpoint</description></item>
        /// <item><description>The <see cref="Timeout"/> for requests to this endpoint, if specified</description></item>
        /// </list>
        /// </remarks>
        /// <returns>An HTTP request message builder</returns>
        protected HttpRequestMessageBuilder<TResponse> GetBaseHttpRequestMessageBuilder<TResponse>()
            => GetBaseHttpRequestMessageBuilder().ToBuilderWithResponseType<TResponse>();

        /// <summary>
        /// Initializes a new instance of a type which derives from <see cref="EndpointBase"/> with a relative URI and an optional HTTP method.
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
        protected EndpointBase(string relativeUri, HttpMethod method = null) : this(new Uri(relativeUri, UriKind.Relative), method) {}
        
        /// <summary>
        /// Initializes a new instance of a type which derives from <see cref="EndpointBase"/> with a URI and an optional HTTP method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you omit the HTTP method, then the created builder will also not specify an HTTP method, which
        /// (if used to generate a request) will result in an HTTP <c>GET</c> request.  See <see cref="HttpRequestMessageBuilder.CreateRequestMessage()"/>.
        /// </para>
        /// </remarks>
        /// <param name="uri">A URI for the current endpoint; this may be relative or absolute.</param>
        /// <param name="method">An optional HTTP method.</param>
        protected EndpointBase(Uri uri, HttpMethod method = null)
        {
            requestMessageBuilder = new HttpRequestMessageBuilder
            {
                RequestUri = uri,
                Method = method,
            };
        }
    }
}