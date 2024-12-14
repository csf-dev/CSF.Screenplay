using CSF.Screenplay.Selenium.Queries;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Provides a factory method for creating instances of <see cref="SingleElementQuery{TResult}"/>.
    /// </summary>
    public static class SingleElementQuery
    {
        /// <summary>
        /// Creates a new instance of <see cref="SingleElementQuery{TResult}"/> with the specified query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the query.</typeparam>
        /// <param name="query">The query to be used for retrieving the value from the Selenium element.</param>
        /// <returns>A new instance of <see cref="SingleElementQuery{TResult}"/>.</returns>
        public static SingleElementQuery<TResult> From<TResult>(IQuery<TResult> query) => new SingleElementQuery<TResult>(query);
    }
}