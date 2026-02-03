using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CSF.Screenplay
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="EnumerableResolutionAdapter{T}"/> to the DI container for the service type <typeparamref name="TService"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is required to work around a limitation of the BoDi DI container which ships with Reqnroll/SpecFlow.
        /// See <see cref="EnumerableResolutionAdapter{T}"/> for more information.
        /// </para>
        /// </remarks>
        /// <typeparam name="TService">The service type for which to add the adapter</typeparam>
        /// <param name="services">A service collection</param>
        /// <returns>The same service collection, so calls may be chained</returns>
        public static IServiceCollection AddEnumerableAdapter<TService>(this IServiceCollection services) where TService : class
            => services.AddTransient<IEnumerable<TService>, EnumerableResolutionAdapter<TService>>();

        /// <summary>
        /// Adds enumerable adapters for service types which are required in order to enable
        /// <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/options"> the Microsoft Options Pattern</see> with the specified options type.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is required to work around a limitation of the BoDi DI container which ships with Reqnroll/SpecFlow.
        /// See <see cref="EnumerableResolutionAdapter{T}"/> for more information.
        /// </para>
        /// <para>
        /// Use of the options pattern requires the resolution of three enumerable types: <see cref="IConfigureOptions{TOptions}"/>,
        /// <see cref="IPostConfigureOptions{TOptions}"/> and <see cref="IValidateOptions{TOptions}"/>.  This method uses
        /// <see cref="AddEnumerableAdapter{TService}(IServiceCollection)"/> for each of those types.
        /// </para>
        /// </remarks>
        /// <typeparam name="TOptions">The options type</typeparam>
        /// <param name="services">A service collection</param>
        /// <returns>The same service collection, so calls may be chained</returns>
        public static IServiceCollection AddOptionsAdapters<TOptions>(this IServiceCollection services) where TOptions : class
        {
            return services
                .AddEnumerableAdapter<IConfigureOptions<TOptions>>()
                .AddEnumerableAdapter<IPostConfigureOptions<TOptions>>()
                .AddEnumerableAdapter<IValidateOptions<TOptions>>();
        }

        /// <summary>
        /// Adds the services to DI which are required to use the Reqnroll/SpecFlow plugin.
        /// </summary>
        /// <param name="services">A service collection</param>
        /// <returns>The same service collection, so calls may be chained</returns>
        public static IServiceCollection AddScreenplayPlugin(this IServiceCollection services)
        {
            return services
                .AddScreenplay()
                .AddOptionsAdapters<ScreenplayOptions>()
                .AddSingleton<PerformanceProviderFactory>();
        }
    }
}