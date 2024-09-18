using System;
using System.Collections;
using System.Collections.Generic;

#if NET5_0_OR_GREATER
#nullable enable
#endif

namespace CSF.Screenplay
{
    /// <summary>
    /// A simple name/value collection with an indexer, backed by a <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Please note that this type differs in form depending upon the .NET version under which it is consumed.
    /// If consuming this type from logic which targets .NET Standard 2.0 or .NET Framework 4.6.2 then this type is mutable.
    /// In that scenario the developer should take care to ensure that they do not mutate/alter its
    /// state inadvertently. Developers should ensure that they manually copy the state from the current instance into a new instance
    /// instead of modifying an existing instance.
    /// In these target frameworks, a <c>Clone</c> method has been provided to assist with this.
    /// </para>
    /// <para>
    /// When consuming this from .NET 5 or higher, this type is immutable by design; the indexer is init-only.
    /// When using .NET 5, developers may use nondestructive mutation by using either the <c>WithItem</c> or <c>WithItems</c> methods
    /// to create a copy of the current instance, but with modified items.
    /// </para>
    /// </remarks>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public sealed class NameValueRecordCollection<TKey,TValue> : IEnumerable<KeyValuePair<TKey,TValue>>, IEquatable<NameValueRecordCollection<TKey,TValue>>
        where TValue : class
#if NET5_0_OR_GREATER
        where TKey : notnull
#endif
    {
        readonly Dictionary<TKey, TValue> data;

        /// <summary>
        /// Gets or sets the values within this collection, via an indexer.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see langword="null" /> may not be stored in this collection as a value. An attempt to store a null value will result in the removal
        /// of the item at the specified key.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In order to set this property, particularly when using .NET 5 or higher (when this type is a <c>record</c> rather than a class,
        /// and this indexer is immutable), use the following syntax.
        /// </para>
        /// <code>
        /// var nvr = new NameValueRecordCollection&lt;int,string&gt;
        /// {
        ///     [7] = "seven",
        ///     [9] = "nine",
        ///     [13] = "thirteen",
        /// }
        /// </code>
        /// </example>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
#if NET5_0_OR_GREATER
        public TValue? this[TKey key]
#else
        public TValue this[TKey key]
#endif
        {
            get {
                return data.ContainsKey(key) ? data[key] : null;
            }
#if NET5_0_OR_GREATER
            init
            {
#else
            set
            {
#endif
                if (value is null) data.Remove(key);
                else data[key] = value;
            }
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) data).GetEnumerator();

        /// <inheritdoc/>
        public override bool Equals(
#if NET5_0_OR_GREATER
                object? obj
#else
                object obj
#endif
            ) => Equals(obj as NameValueRecordCollection<TKey, TValue>);

        /// <inheritdoc/>
        public bool Equals(
#if NET5_0_OR_GREATER
                NameValueRecordCollection<TKey, TValue>? other
#else
                NameValueRecordCollection<TKey, TValue> other
#endif
            )
        {
            if(ReferenceEquals(other, this)) return true;
            if(ReferenceEquals(other, null)) return false;

            var thisKvpSet = new HashSet<KeyValuePair<TKey, TValue>>(this);
            var otherKvpSet = new HashSet<KeyValuePair<TKey, TValue>>(other);

            return thisKvpSet.SetEquals(otherKvpSet);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            const int prime = 31;
            int current = prime;

            foreach(var kvp in data)
            {
                unchecked
                {
                    current = prime * current + kvp.GetHashCode();
                }
            }

            return current;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="NameValueRecordCollection{TKey, TValue}"/>.
        /// </summary>
        public NameValueRecordCollection() : this(new Dictionary<TKey, TValue>()) {}

        private NameValueRecordCollection(Dictionary<TKey, TValue> data)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));
        }

#if !NET5_0_OR_GREATER
        /// <summary>
        /// Gets a clone (shallow copy) of the current <see cref="NameValueRecordCollection{TKey, TValue}"/> containing the same items.
        /// </summary>
        public NameValueRecordCollection<TKey,TValue> Clone() => new NameValueRecordCollection<TKey, TValue>(new Dictionary<TKey, TValue>(data));
#else
        /// <summary>
        /// Gets a clone (shallow copy) of the current <see cref="NameValueRecordCollection{TKey, TValue}"/> containing the same items, as well as the specified item.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As with the indexer, if the value is <see langword="null" /> then this will result in the removal of the item in the returned copy.
        /// </para>
        /// </remarks>
        /// <param name="key">The key at which to add, remove or update an item</param>
        /// <param name="value">The value to store for the item, or <see langword="null" /> which indicates that the item is to be removed.</param>
        /// <returns>
        /// A copy of the current instance, with a single item added, removed or altered in that copied instance.
        /// </returns>
        public NameValueRecordCollection<TKey,TValue> WithItem(TKey key, TValue? value) => WithItems(new KeyValuePair<TKey, TValue?>[] { new(key, value) });

        /// <summary>
        /// Gets a clone (shallow copy) of the current <see cref="NameValueRecordCollection{TKey, TValue}"/> containing the same items, as well as the specified items.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As with indexer, if any value is <see langword="null" /> then this will result in the removal of the corresponding item in the returned copy.
        /// </para>
        /// </remarks>
        /// <param name="items">A collection of key/value pairs, indicating the keys &amp; values to add, remove or alter in the copied instance.</param>
        /// <returns>
        /// A copy of the current instance, with the specified items added, removed or altered.
        /// </returns>
        public NameValueRecordCollection<TKey,TValue> WithItems(IEnumerable<KeyValuePair<TKey, TValue?>> items)
        {
            if(items is null) throw new ArgumentNullException(nameof(items));

            var copiedData = new Dictionary<TKey, TValue>(data);

            foreach(var kvp in items)   
            {
                if(kvp.Value is null) copiedData.Remove(kvp.Key);
                else copiedData[kvp.Key] = kvp.Value;
            }

            return new NameValueRecordCollection<TKey, TValue>(copiedData);
        }
#endif
    }
}