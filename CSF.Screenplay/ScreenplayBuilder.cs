using System;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// A type which may be used to configure an instance of <see cref="Screenplay"/> prior to creating it.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="Screenplay"/> type is immutable once created, so this builder type permits configuring the Screenplay
    /// before its actual creation.
    /// </para>
    /// <para>
    /// The majority of configuration of the Screenplay is performed via Dependency Injection.  The constructor for this
    /// builder type accepts an <see cref="IServiceCollection"/> as a parameter.  If you explicitly specify a service collection
    /// in the constructor then you may pass an instance which has service descriptors already added.  These descriptors would
    /// typically be for types which assist in the resolution of <xref href="AbilityGlossaryItem?text=Abilitity+types"/> and
    /// <see cref="IPersona"/> types.
    /// In advanced usages, you might wish to add custom implementations of the following types to the service collection:
    /// </para>
    /// <list type="bullet">
    /// <item><description>The <see cref="ICast"/></description></item>
    /// <item><description>The <see cref="IStage"/></description></item>
    /// <item><description>The performance factory: <see cref="ICreatesPerformance"/></description></item>
    /// </list>
    /// <para>
    /// For more information, see <xref href="DependencyInjectionMainArticle?text=the+Dependency+Injection+article"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="Screenplay"/>
    /// <seealso cref="IServiceCollection"/>
    /// <seealso cref="IPersona"/>
    /// <seealso cref="ICast"/>
    /// <seealso cref="IStage"/>
    /// <seealso cref="ICreatesPerformance"/>
    /// <seealso cref="BuildScreenplay"/>
    public class ScreenplayBuilder
    {
        readonly IServiceCollection services;
        bool alreadyUsed;

        /// <summary>
        /// Builds and returns the <see cref="Screenplay"/> instance, and finalises the service collection associated with the current instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will not only build and return the Screenplay instance, it will also finalise the service collection, by
        /// executing <see cref="ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(IServiceCollection)"/> upon it.
        /// If you wish to add further services to the service collection, to be used by the Screenplay, then you must do so before
        /// using this method.
        /// </para>
        /// <para>
        /// Also take note that instances of <see cref="ScreenplayBuilder"/> are not reusable.  Calling this method a second time
        /// from the same builder instance will raise an exception.
        /// </para>
        /// </remarks>
        /// <returns>A Screenplay instance</returns>
        /// <exception cref="InvalidOperationException">If this method has already been used</exception>
        public Screenplay BuildScreenplay()
        {
            if (alreadyUsed)
                throw new InvalidOperationException($"Instances of {nameof(ScreenplayBuilder)} may only be used once; to create a new {nameof(Screenplay)}, create a new builder.");
            
            if(HasNoRegistrationFor<ICreatesPerformance>(services))
                services.AddTransient<ICreatesPerformance, PerformanceFactory>();
            
            if(HasNoRegistrationFor<ICast>(services))
                services.AddScoped<ICast>(s => new Cast(s, s.GetRequiredService<IPerformance>().PerformanceIdentity));
            
            if(HasNoRegistrationFor<IStage>(services))
                services.AddScoped<IStage, Stage>();

            AddStandardDiTypes();

            alreadyUsed = true;
            return new Screenplay(services);    
        }

        void AddStandardDiTypes()
        {
            services.AddSingleton<PerformanceEventBus>();
            services.AddTransient<IHasPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddTransient<IRelaysPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddScoped(s => s.GetRequiredService<ICreatesPerformance>().CreatePerformance());
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ScreenplayBuilder"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you specify a non-<see langword="null" /> service collection then you may use a service collection which contains
        /// your own dependency injection service descriptors.
        /// If you omit this parameter or specify <see langword="null" /> then a new/empty service collection will be used as
        /// the basis for the <see cref="Screenplay"/>.
        /// </para>
        /// </remarks>
        /// <param name="services">An optional service collection</param>
        public ScreenplayBuilder(IServiceCollection services = default)
        {
            this.services = services ?? new ServiceCollection();
        }

        static bool HasNoRegistrationFor<T>(IServiceCollection services) => services.All(x => x.ServiceType != typeof(T));
    }
}