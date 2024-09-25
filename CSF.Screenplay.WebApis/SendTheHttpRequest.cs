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
        readonly HttpRequestMessageBuilder messageBuilder;
        readonly string clientName;

        internal string Url { get; private set; }

        internal string MethodName { get; private set; }

        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor) => $"{actor.Name} sends an HTTP {MethodName} request to {Url}";

        /// <inheritdoc/>
        public async ValueTask<HttpResponseMessage> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<MakeWebApiRequests>();
            var client = clientName is null
                ? ability.DefaultClient
                : (ability[clientName] ?? throw new InvalidOperationException($"The actor must have an HTTP client for name '{clientName}'."));
            var request = messageBuilder.CreateRequestMessage();
            Url = request.RequestUri.ToString();
            MethodName = request.Method.ToString();
            return await client.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SendTheHttpRequest"/>.
        /// </summary>
        /// <param name="messageBuilder">The HTTP request message builder.</param>
        /// <param name="clientName">An optional client name, when actors must maintain more than one HTTP client.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="messageBuilder"/> is <see langword="null" />.</exception>
        public SendTheHttpRequest(HttpRequestMessageBuilder messageBuilder, string clientName = null)
        {
            this.messageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            this.clientName = clientName;
        }
    }
}