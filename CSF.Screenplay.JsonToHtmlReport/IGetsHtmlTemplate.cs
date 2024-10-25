using System.Threading.Tasks;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Provides functionality to read the HTML template.
    /// </summary>
    public interface IGetsHtmlTemplate
    {
        /// <summary>
        /// Reads the HTML template as a string.
        /// </summary>
        Task<string> ReadTemplate();
    }
}
