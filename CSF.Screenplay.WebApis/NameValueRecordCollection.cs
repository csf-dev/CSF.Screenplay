using System;
using System.Collections;
using System.Collections.Generic;

namespace CSF.Screenplay
{
    /// <summary>
    /// A simple name/value collection with an indexer, backed by a <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Please note that this type differs in form depending upon the .NET version under which it is consumed.
    /// If consuming this type from logic which targets .NET Standard 2.0 or .NET Framework 4.6.2 then this type is a <see langword="class" />.
    /// In that scenario the indexer is mutable and the developer should take care to ensure that they do not mutate/alter its
    /// state inadvertently. Developers should ensure that they manually copy the state from the current instance into a new instance
    /// instead of modifying an existing instance.
    /// In these target frameworks, a <c>Clone</c> method has been provided to assist with this.
    /// </para>
    /// <para>
    /// When consuming this from .NET 5 or higher, this type is instead a <see langword="record" /> and is immutable by design.
    /// The indexer is init-only in that case.
    /// When using .NET 5, developers may use nondestructive mutation with the <see langword="with" /> keyword/expression to create a copy of
    /// the current instance but with some differences.
    /// </para>
    /// </remarks>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
#if NET5_0_OR_GREATER
    public record
#else
    public class
#endif
        NameValueRecordCollection<TKey,TValue>
        : IEnumerable<KeyValuePair<TKey,TValue>>
        where TValue : class
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
        public TValue this[TKey key]
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
#endif
    }
}