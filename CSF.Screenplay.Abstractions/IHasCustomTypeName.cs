namespace CSF.Screenplay
{
    /// <summary>
    /// An object which can provide a custom human-readable .NET type name.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is particularly important when reporting, particularly when writing the name of the performable type
    /// into a report.  Some performables may be written using the adapter or decorator patterns, in which a general-use
    /// class wraps a specific class which implements a subset of a performable. Using <see cref="System.Type.GetType()"/>
    /// will yield the type name of the general-use 'outer' class, which is usually not very useful on its own.
    /// </para>
    /// <para>
    /// If general-use performables, such as adapters, implement this interface, then they can return more useful human-readable
    /// type names to the consuming logic, making use of their inner/wrapped implementation type.
    /// </para>
    /// </remarks>
    public interface IHasCustomTypeName
    {
        /// <summary>
        /// Gets a human-readable name of the type of the current instance.
        /// </summary>
        /// <remarks>
        /// <para>See the remarks on <see cref="IHasCustomTypeName"/>; this does not need to be the same as <see cref="System.Type.GetType()"/>.</para>
        /// </remarks>
        /// <returns>A human-readable name of the .NET type of the current instance, which could (for example) be
        /// qualified with additional context, such as a wrapped implementation.</returns>
        string GetHumanReadableTypeName();
    }
}