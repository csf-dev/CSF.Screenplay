using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Provides functionality to read the HTML template which is embedded as a resource into the current assembly.
    /// </summary>
    public class TemplateReader : IGetsHtmlTemplate
    {
        /// <summary>
        /// Reads the HTML template which is embedded as a resource into the current assembly.
        /// </summary>
        /// <returns>The HTML template as a string.</returns>
        public async Task<string> ReadTemplate()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("template.html"))
            {
                if(stream is null)
                    throw new InvalidOperationException("The embedded HTML template could not be found; this indicates a serious build error.");
                
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
        }
    }
}