using System.Linq;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay
{
    /// <summary>
    /// Default implementation of <see cref="IFormatsReportFragment"/>.
    /// </summary>
    public class ReportFragmentFormatter : IFormatsReportFragment
    {
        readonly IGetsReportFormat formatCreator;
        readonly IGetsValueFormatter formatterFactory;

        /// <inheritdoc/>
        public ReportFragment Format(string template, params object[] values)
        {
            var format = formatCreator.GetReportFormat(template, values);
            var formattedValues = format.Values
                .Select(x => formatterFactory.GetValueFormatter(x.Value).Format(x.Value))
                .Cast<object>()
                .ToArray();
            var fragment = string.Format(format.FormatTemplate, formattedValues);
            return new ReportFragment(format.OriginalTemplate, fragment, format.Values);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ReportFragmentFormatter"/>.
        /// </summary>
        /// <param name="formatCreator">A format creator which parses the original format string and values into a <see cref="ReportFormat"/></param>
        /// <param name="formatterFactory">A factory for instances of <see cref="IValueFormatter"/></param>
        /// <exception cref="System.ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        public ReportFragmentFormatter(IGetsReportFormat formatCreator, IGetsValueFormatter formatterFactory)
        {
            this.formatCreator = formatCreator ?? throw new System.ArgumentNullException(nameof(formatCreator));
            this.formatterFactory = formatterFactory ?? throw new System.ArgumentNullException(nameof(formatterFactory));
        }
    }
}