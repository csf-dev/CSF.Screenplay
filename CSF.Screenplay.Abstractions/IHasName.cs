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
        string Name { get; }
    }
}
