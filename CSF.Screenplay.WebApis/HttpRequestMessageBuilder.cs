using System;
using System.Net.Http;

namespace CSF.Screenplay
{
    /// <summary>
    /// An object which can create an <see cref="HttpRequestMessage"/> for use with an HTTP client.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Please note that this type differs in form depending upon the .NET version under which it is consumed.
    /// If consuming this type from logic which targets .NET Standard 2.0 or .NET Framework 4.6.2 then this type is a <see langword="class" />.
    /// In that scenario its properties are mutable and the developer should take care to ensure that they do not mutate/alter its
    /// state inadvertently. Developers should ensure that they manually copy the state from the current instance into a new instance
    /// instead of modifying an existing instance.
    /// In these target frameworks, a <c>Clone</c> method has been provided to assist with this.
    /// </para>
    /// <para>
    /// When consuming this from .NET 5 or higher, this type is instead a <see langword="record" /> and is immutable by design.
    /// All properties are init-only. Additionally, in .NET 5+, some additional properties are available, supporting features of
    /// <see cref="HttpRequestMessage"/> which are unavailable in lower .NET versions.
    /// When using .NET 5, developers may use nondestructive mutation with the <see langword="with" /> keyword/expression to create a copy of
    /// the current instance but with some differences.
    /// </para>
    /// </remarks>
#if NET5_0_OR_GREATER
    public record
#else
    public class
#endif
        HttpRequestMessageBuilder : ICreatesHttpRequestMessage
    {
        HttpMethod method = HttpMethod.Get;
        Version version = new Version(1, 1);
        NameValueRecordCollection<string,string> headers = new NameValueRecordCollection<string, string>();

        /// <summary>
        /// Gets or sets the <see cref="Uri"/> to which the HTTP request shall be sent.
        /// </summary>
        public Uri RequestUri
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }

        /// <summary>
        /// Gets or sets the HTTP method (aka "verb") which shall be used to send the request.
        /// </summary>
        public HttpMethod Method
        {
            get => method;
#if NET5_0_OR_GREATER
            init
#else
            set
#endif
                => method = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the content which will be sent with the request: the request body.
        /// </summary>
        public HttpContent Content
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }
        
        /// <summary>
        /// Gets or sets the HTTP version which shall be used by the message.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The documentation for the <see cref="HttpRequestMessage.Version"/> property notes that it would default to <c>2.0</c> for
        /// .NET Core 2.1 or 2.2, and defaults to <c>1.1</c> for all other versions of .NET or .NET Framework.
        /// This property will always default to <c>1.1</c> regardless.
        /// </para>
        /// </remarks>
        public Version Version
        {
            get => version;
#if NET5_0_OR_GREATER
            init
#else
            set
#endif
                => version = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the HTTP headers which will be sent with the request, corresponding to <see cref="HttpRequestMessage.Headers"/>.
        /// </summary>
        public NameValueRecordCollection<string,string> Headers
        {
            get => headers;
#if NET5_0_OR_GREATER
            init
#else
            set
#endif
                => headers = value ?? throw new ArgumentNullException(nameof(value));
        }



#if NET5_0_OR_GREATER
        /// <summary>
        /// Gets or sets the HTTP version policy, corresponding to <see cref="HttpRequestMessage.VersionPolicy"/>.
        /// </summary>
        public HttpVersionPolicy VersionPolicy { get; init; }

        NameValueRecordCollection<string, object> options = new();

        /// <summary>
        /// Gets or sets the HTTP web request options, corresponding to <see cref="HttpRequestMessage.Options"/>.
        /// </summary>
        public NameValueRecordCollection<string,object> Options
        {
            get => options;
            init => options = value ?? throw new ArgumentNullException(nameof(value));
        }
#endif

#if !NET5_0_OR_GREATER
        /// <summary>Creates a clone of the current instance.</summary>
        /// <remarks>
        /// <para>
        /// This is mainly a shallow copy, although the <see cref="Headers"/> will be shallow-copied via
        /// <see cref="NameValueRecordCollection{TKey, TValue}.Clone()"/>.
        /// </para>
        /// </remarks>
        public HttpRequestMessageBuilder Clone()
        {
            return new HttpRequestMessageBuilder
            {
                Content = Content,
                Method = Method,
                RequestUri = RequestUri,
                Version = Version,
                Headers = Headers.Clone(),
            };
        }
#endif

        /// <inheritdoc/>
        public HttpRequestMessage CreateRequestMessage()
        {
            var message = new HttpRequestMessage(Method ?? HttpMethod.Get, RequestUri)
            {
                Content = Content,
                Method = Method,
                RequestUri = RequestUri,
                Version = Version,
#if NET5_0_OR_GREATER
                VersionPolicy = VersionPolicy,
#endif
            };

            foreach(var headerKvp in Headers)
                message.Headers.Add(headerKvp.Key, headerKvp.Value);

#if NET5_0_OR_GREATER
            foreach (var optionKvp in Options)
                ((System.Collections.Generic.IDictionary<string,object>) message.Options).Add(optionKvp.Key, optionKvp.Value);
#endif

            return message;
        }
    }
}