using System;

namespace CSF.Screenplay.WebApis
{
    /// <summary>
    /// Abstract base class for types which represent web API endpoints.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When writing custom endpoint types, do not derive directly from this type.
    /// Instead, derive from one of <see cref="Endpoint"/>, <see cref="Endpoint{TResult}"/>, <see cref="ParameterizedEndpoint{TParameters}"/>
    /// or <see cref="ParameterizedEndpoint{TParameters, TResponse}"/>, depending upon whether or not your endpoint requires parameters
    /// and/or whether it returns a strongly-typed response.
    /// </para>
    /// </remarks>
    public abstract class EndpointBase : IHasName
    {
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
        /// Gets or sets an optional timeout duration for requests built from this endpoint.
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
    }
}