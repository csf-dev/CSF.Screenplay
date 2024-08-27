namespace CSF.Screenplay
{
    /// <summary>
    /// A part of a Screenplay performance which has a human-readable name.
    /// </summary>
    public interface IHasName
    {
        /// <summary>
        /// Gets the human-readable name of the current object.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see langword="null"/> is not an acceptable or permitted return type here.  All types which implement this interface
        /// must return a non-null response from this property.
        /// </para>
        /// </remarks>
        string Name { get; }
    }
}
