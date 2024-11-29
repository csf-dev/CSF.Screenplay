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
        /// <param name="doNotThrowOnMissingElement">If set to <see langword="true" /> then this performable will not throw an exception when the element does not exist.</param>
        /// <returns>A new instance of <see cref="SingleElementPerformableWithResultAdapter{TResult}"/>.</returns>
        public static SingleElementPerformableWithResultAdapter<TResult> From<TResult>(ISingleElementPerformableWithResult<TResult> performable, ITarget target, bool doNotThrowOnMissingElement = false)
            => new SingleElementPerformableWithResultAdapter<TResult>(performable, target, doNotThrowOnMissingElement);
    }
}