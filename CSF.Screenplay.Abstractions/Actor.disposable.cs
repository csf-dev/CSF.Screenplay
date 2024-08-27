using System;

namespace CSF.Screenplay
{
    public sealed partial class Actor : IDisposable
    {
        bool disposedValue;

        /// <summary>Method implementing the dispose pattern.</summary>
        /// <remarks>
        /// <para>
        /// This implementation of disposal will iterate through all of this actor's <see cref="Abilities"/>
        /// and - for any of them which implement <see cref="IDisposable"/> - dispose them.
        /// </para>
        /// </remarks>
        void Dispose(bool disposing)
        {
            if (disposedValue) return;

            if (disposing)
            {
                foreach(var ability in Abilities)
                {
                    if (!(ability is IDisposable disposable))
                        continue;
                    
                    disposable.Dispose();
                }
            }

            disposedValue = true;
        }

        /// <summary>Destructor for instances of <see cref="Actor"/>.</summary>
        ~Actor() => Dispose(false);

        void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose() => Dispose();

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