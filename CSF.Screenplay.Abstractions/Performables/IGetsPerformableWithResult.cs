namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// An object which can get a non-generic <see cref="IPerformableWithResult"/> instance, such as a performable builder.
    /// </summary>
    public interface IGetsPerformableWithResult
    {
        /// <summary>
        /// Gets the performable object from the current instance.
        /// </summary>
        /// <returns>A performable object</returns>
        IPerformableWithResult GetPerformable();
    }
}