using System.IO;
using System.Threading.Tasks;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which serializes a Screenplay report into a stream.
    /// </summary>
    public interface ISerializesReport
    {
        /// <summary>
        /// Serializes a Screenplay report into a stream asynchronously.
        /// </summary>
        /// <param name="report">A Screenplay report.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the serialized report stream.</returns>
        Task<Stream> SerializeAsync(ScreenplayReport report);
    }
}