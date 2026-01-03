using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// An action which creates &amp; sends an HTTP request, using the specification within an
    /// <see cref="HttpRequestMessageBuilder{TResponse}"/> and returns a strongly-typed result deserialized from
    /// a JSON HTTP response.
    /// </summary>
    public class SendTheHttpRequestAndGetJsonResponse<TResponse> : IPerformableWithResult<TResponse>, ICanReport
    {
        readonly string clientName;
        readonly HttpRequestMessageBuilder<TResponse> messageBuilder;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format(Resources.PerformableReportStrings.SendTheHttpRequestAndGetJsonResponseFormat, actor, typeof(TResponse).Name);

        /// <inheritdoc/>
        public async ValueTask<TResponse> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var httpResult = await actor.PerformAsync(new SendTheHttpRequestAndGetTheResponse<TResponse>(messageBuilder, clientName), cancellationToken);
            return await httpResult.ResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SendTheHttpRequestAndGetJsonResponse{TResponse}"/>.
        /// </summary>
        /// <param name="messageBuilder">The HTTP request message builder.</param>
        /// <param name="clientName">An optional client name, when actors must maintain more than one HTTP client.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="messageBuilder"/> is <see langword="null" />.</exception>
        public SendTheHttpRequestAndGetJsonResponse(HttpRequestMessageBuilder<TResponse> messageBuilder, string clientName = null)
        {
            this.messageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            this.clientName = clientName;
        }
    }
}