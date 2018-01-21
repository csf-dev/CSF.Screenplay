using System.Collections.Generic;

namespace CSF.Screenplay.Reporting.Models
{
    /// <summary>
    /// Factory service which creates instances of <see cref="Report"/>.
    /// </summary>
    public interface IReportFactory
    {
        /// <summary>
        /// Creates and returns a <see cref="Report"/> instance.
        /// </summary>
        /// <param name="scenarios"></param>
        /// <returns></returns>
        Report GetReport(IReadOnlyCollection<Scenario> scenarios);
    }
}