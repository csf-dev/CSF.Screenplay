using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
    /// <summary>
    /// Factory service which creates instances of <see cref="Report"/>.
    /// </summary>
    public class ReportFactory
    {
        /// <summary>
        /// Creates and returns a <see cref="Report"/> instance.
        /// </summary>
        /// <param name="scenarios"></param>
        /// <returns></returns>
        public Report GetReport(IReadOnlyCollection<Scenario> scenarios)
        {
            return new Report(scenarios.ToArray());
        }
    }
}