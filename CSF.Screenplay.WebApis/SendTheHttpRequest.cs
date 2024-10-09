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
        readonly HttpRequestMessageBuilder messageBuilder;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format(Resources.PerformableReportStrings.SendTheHttpRequestFormat, actor, messageBuilder.Method, messageBuilder);

        /// <inheritdoc/>
        public async ValueTask<HttpResponseMessage> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<MakeWebApiRequests>();
            return await CommonHttpRequestLogic.SendRequestAsync(ability, messageBuilder, clientName, cancellationToken);
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