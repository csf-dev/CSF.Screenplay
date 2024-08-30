namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// An object which can get an <see cref="IPerformable"/> instance, such as a performable builder.
    /// </summary>
    public interface IGetsPerformable
    {
        /// <summary>
        /// Gets the performable object from the current instance.
        /// </summary>
        /// <returns>A performable object</returns>
        IPerformable GetPerformable();
    }
}