using System.Drawing;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the pixel size a Selenium element.
    /// </summary>
    public class SizeQuery : IQuery<Size>
    {
        /// <inheritdoc/>
        public Size GetValue(SeleniumElement element) => element.WebElement.Size;
    }
}
