using System.Net.Http;

namespace CSF.Screenplay
{
    /// <summary>
    /// An object which can create an <see cref="HttpRequestMessage"/> for use with an HTTP client.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Objects which build and return HTTP request messages are useful in frameworks such as Screenplay, because individual instances of
    /// request message must not be reused. Builder types which implement this interface may consistently create identical new request
    /// messages from the same source data, lowering the risk of accidentally reusing an old instance.
    /// </para>
    /// </remarks>
    public interface ICreatesHttpRequestMessage
    {
        /// <summary>
        /// Creates and returns an HTTP request message.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The request message is typically created from the state of the current object instance.
        /// Multiple usages of this method must create a new request message each time.
        /// Unless some state has been altered between usages, though, each of these messages is likely to have
        /// the same data/property values.
        /// </para>
        /// </remarks>
        /// <returns>An HTTP request message</returns>
        HttpRequestMessage CreateRequestMessage();
    }
}