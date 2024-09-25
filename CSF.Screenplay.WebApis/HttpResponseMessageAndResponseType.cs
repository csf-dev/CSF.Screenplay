using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Wraps an <see cref="HttpResponseMessage"/> but also provides information about the expected response type
    /// from that message.
    /// </summary>
    /// <typeparam name="TResponse">The expected response type.</typeparam>
    public class HttpResponseMessageAndResponseType<TResponse>
    {
        /// <summary>
        /// Gets the HTTP response message.
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpResponseMessageAndResponseType{TResponse}"/>.
        /// </summary>
        /// <param name="responseMessage">The HTTP response message.</param>
        /// <exception cref="System.ArgumentNullException">If <paramref name="responseMessage"/> is <see langword="null" />.</exception>
        public HttpResponseMessageAndResponseType(HttpResponseMessage responseMessage)
        {
            ResponseMessage = responseMessage ?? throw new System.ArgumentNullException(nameof(responseMessage));
        }
    }
}