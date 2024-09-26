using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Represents a simple Web API endpoint; a URI and HTTP request method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type implements may be used to get an HTTP request message builder.
    /// It is intended to describe an endpoint to a web API.
    /// At its simplest this is a URL and optionally an HTTP method (aka "verb").
    /// </para>
    /// <para>
    /// Developers are free to create derived classes based upon this type, for endpoints which are more complex and require
    /// manipulation of other aspects of the <see cref="HttpRequestMessageBuilder"/> which is returned.
    /// Simply <see langword="override" /> the <see cref="GetHttpRequestMessageBuilder"/> method and configure the message builder
    /// as desired.
    /// </para>
    /// <para>
    /// When using or deriving from this class, developers are strongly encouraged to set the <see cref="EndpointBase.Name"/> property to a human-readable
    /// name for this endpoint. This will improve the readability of reports.
    /// </para>
    /// </remarks>
    /// <seealso cref="Endpoint{TResult}"/>
    public class Endpoint : EndpointBase
    {
        readonly HttpRequestMessageBuilder builder;

        /// <summary>
        /// Gets a <see cref="HttpRequestMessageBuilder"/> from the state of the current instance.
        /// </summary>
        /// <returns>An HTTP request message builder</returns>
        public virtual HttpRequestMessageBuilder GetHttpRequestMessageBuilder()
#if NET5_0_OR_GREATER
            => builder with { Name = Name, Timeout = Timeout };
#else
        {
            var output = builder.Clone();
            output.Name = Name;
            output.Timeout = Timeout;
            return output;
        }
#endif
        
        /// <summary>
        /// Initializes a new instance of <see cref="Endpoint"/> with a relative URI and an optional HTTP method.
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
        public Endpoint(string relativeUri, HttpMethod method = null) : this(new Uri(relativeUri, UriKind.Relative), method) {}
        
        /// <summary>
        /// Initializes a new instance of <see cref="Endpoint"/> with a URI and an optional HTTP method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you omit the HTTP method, then the created builder will also not specify an HTTP method, which
        /// (if used to generate a request) will result in an HTTP <c>GET</c> request.  See <see cref="HttpRequestMessageBuilder.CreateRequestMessage()"/>.
        /// </para>
        /// </remarks>
        /// <param name="uri">A URI for the current endpoint; this may be relative or absolute.</param>
        /// <param name="method">An optional HTTP method.</param>
        public Endpoint(Uri uri, HttpMethod method = null)
        {
            builder = new HttpRequestMessageBuilder
            {
                RequestUri = uri,
                Method = method,
            };
        }
    }
}