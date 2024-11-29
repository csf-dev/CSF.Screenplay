using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Static helper methods for creating instances of <see cref="ElementCollectionPerformableWithResultAdapter{TResult}"/>.
    /// </summary>
    public static class ElementCollectionPerformableWithResultAdapter
    {
        /// <summary>
        /// Creates a new instance of <see cref="ElementCollectionPerformableWithResultAdapter{TResult}"/> from the specified performable and target.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="performable">The performable to be adapted.</param>
        /// <param name="target">The target elements for the performable.</param>
        /// <returns>A new instance of <see cref="ElementCollectionPerformableWithResultAdapter{TResult}"/>.</returns>
        public static ElementCollectionPerformableWithResultAdapter<TResult> From<TResult>(IElementCollectionPerformableWithResult<TResult> performable, ITarget target)
            => new ElementCollectionPerformableWithResultAdapter<TResult>(performable, target);
    }
}