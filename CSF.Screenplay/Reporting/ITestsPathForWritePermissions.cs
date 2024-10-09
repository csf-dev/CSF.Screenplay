namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which may test a file system path for writability.
    /// </summary>
    public interface ITestsPathForWritePermissions
    {
        /// <summary>
        /// Gets a value indicating whether or not the current process should be able to write a file at the specified path.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The path may be relative or absolute; if relative then it is treated as relative to the current working directory.
        /// </para>
        /// </remarks>
        /// <param name="path">An absolute or relative file path.</param>
        /// <returns><see langword="true" /> if the current process is able to write to the specified path; <see langword="false" /> if not.</returns>
        bool HasWritePermission(string path);
    }
}