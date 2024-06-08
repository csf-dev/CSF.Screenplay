using System;

namespace CSF.Screenplay
{
    public partial class Actor : IDisposable
    {
        bool disposedValue;

        /// <summary>Virtual method implementing the dispose pattern, for extensibility.</summary>
        /// <remarks>
        /// <para>
        /// This implementation of disposal will iterate through all of this actor's <see cref="Abilities"/>
        /// and - for any of them which implement <see cref="IDisposable"/> - dispose them.
        /// </para>
        /// </remarks>
        protected virtual void Dispose(bool disposing)
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
    }
}