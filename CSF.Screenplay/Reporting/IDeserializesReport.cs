using System.IO;
using System.Threading.Tasks;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which deserializes a Screenplay report from a stream.
    /// </summary>
    public interface IDeserializesReport
    {
        /// <summary>
        /// Deserializes a Screenplay report from the provided stream asynchronously.
        /// </summary>
        /// <param name="stream">The stream containing the serialized Screenplay report.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized Screenplay report.</returns>
        Task<ScreenplayReport> DeserializeAsync(Stream stream);
    }
}