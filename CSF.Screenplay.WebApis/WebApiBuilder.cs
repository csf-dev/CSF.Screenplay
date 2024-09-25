namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// A builder for the performables <see cref="WebApis.SendTheHttpRequest"/> and <see cref="SendTheHttpRequestAndGetTheResponse{TResponse}"/>.
    /// </summary>
    public static class WebApiBuilder
    {
        /// <summary>
        /// Gets an instance of <see cref="WebApis.SendTheHttpRequest"/> from an endpoint and optional client name.
        /// </summary>
        /// <param name="endpoint">The endpoint from which to get an action.</param>
        /// <param name="clientName">An optional client name, for actors who must maintain multiple HTTP clients.</param>
        /// <returns>A performable that will send the HTTP request and await its result.</returns>
        public static SendTheHttpRequest SendTheHttpRequest(Endpoint endpoint,
                                                            string clientName = null)
            => new SendTheHttpRequest(endpoint.GetHttpRequestMessageBuilder(), clientName);

        /// <summary>
        /// Gets an instance of <see cref="SendTheHttpRequestAndGetTheResponse{TResponse}"/> from an endpoint and optional client name.
        /// </summary>
        /// <param name="endpoint">The endpoint from which to get an action.</param>
        /// <param name="clientName">An optional client name, for actors who must maintain multiple HTTP clients.</param>
        /// <returns>A performable that will send the HTTP request and await its result.</returns>
        public static SendTheHttpRequestAndGetTheResponse<TResponse> SendTheHttpRequest<TResponse>(Endpoint<TResponse> endpoint,
                                                                                                   string clientName = null)
            => new SendTheHttpRequestAndGetTheResponse<TResponse>(endpoint.GetHttpRequestMessageBuilder(), clientName);

        /// <summary>
        /// Gets an instance of <see cref="WebApis.SendTheHttpRequest"/> from a parameterized endpoint, its parameters and an optional client name.
        /// </summary>
        /// <param name="endpoint">The endpoint from which to get an action.</param>
        /// <param name="parameters">The parameters required by the endpoint.</param>
        /// <param name="clientName">An optional client name, for actors who must maintain multiple HTTP clients.</param>
        /// <returns>A performable that will send the HTTP request and await its result.</returns>
        public static SendTheHttpRequest SendTheHttpRequest<TParameters>(ParameterizedEndpoint<TParameters> endpoint,
                                                                         TParameters parameters,
                                                                         string clientName = null)
            => new SendTheHttpRequest(endpoint.GetHttpRequestMessageBuilder(parameters), clientName);

        /// <summary>
        /// Gets an instance of <see cref="SendTheHttpRequestAndGetTheResponse{TResponse}"/> from a parameterized endpoint, its parameters and an optional client name.
        /// </summary>
        /// <param name="endpoint">The endpoint from which to get an action.</param>
        /// <param name="parameters">The parameters required by the endpoint.</param>
        /// <param name="clientName">An optional client name, for actors who must maintain multiple HTTP clients.</param>
        /// <returns>A performable that will send the HTTP request and await its result.</returns>
        public static SendTheHttpRequestAndGetTheResponse<TResponse> SendTheHttpRequest<TParameters,TResponse>(ParameterizedEndpoint<TParameters,TResponse> endpoint,
                                                                                                               TParameters parameters,
                                                                                                               string clientName = null)
            => new SendTheHttpRequestAndGetTheResponse<TResponse>(endpoint.GetHttpRequestMessageBuilder(parameters), clientName);
    }
}