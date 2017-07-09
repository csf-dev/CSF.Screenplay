using System;
using System.IO;
using CSF.IO;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Tests
{
  public class ScreenshotService
  {
    readonly IWebDriver webDriver;
    readonly DirectoryInfo rootPath;
    readonly ScreenshotImageFormat format;

    public void TakeAndSaveScreenshot(Type testClass, string testName)
    {
      var screenshotDriver = webDriver as ITakesScreenshot;
      if(screenshotDriver == null)
        return;

      TakeAndSaveScreenshot(screenshotDriver, testClass, testName);
    }

    void TakeAndSaveScreenshot(ITakesScreenshot driver, Type testClass, string testName)
    {
      CreateRootPath();
      var filePath = GetFilePath(testClass, testName);
      driver.GetScreenshot().SaveAsFile(filePath, format);
    }

    void CreateRootPath()
    {
      rootPath.CreateRecursively();
    }

    string GetFilePath(Type testClass, string testName)
    {
      string filename = $"{testClass.Name}.{testName}.{format.ToString().ToLowerInvariant()}";
      return Path.Combine(rootPath.FullName, filename);
    }

    public ScreenshotService(IWebDriver webDriver,
                             DirectoryInfo rootPath,
                             ScreenshotImageFormat format = ScreenshotImageFormat.Png)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));
      if(rootPath == null)
        throw new ArgumentNullException(nameof(rootPath));
      format.RequireDefinedValue(nameof(format));

      this.webDriver = webDriver;
      this.rootPath = rootPath;
      this.format = format;
    }
  }
}
