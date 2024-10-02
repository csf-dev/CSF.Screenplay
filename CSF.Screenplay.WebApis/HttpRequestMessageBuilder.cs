using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
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
        HttpRequestMessageBuilder : IHasName, Reporting.IFormattable
    {
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
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
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
        /// This property will always default to <c>1.1</c> regardless of the target framework.
        /// </para>
        /// </remarks>
        public Version Version
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        } = new Version(1, 1);

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

        /// <inheritdoc/>
        public string Name
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }

        /// <summary>
        /// Gets or sets an optional timeout duration for requests built from this builder.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this set to a non-<see langword="null" /> value, then the HTTP client used to make the request will include cancellation
        /// after an amount of time (equal to this timespan) has passed.
        /// This logic is handled within the <see cref="MakeWebApiRequests"/> action.  If this action is not used then this timeout might
        /// not be honoured.
        /// </para>
        /// </remarks>
        public TimeSpan? Timeout
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            set;
#endif
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Gets or sets the HTTP version policy, corresponding to <see cref="HttpRequestMessage.VersionPolicy"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this property is supported only for .NET 5 and above.  It is unavailable in lower .NET versions,
        /// including .NET Standard and .NET Framework.
        /// </para>
        /// </remarks>
        public HttpVersionPolicy VersionPolicy { get; init; }

        NameValueRecordCollection<string, object> options = new();

        /// <summary>
        /// Gets or sets the HTTP web request options, corresponding to <see cref="HttpRequestMessage.Options"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this property is supported only for .NET 5 and above.  It is unavailable in lower .NET versions,
        /// including .NET Standard and .NET Framework.
        /// </para>
        /// </remarks>
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
        /// <para>
        /// Note that this property is supported only for .NET versions below 5.  It is available for .NET Standard
        /// and .NET Framework but it is unavailable for target frameworks in which this is a <see langword="record" />
        /// type. Records come with their own cloning mechanism, in the form of non-destructive mutation.
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
                Name = Name,
                Timeout = Timeout,
            };
        }
#endif

        /// <summary>
        /// Creates and returns an HTTP request message.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The request message is typically created from the state of the current object instance.
        /// Multiple usages of this method must create a new request message each time.
        /// Unless some state has been altered between usages, though, each of these messages is likely to have
        /// the same data/property values.
        /// </para>
        /// <para>
        /// If the state of the current instance does not specify a <see cref="Method"/>; IE the method is <see langword="null" />,
        /// then <see cref="HttpMethod.Get"/> will be used.
        /// </para>
        /// </remarks>
        /// <returns>An HTTP request message</returns>
        public HttpRequestMessage CreateRequestMessage()
        {
            var message = new HttpRequestMessage(Method ?? HttpMethod.Get, RequestUri)
            {
                Content = Content,
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

        /// <inheritdoc/>
        public string Format() => Name ?? RequestUri.ToString();

        /// <summary>
        /// Gets a copy of the current request message builder instance, but with information about the expected type of response.
        /// </summary>
        /// <typeparam name="T">The expected type associated with the response from this message.</typeparam>
        /// <returns>A request message builder with response-type information.</returns>
        internal HttpRequestMessageBuilder<T> ToBuilderWithResponseType<T>()
        {
            return new HttpRequestMessageBuilder<T>
            {
                Content = Content,
                Method = Method,
                RequestUri = RequestUri,
                Version = Version,
                Name = Name,
                Timeout = Timeout,
#if NET5_0_OR_GREATER
                Headers = Headers,
                VersionPolicy = VersionPolicy,
                Options = Options,
#else
                Headers = Headers.Clone(),
#endif
            };
        }
    }
}