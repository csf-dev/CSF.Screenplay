using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BoDi;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// Adapter class which allows a SpecFlow/BoDi <c>IObjectContainer</c> to be used as an <see cref="IServiceCollection"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This adapter class is highly limited and supports only a tiny subset of <see cref="IServiceCollection"/>'s functionality. For
    /// many methods, and the indexer for <see cref="ServiceDescriptor"/>, it will raise <see cref="NotSupportedException"/>.
    /// The only functionality supported is <see cref="IsReadOnly"/> and <see cref="Add(ServiceDescriptor)"/>.
    /// </para>
    /// </remarks>
    public class ServiceCollectionAdapter : IServiceCollection
    {
        const string NotSupportedMessage = "This method is not supported for ServiceCollectionAdapter; a BoDi object container is not a true service collection.";

// Suppress SonarQube issue S3011, I'm using BindingFlags.NonPublic here but only from internally within the same class.
// The technique calling for reflection is to make an open generic method runtime-generic instead of compile-time. It's
// not actually bypassing accessibility.
#pragma warning disable S3011
        static readonly MethodInfo
            OpenGenericRegisterType = typeof(ServiceCollectionAdapter).GetMethod(nameof(RegisterType), BindingFlags.Instance | BindingFlags.NonPublic),
            OpenGenericRegisterInstance = typeof(ServiceCollectionAdapter).GetMethod(nameof(RegisterInstance), BindingFlags.Instance | BindingFlags.NonPublic),
            OpenGenericRegisterFactory = typeof(ServiceCollectionAdapter).GetMethod(nameof(RegisterFactory), BindingFlags.Instance | BindingFlags.NonPublic);
#pragma warning restore S3011

        readonly IObjectContainer wrapped;

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public ServiceDescriptor this[int index]
        {
            get => throw new NotSupportedException("This indexer is not supported for this adapter.");
            set => throw new NotSupportedException("This indexer is not supported for this adapter.");
        }

        /// <summary>
        /// Not supported, always returns zero.
        /// </summary>
        public int Count => default;

        /// <summary>
        /// Partially supported, always returns <see langword="false" />
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Partially-supported, will add the specified service descriptor to the current object container.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method supports only transient or singleton services. If the <see cref="ServiceDescriptor.Lifetime"/> present on the item specified
        /// is <see cref="ServiceLifetime.Scoped"/> then the service descriptor will not be added and will be silently ignored.
        /// </para>
        /// <para>
        /// In reality, the Specflow BoDi object container only really supports singleton services at this level. So, any services added as
        /// <see cref="ServiceLifetime.Transient"/> will actually become singletons here. Whilst the BoDi container does support scoped services, they
        /// must be added directly to the scope instance and cannot be added in advance.
        /// </para>
        /// </remarks>
        /// <param name="item">The service descriptor</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">If the <paramref name="item"/> has a <see langword="null" /> <see cref="ServiceDescriptor.ServiceType"/>.</exception>
        public void Add(ServiceDescriptor item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (item.ServiceType is null)
                throw new ArgumentException("The service type must not be null", nameof(item));
            if (item.Lifetime == ServiceLifetime.Scoped) return;

            if (item.ImplementationFactory != null)
                OpenGenericRegisterFactory.MakeGenericMethod(item.ServiceType).Invoke(this, new[] { item });
            else if (item.ImplementationInstance != null)
                OpenGenericRegisterInstance.MakeGenericMethod(item.ServiceType).Invoke(this, new[] { item });
            else
                OpenGenericRegisterType.MakeGenericMethod(item.ServiceType, item.ImplementationType).Invoke(this, Array.Empty<object>());
        }

        void RegisterType<TSvc,TImpl>() where TImpl : class,TSvc
        {
            wrapped.RegisterTypeAs<TImpl, TSvc>();
        }

        void RegisterInstance<T>(ServiceDescriptor item) where T : class
        {
            wrapped.RegisterInstanceAs((T) item.ImplementationInstance);
        }

        void RegisterFactory<T>(ServiceDescriptor item) where T : class
        {
            wrapped.RegisterFactoryAs(objectContainer => (T) item.ImplementationFactory(new ServiceProviderAdapter(objectContainer)));
        }

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public void Clear() => throw new NotSupportedException();

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public bool Contains(ServiceDescriptor item) => throw new NotSupportedException(NotSupportedMessage);

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public void CopyTo(ServiceDescriptor[] array, int arrayIndex) => throw new NotSupportedException(NotSupportedMessage);

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public IEnumerator<ServiceDescriptor> GetEnumerator() => throw new NotSupportedException(NotSupportedMessage);

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public int IndexOf(ServiceDescriptor item) => throw new NotSupportedException(NotSupportedMessage);

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public void Insert(int index, ServiceDescriptor item) => throw new NotSupportedException(NotSupportedMessage);

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public bool Remove(ServiceDescriptor item) => throw new NotSupportedException(NotSupportedMessage);

        /// <summary>
        /// Not supported; always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public void RemoveAt(int index) => throw new NotSupportedException(NotSupportedMessage);

        IEnumerator IEnumerable.GetEnumerator() => throw new NotSupportedException(NotSupportedMessage);
        
        /// <summary>
        /// Initialises an instance of <see cref="ServiceCollectionAdapter"/>.
        /// </summary>
        /// <param name="wrapped">The BoDi object container</param>
        /// <exception cref="ArgumentNullException">If <paramref name="wrapped"/> is <see langword="null" />.</exception>
        public ServiceCollectionAdapter(IObjectContainer wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}