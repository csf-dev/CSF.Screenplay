using System.Drawing;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the location of the top-left corner of a Selenium element.
    /// </summary>
    public class LocationQuery : IQuery<Point>
    {
        /// <inheritdoc/>
        public string Name => $"the location of the topleft corner of the element";

        /// <inheritdoc/>
        public Point GetValue(SeleniumElement element) => element.WebElement.Location;
    }
}
