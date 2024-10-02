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
        readonly string clientName;
        readonly HttpRequestMessageBuilder messageBuilder;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format(Resources.PerformableReportStrings.SendTheHttpRequestAndGetTheResponseFormat, actor, messageBuilder.Method, messageBuilder, typeof(TResponse).Name);
        
        /// <inheritdoc/>
        public async ValueTask<HttpResponseMessageAndResponseType<TResponse>> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<MakeWebApiRequests>();
            var httpResponse = await CommonHttpRequestLogic.SendRequestAsync(ability, messageBuilder, clientName, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();
            return new HttpResponseMessageAndResponseType<TResponse>(httpResponse);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SendTheHttpRequestAndGetTheResponse{TResponse}"/>.
        /// </summary>
        /// <param name="messageBuilder">The HTTP request message builder.</param>
        /// <param name="clientName">An optional client name, when actors must maintain more than one HTTP client.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="messageBuilder"/> is <see langword="null" />.</exception>
        public SendTheHttpRequestAndGetTheResponse(HttpRequestMessageBuilder<TResponse> messageBuilder, string clientName = null)
        {
            this.messageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            this.clientName = clientName;
        }
    }
}