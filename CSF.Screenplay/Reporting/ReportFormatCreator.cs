using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Default implementation of <see cref="IGetsReportFormat"/>.
    /// </summary>
    public class ReportFormatCreator : IGetsReportFormat
    {
        const char openBrace = '{', closeBrace = '}';

        /// <inheritdoc/>
        public ReportFormat GetReportFormat(string template, IList<object> values)
        {
            if (template is null)
                throw new ArgumentNullException(nameof(template));
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            var placeholderNames = GetPlaceholderNames(template, out var updatedTemplate);
            var namesAndValues = placeholderNames
                .Select((name, index) => new NameAndValue(name, index < values.Count ? values[index] : null))
                .ToList();
            return new ReportFormat(template, updatedTemplate, namesAndValues);
        }

        static List<string> GetPlaceholderNames(string template, out string updatedTemplate)
        {
            var builder = new StringBuilder(template.Length);
            var results = new List<string>();
            int maxIndex = template.Length, i = 0;

            while(i < maxIndex)
            {
                if(template[i] != openBrace)
                {
                    builder.Append(template[i]);
                    i++;
                    continue;
                }

                if(template[i] == openBrace && i < maxIndex - 1 && template[i+1] == openBrace)
                {
                    builder.Append(openBrace).Append(openBrace);
                    // We have consumed 2 characters of the template, hence the need to double-increment
                    i += 2;
                    continue;
                }

                if(!TryGetBraceContent(template, i, maxIndex, out var content, out var consumed))
                {
                    builder.Append(template[i]);
                    i++;
                    continue;
                }

                i += consumed + 1;
                if(results.Contains(content))
                {
                    builder.Append(openBrace).Append(results.IndexOf(content)).Append(closeBrace);
                }
                else
                {
                    builder.Append(openBrace).Append(results.Count).Append(closeBrace);
                    results.Add(content);
                }
            }

            updatedTemplate = builder.ToString();
            return results;
        }

        static bool TryGetBraceContent(string template, int startIndex, int maxIndex, out string content, out int charactersConsumed)
        {
            var builder = new StringBuilder(template.Length - startIndex);
            int i = startIndex + 1, consumed = 1;

            while(i < maxIndex)
            {
                if(template[i] == closeBrace)
                {
                    content = builder.ToString();
                    charactersConsumed = consumed;
                    return true;
                }

                builder.Append(template[i]);
                i++;
                consumed++;
            }

            // We never found a matching close-brace!
            content = null;
            charactersConsumed = 0;
            return false;
        }
    }
}