using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the text content of a Selenium element.
    /// </summary>
    public class TextQuery : IQuery<string>
    {
        /// <inheritdoc/>
        public string GetValue(SeleniumElement element) => element.WebElement.Text;
    }
}
