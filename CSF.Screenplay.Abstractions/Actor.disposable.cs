using System;

namespace CSF.Screenplay
{
    public partial class Actor : IDisposable
    {
        bool disposedValue;

        /// <summary>
        /// Asserts that the current instance is not disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the current instance is already disposed.</exception>
        void AssertNotDisposed()
        {
            if(disposedValue)
                throw new ObjectDisposedException($"{nameof(Actor)} '{Name}'", "Operations upon a disposed actor are not permitted.");
        }

        /// <summary>
        /// Disposes the current instance, via the Dispose Pattern.
        /// </summary>
        /// <param name="disposing">A value indicating wherher or not disposal should occur.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;

            if (disposing)
            {
                foreach(var ability in Abilities)
                {
                    if (ability is IDisposable disposable)
                        disposable.Dispose();
                }
            }

            disposedValue = true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}