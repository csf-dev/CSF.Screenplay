using System;

namespace CSF.Screenplay
{
    public partial class Actor : IDisposable
    {
        bool disposedValue;

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            if (disposedValue) return;

            foreach(var ability in Abilities)
            {
                if (ability is IDisposable disposable)
                    disposable.Dispose();
            }

            disposedValue = true;
        }

        /// <summary>
        /// Asserts that the current instance is not disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the current instance is already disposed.</exception>
        void AssertNotDisposed()
        {
            if(disposedValue)
                throw new ObjectDisposedException($"{nameof(Actor)} '{Name}'", "Operations upon a disposed actor are not permitted.");
        }
    }
}