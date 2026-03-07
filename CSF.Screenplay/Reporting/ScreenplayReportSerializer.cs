using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{

    /// <summary>
    /// Implementation of <see cref="IDeserializesReport"/> and <see cref="ISerializesReport"/>
    /// which serializes and/or deserializes a Screenplay report to/from a JSON stream.
    /// </summary>
    public class ScreenplayReportSerializer : IDeserializesReport, ISerializesReport
    {
        /// <inheritdoc/>
        public async Task<ScreenplayReport> DeserializeAsync(Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return await JsonSerializer.DeserializeAsync<ScreenplayReport>(stream).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Stream> SerializeAsync(ScreenplayReport report)
        {
            if (report is null)
                throw new ArgumentNullException(nameof(report));

            var stream = new BufferedStream(new MemoryStream());
            await JsonSerializer.SerializeAsync(stream, report).ConfigureAwait(false);
            stream.Position = 0;
            return stream;
        }
    }
}