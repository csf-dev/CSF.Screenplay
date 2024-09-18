using System;
using System.Net.Http;

namespace CSF.Screenplay
{
    /// <summary>
    /// An object which can create an <see cref="HttpRequestMessage"/> for use with an HTTP client.
    /// </summary>
#if NET5_0_OR_GREATER
    public record HttpRequestMessageBuilder : ICreatesHttpRequestMessage
#else
    public class HttpRequestMessageBuilder : ICreatesHttpRequestMessage
#endif
    {
        /// <summary>
        /// Gets or sets a string which is the <see cref="Uri"/> to which the HTTP request shall be sent.
        /// </summary>
        public string RequestUri
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }

        /// <summary>
        /// Gets or sets the HTTP method (aka "verb") which shall be used to send the request.
        /// </summary>
        public HttpMethod Method
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }

        /// <inheritdoc/>
        HttpRequestMessage ICreatesHttpRequestMessage.CreateRequestMessage()
        {
            return new HttpRequestMessage(Method ?? HttpMethod.Get, RequestUri);
            // TODO: The other properties here!
        }
    }
}