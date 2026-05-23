using CSF.Screenplay.Selenium.Tasks;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Builder class for creating an instance of <see cref="GetTheBrowserLogs"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Primarily, this is concerned with whether the constructed task should throw or silently return an empty
    /// collection, should retrieving logs be unsupported by the current WebDriver.
    /// </para>
    /// </remarks>
    public class GetTheBrowserLogsBuilder
    {
        /// <summary>
        /// Gets a <see cref="GetTheBrowserLogs"/> task.
        /// The task will be configured such that - if no viable technique for getting logs is available - it will return
        /// an empty collection instead of throwing.
        /// </summary>
        /// <returns>A performable task</returns>
        public GetTheBrowserLogs ButReturnEmptyLogsIfUnsupported()
            => new GetTheBrowserLogs(false);

        /// <summary>
        /// Implicitly converts the builder to an instance of <see cref="GetTheBrowserLogs"/>.
        /// The task will throw if no viable technique for getting logs is available.
        /// </summary>
        /// <param name="builder">The builder instance</param>
        public static implicit operator GetTheBrowserLogs(GetTheBrowserLogsBuilder builder)
            => new GetTheBrowserLogs();
    }
}