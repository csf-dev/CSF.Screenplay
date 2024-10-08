using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// An ability class which allows Actors to make HTTP web API requests using <see cref="HttpClient"/>.
    /// </summary>
    public sealed class MakeWebApiRequests : IDisposable
    {
        const string DefaultClientKey = nameof(DefaultClient);

        readonly Dictionary<string, HttpClient> clients = new Dictionary<string, HttpClient>();

        /// <summary>
        /// Gets or sets an HTTP client with a specified name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this indexer if an actor needs to manage more than one HTTP client.  The key to this index - a name - is
        /// completely arbitrary and may mean whatever your needs require.  Names must not be <see langword="null" /> and should
        /// not be empty or whitespace-only strings.
        /// </para>
        /// <para>
        /// The name <c>DefaultClient</c> is reserved for the value of <see cref="DefaultClient"/>.  If the actor needs to use only a single
        /// HTTP client then yoy may find the DefaultClient property easier to use, instead of this indexer.
        /// </para>
        /// </remarks>
        /// <param name="name">The name of the HTTP client for which this instance is getting or setting.</param>
        /// <returns>An HTTP client associated with the specified name, or <see langword="null" /> reference if no HTTP client has been configured for that name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null" />.</exception>
        public HttpClient this[string name]
        {
            get
            {
                if (name is null) throw new ArgumentNullException(nameof(name));
                return clients.ContainsKey(name) ? clients[name] : null;
            }
            set
            {
                if (name is null) throw new ArgumentNullException(nameof(name));
                if (value is null) clients.Remove(name);
                else clients[name] = value;
            }
        }

        /// <summary>
        /// Gets or sets the HTTP client, for scenarios in which the actor needs to use a single client.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the actor needs to know about more than one HTTP client, then consider using the indexer instead, which allows storage of more than one
        /// client, using arbitrary names.
        /// </para>
        /// </remarks>
        public HttpClient DefaultClient
        {
            get => this[DefaultClientKey];
            set => this[DefaultClientKey] = value;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach (var client in clients.Values)
                client.Dispose();
        }

        /// <summary>
        /// Gets an HTTP client with the specified name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <paramref name="clientName"/> is <see langword="null" /> then the <see cref="DefaultClient"/> will be returned.
        /// </para>
        /// </remarks>
        /// <param name="clientName">An optional client name, if omitted then the default client will be returned.</param>
        /// <returns>An HTTP client</returns>
        /// <exception cref="InvalidOperationException">If <paramref name="clientName"/> is specified &amp; non-<see langword="null" /> but the current instance has no client of that name.</exception>
        public HttpClient GetClient(string clientName = null)
        {
            return clientName is null
                ? DefaultClient
                : (this[clientName] ?? throw new InvalidOperationException($"The actor must have an HTTP client for name '{clientName}'."));
        }

        /// <summary>
        /// Adds a new HTTP client to the current instance, with an optional base URI and client name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is a shorthand for adding a new item to either <see cref="DefaultClient"/> or the class indexer.
        /// It allows the convenient configuration of a new <see cref="HttpClient"/> with just a base URI to which requests should be sent.
        /// </para>
        /// </remarks>
        /// <param name="baseUri">Optional, the base URI to which requests should be sent</param>
        /// <param name="clientName">Optional, the name of the client. If omitted then the new client will be the <see cref="DefaultClient"/></param>
        public void AddClient(string baseUri = null, string clientName = null)
        {
            var client = new HttpClient();

            if (!string.IsNullOrEmpty(baseUri))
                client.BaseAddress = new Uri(baseUri);

            if (clientName is null) DefaultClient = client;
            else this[clientName] = client;
        }
    }
}