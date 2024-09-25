using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// An action which creates &amp; sends an HTTP request using the specification within an
    /// <see cref="HttpRequestMessageBuilder{TResponse}"/> and returns a result which preserves the intended
    /// response type.
    /// </summary>
    public class SendTheHttpRequestAndGetTheResponse<TResponse> : IPerformableWithResult<HttpResponseMessageAndResponseType<TResponse>>, ICanReport
    {
        readonly SendTheHttpRequest untypedSender;

        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor)
            => $"{actor.Name} sends an HTTP {untypedSender.MethodName} request to {untypedSender.Url} and reads the response as {typeof(TResponse).Name}";

        /// <inheritdoc/>
        public async ValueTask<HttpResponseMessageAndResponseType<TResponse>> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var untypedResult = await untypedSender.PerformAsAsync(actor, cancellationToken);
            return new HttpResponseMessageAndResponseType<TResponse>(untypedResult);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SendTheHttpRequestAndGetTheResponse{TResponse}"/>.
        /// </summary>
        /// <param name="messageBuilder">The HTTP request message builder.</param>
        /// <param name="clientName">An optional client name, when actors must maintain more than one HTTP client.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="messageBuilder"/> is <see langword="null" />.</exception>
        public SendTheHttpRequestAndGetTheResponse(HttpRequestMessageBuilder<TResponse> messageBuilder, string clientName = null)
        {
            untypedSender = new SendTheHttpRequest(messageBuilder, clientName);
        }
    }
}