using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// An action which creates &amp; sends an HTTP request using the specification within an
    /// <see cref="HttpRequestMessageBuilder"/>.
    /// </summary>
    public class SendTheHttpRequest : IPerformableWithResult<HttpResponseMessage>, ICanReport
    {
        readonly string clientName;

        internal HttpRequestMessageBuilder MessageBuilder { get; }

        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor) => $"{actor.Name} sends an HTTP {MessageBuilder.Method} request to {MessageBuilder.Name ?? MessageBuilder.RequestUri.ToString()}";

        /// <inheritdoc/>
        public async ValueTask<HttpResponseMessage> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<MakeWebApiRequests>();
            var client = clientName is null
                ? ability.DefaultClient
                : (ability[clientName] ?? throw new InvalidOperationException($"The actor must have an HTTP client for name '{clientName}'."));
            var request = MessageBuilder.CreateRequestMessage();
            var sendToken = GetCancellationToken(cancellationToken);
            return await client.SendAsync(request, sendToken);
        }

        CancellationToken GetCancellationToken(CancellationToken externalToken)
        {
            var timeoutToken = MessageBuilder.Timeout != null ? new CancellationTokenSource(MessageBuilder.Timeout.Value).Token : CancellationToken.None;
            var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(externalToken, timeoutToken);
            return linkedSource.Token;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SendTheHttpRequest"/>.
        /// </summary>
        /// <param name="messageBuilder">The HTTP request message builder.</param>
        /// <param name="clientName">An optional client name, when actors must maintain more than one HTTP client.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="messageBuilder"/> is <see langword="null" />.</exception>
        public SendTheHttpRequest(HttpRequestMessageBuilder messageBuilder, string clientName = null)
        {
            MessageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            this.clientName = clientName;
        }
    }
}