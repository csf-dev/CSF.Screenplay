using System.Net.Http;

namespace CSF.Screenplay
{
    /// <summary>
    /// An object which can create an <see cref="HttpRequestMessage"/> for use with an HTTP client.
    /// </summary>
    public interface ICreatesHttpRequestMessage
    {
        /// <summary>
        /// Creates and returns an HTTP request message.
        /// </summary>
        /// <returns>An HTTP request message</returns>
        HttpRequestMessage CreateRequestMessage();
    }
}