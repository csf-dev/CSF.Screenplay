namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Implementation of <see cref="IGetsContentType"/> which makes use of the MimeTypes NuGet package.
    /// </summary>
    public class ContentTypeProvider : IGetsContentType
    {
        /// <inheritdoc/>
        public string GetContentType(string fileName) => MimeTypes.GetMimeType(fileName);
    }
}