using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Default implementation of <see cref="IFormatterRegistry"/> which also serves as an object factory, by virtue of a service provider.
    /// </summary>
    public class ValueFormatterRegistry : IFormatterRegistry
    {
        readonly List<Type> formatterTypes = new List<Type>();

        /// <inheritdoc/>
        public Type this[int index]
        {
            get => formatterTypes[index];
            set {
                AssertIsValid(value);
                formatterTypes[index] = value;
            }
        }

        /// <inheritdoc/>
        public int Count => formatterTypes.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((IList<Type>) formatterTypes).IsReadOnly;

        /// <inheritdoc/>
        public void Add(Type item)
        {
            AssertIsValid(item);
            formatterTypes.Add(item);
        }

        /// <inheritdoc/>
        public void Clear() => formatterTypes.Clear();

        /// <inheritdoc/>
        public bool Contains(Type item) => formatterTypes.Contains(item);

        /// <inheritdoc/>
        public void CopyTo(Type[] array, int arrayIndex) => formatterTypes.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public IEnumerator<Type> GetEnumerator() => formatterTypes.GetEnumerator();

        /// <inheritdoc/>
        public int IndexOf(Type item) => formatterTypes.IndexOf(item);

        /// <inheritdoc/>
        public void Insert(int index, Type item)
        {
            AssertIsValid(item);
            formatterTypes.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(Type item) => formatterTypes.Remove(item);

        /// <inheritdoc/>
        public void RemoveAt(int index) => formatterTypes.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)formatterTypes).GetEnumerator();

        static void AssertIsValid(Type item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (!typeof(IValueFormatter).IsAssignableFrom(item))
                throw new ArgumentException($"The type must derive from {nameof(IValueFormatter)}.", nameof(item));
        }
    }
}
