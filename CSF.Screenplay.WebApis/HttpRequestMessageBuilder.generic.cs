using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// An object which can create an <see cref="HttpRequestMessage"/> for use with an HTTP client,
    /// which is expected to expose a response of a particular type.
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
    /// <para>
    /// This difference between this type and its non-generic counterpart is that this type includes generic information about the
    /// expected response type. This can aid subsequent type-safety in consuming logic.
    /// </para>
    /// </remarks>
    /// <typeparam name="TResponse">The type of the response expected from the endpoint to which this request would be sent.</typeparam>
#if NET5_0_OR_GREATER
    public record
#else
    public class
#endif
#pragma warning disable S2326
// TResponse is unused in impl logic; it is used elsewhere for generic type inference,
// as it allows the developer to indicate their intentions.
        HttpRequestMessageBuilder<TResponse> : HttpRequestMessageBuilder
#pragma warning restore S2326
    {
#if !NET5_0_OR_GREATER
        /// <summary>Creates a clone of the current instance.</summary>
        /// <remarks>
        /// <para>
        /// This is mainly a shallow copy, although the <see cref="HttpRequestMessageBuilder.Headers"/> will be shallow-copied via
        /// <see cref="NameValueRecordCollection{TKey, TValue}.Clone()"/>.
        /// </para>
        /// <para>
        /// Note that this property is supported only for .NET versions below 5.  It is available for .NET Standard
        /// and .NET Framework but it is unavailable for target frameworks in which this is a <see langword="record" />
        /// type. Records come with their own cloning mechanism, in the form of non-destructive mutation.
        /// </para>
        /// </remarks>
        public new HttpRequestMessageBuilder<TResponse> Clone()
        {
            return new HttpRequestMessageBuilder<TResponse>
            {
                Content = Content,
                Method = Method,
                RequestUri = RequestUri,
                Version = Version,
                Headers = Headers.Clone(),
            };
        }
#endif
    }
}