namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Options for converting a JSON report to HTML.
    /// </summary>
    public class ReportConverterOptions
    {
        /// <summary>
        /// Gets or sets the file system path to the JSON report which is to be converted to HTML.
        /// </summary>
        public string ReportPath { get; set; }
    }
}