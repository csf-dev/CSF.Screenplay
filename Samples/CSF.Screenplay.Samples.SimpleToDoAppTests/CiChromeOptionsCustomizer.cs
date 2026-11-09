using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium.Chrome;

namespace CSF.Screenplay;

/// <summary>
/// A customization class which sets up Google Chrome browser with some headless options.
/// </summary>
/// <remarks>
/// <para>
/// This is not strictly part of Screenplay or its Selenium plugin.  This is used with <see cref="ICustomizesOptions{T}"/>,
/// to pre-configure a browser.
/// </para>
/// </remarks>
public class CiChromeOptionsCustomizer : ICustomizesOptions<ChromeOptions>
{
    public void CustomizeOptions(ChromeOptions options)
    {
        options.AddArguments("--disable-dev-shm-usage", "--headless", "--no-sandbox", "--disable-gpu", "--window-size=1280,1024");
    }
}