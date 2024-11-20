using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Static helper methods for creating instances of <see cref="SingleElementPerformableWithResultAdapter{TResult}"/>.
    /// </summary>
    public static class SingleElementPerformableWithResultAdapter
    {
        /// <summary>
        /// Creates a new instance of <see cref="SingleElementPerformableWithResultAdapter{TResult}"/> from the specified performable and target.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="performable">The performable to be adapted.</param>
        /// <param name="target">The target element for the performable.</param>
        /// <returns>A new instance of <see cref="IPerformableWithResult{TResult}"/>.</returns>
        public static IPerformableWithResult<TResult> From<TResult>(ISingleElementPerformableWithResult<TResult> performable, ITarget target)
            => new SingleElementPerformableWithResultAdapter<TResult>(performable, target);
    }
}