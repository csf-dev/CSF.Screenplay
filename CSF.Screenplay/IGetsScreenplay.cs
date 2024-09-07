namespace CSF.Screenplay
{
    /// <summary>
    /// An object which can fully configure and get a <see cref="Screenplay"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is particularly important when using Screenplay as a testing tool.
    /// Some test integrations do not have any inherent extension points for the placement of 'configuration' or
    /// startup logic which affects the entire test run. In those cases, a developer will need to implement this
    /// interface with a class of their own, in order to configure and get the <see cref="Screenplay"/> instance.
    /// </para>
    /// <para>
    /// Note that implementations of this type should have a public parameterless constructor, because they will not
    /// be resolved from dependency injection.
    /// </para>
    /// </remarks>
    public interface IGetsScreenplay
    {
        /// <summary>
        /// Gets the configured Screenplay instance provided by the current type.
        /// </summary>
        /// <returns>A Screenplay instance</returns>
        Screenplay GetScreenplay();
    }
}

