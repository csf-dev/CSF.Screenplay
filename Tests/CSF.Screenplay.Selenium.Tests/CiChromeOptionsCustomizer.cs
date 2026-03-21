using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium.Chrome;

namespace CSF.Screenplay.Selenium;

public class CiChromeOptionsCustomizer : ICustomizesOptions<ChromeOptions>
{
    public void CustomizeOptions(ChromeOptions options)
    {
        options.AddArguments("--disable-dev-shm-usage", "--headless", "--no-sandbox", "--disable-gpu", "--window-size=1280,1024");
    }
}