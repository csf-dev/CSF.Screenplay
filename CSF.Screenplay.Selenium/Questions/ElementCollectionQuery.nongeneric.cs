using CSF.Screenplay.Selenium.Queries;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Provides a factory method for creating instances of <see cref="ElementCollectionQuery{TResult}"/>.
    /// </summary>
    public static class ElementCollectionQuery
    {
        /// <summary>
        /// Creates a new instance of <see cref="ElementCollectionQuery{TResult}"/> with the specified query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the query.</typeparam>
        /// <param name="query">The query to be used for retrieving the value from each Selenium element.</param>
        /// <returns>A new instance of <see cref="SingleElementQuery{TResult}"/>.</returns>
        public static ElementCollectionQuery<TResult> From<TResult>(IQuery<TResult> query) => new ElementCollectionQuery<TResult>(query);
    }
}