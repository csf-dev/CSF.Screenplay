using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the options within a <c>&lt;select&gt;</c> element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the queried element is not an HTML <c>&lt;select&gt;</c> element then the outcome of this query
    /// is undefined.
    /// </para>
    /// </remarks>
    public class OptionsQuery : IQuery<IReadOnlyList<Option>>
    {
        const string optionTagName = "option";
        static readonly By options = By.TagName(optionTagName);

        readonly bool excludeSelectedOptions;
        readonly bool excludeUnselectedOptions;

        /// <inheritdoc/>
        public IReadOnlyList<Option> GetValue(SeleniumElement element)
        {
            var allOptions = element.WebElement.FindElements(options).ToList();

            if(excludeSelectedOptions)
                allOptions = allOptions.Where(x => x.Selected == false).ToList();
            if(excludeUnselectedOptions)
                allOptions = allOptions.Where(x => x.Selected == true).ToList();

            return allOptions.Select(x => new Option(x.Text, x.GetAttribute(ValueQuery.ValueAttribute))).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsQuery"/> class.
        /// </summary>
        /// <param name="excludeSelectedOptions">If set to <c>true</c>, then options which are currently selected will not be included in the output.</param>
        /// <param name="excludeUnselectedOptions">If set to <c>true</c>, then options which are currently not selected will not be included in the output.</param>
        public OptionsQuery(bool excludeSelectedOptions = false, bool excludeUnselectedOptions = false)
        {
            if(excludeSelectedOptions && excludeUnselectedOptions)
                throw new ArgumentException("You cannot exclude both selected and unselected options; this would always lead to an empty result.");
            
            this.excludeSelectedOptions = excludeSelectedOptions;
            this.excludeUnselectedOptions = excludeUnselectedOptions;
        }
    }
}
