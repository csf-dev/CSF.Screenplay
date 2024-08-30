namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// An object which can get an <see cref="IPerformableWithResult{TResult}"/> instance, such as a performable builder.
    /// </summary>
    public interface IGetsPerformableWithResult<TResult>
    {
        /// <summary>
        /// Gets the performable object from the current instance.
        /// </summary>
        /// <returns>A performable object</returns>
        IPerformableWithResult<TResult> GetPerformable();
    }
}