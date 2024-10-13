using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{

    /// <summary>
    /// Implementation of <see cref="IDeserializesReport"/> that deserializes a Screenplay report from a JSON stream.
    /// </summary>
    public class JsonScreenplayReportReader : IDeserializesReport
    {
        /// <inheritdoc/>
        public async Task<ScreenplayReport> DeserializeAsync(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            return await JsonSerializer.DeserializeAsync<ScreenplayReport>(stream);
        }
    }
}