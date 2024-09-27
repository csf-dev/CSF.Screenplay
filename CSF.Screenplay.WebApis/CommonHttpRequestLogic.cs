using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.WebApis
{
    internal static class CommonHttpRequestLogic
    {
        internal static Task<HttpResponseMessage> SendRequestAsync(MakeWebApiRequests ability, HttpRequestMessageBuilder requestBuilder, string clientName, CancellationToken cancellationToken)
        {
            var client = ability.GetClient(clientName);
            var request = requestBuilder.CreateRequestMessage();
            using (var sendTokenSource = GetCancellationTokenSource(requestBuilder, cancellationToken))
            {
                return client.SendAsync(request, sendTokenSource.Token);
            }
        }

        static CancellationTokenSource GetCancellationTokenSource(HttpRequestMessageBuilder requestBuilder,
                                                                  CancellationToken externalToken)
        {
            var timeoutToken = requestBuilder.Timeout != null ? new CancellationTokenSource(requestBuilder.Timeout.Value).Token : CancellationToken.None;
            return CancellationTokenSource.CreateLinkedTokenSource(externalToken, timeoutToken);
        }

        internal static TResult GetMessageBuilderWithJsonContent<TRequest,TResult>(TRequest requestPayload, TResult messageBuilder)
            where TResult : HttpRequestMessageBuilder
        {
            var content = JsonContent.Create(requestPayload);
#if NET5_0_OR_GREATER
            return messageBuilder with { Content = content };
#else
            var builder = messageBuilder.Clone();
            builder.Content = content;
            return builder;
#endif
        }
    }
}