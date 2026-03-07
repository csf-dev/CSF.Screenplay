namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which can get the MIME type for a given filename.
    /// </summary>
    public interface IGetsContentType
    {
        /// <summary>
        /// Gets the content type (aka MIME type) for a specified filename.
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <returns>The content type</returns>
        string GetContentType(string fileName);
    }
}