//
// SaveScreenshotsBuilderExtensions.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Selenium.Abilities;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Extension methods relating to the registration of a screenshot-saving service.
  /// </summary>
  public static class SaveScreenshotsBuilderExtensions
  {
    /// <summary>
    /// Indicates that any screenshots should be saved in a given directory.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="screenshotDirectory">Screenshot directory.</param>
    public static void SaveScreenshotsInDirectory(this IIntegrationConfigBuilder helper,
                                                  string screenshotDirectory)
    {
      SaveScreenshotsInDirectory(helper, new DirectoryInfo(screenshotDirectory));
    }

    /// <summary>
    /// Indicates that any screenshots should be saved in a directory per test feature, each of which is contained
    /// within a given root screenshot-saving directory.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="screenshotRootDirectory">Screenshot root directory.</param>
    public static void SaveScreenshotsInDirectoryPerFeature(this IIntegrationConfigBuilder helper,
                                                            string screenshotRootDirectory)
    {
      SaveScreenshotsInDirectoryPerFeature(helper, new DirectoryInfo(screenshotRootDirectory));
    }

    /// <summary>
    /// Indicates that any screenshots should be saved in a directory hierarchy, first by test feature and
    /// then by test scenario.  This directory hierarchy will be contained within a given root
    /// screenshot-saving directory.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="screenshotRootDirectory">Screenshot root directory.</param>
    public static void SaveScreenshotsInDirectoryPerScenario(this IIntegrationConfigBuilder helper,
                                                             string screenshotRootDirectory)
    {
      SaveScreenshotsInDirectoryPerScenario(helper, new DirectoryInfo(screenshotRootDirectory));
    }
    /// <summary>
    /// Indicates that any screenshots should be saved in a given directory.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="screenshotDirectory">Screenshot directory.</param>
    public static void SaveScreenshotsInDirectory(this IIntegrationConfigBuilder helper,
                                                  DirectoryInfo screenshotDirectory)
    {
      var options = new Screenshots.SaveOptions(screenshotDirectory, false, false);
      SaveScreenshots(helper, options);
    }

    /// <summary>
    /// Indicates that any screenshots should be saved in a directory per test feature, each of which is contained
    /// within a given root screenshot-saving directory.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="screenshotRootDirectory">Screenshot root directory.</param>
    public static void SaveScreenshotsInDirectoryPerFeature(this IIntegrationConfigBuilder helper,
                                                            DirectoryInfo screenshotRootDirectory)
    {
      var options = new Screenshots.SaveOptions(screenshotRootDirectory, true, false);
      SaveScreenshots(helper, options);
    }

    /// <summary>
    /// Indicates that any screenshots should be saved in a directory hierarchy, first by test feature and
    /// then by test scenario.  This directory hierarchy will be contained within a given root
    /// screenshot-saving directory.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="screenshotRootDirectory">Screenshot root directory.</param>
    public static void SaveScreenshotsInDirectoryPerScenario(this IIntegrationConfigBuilder helper,
                                                             DirectoryInfo screenshotRootDirectory)
    {
      var options = new Screenshots.SaveOptions(screenshotRootDirectory, true, true);
      SaveScreenshots(helper, options);
    }

    static void SaveScreenshots(IIntegrationConfigBuilder helper, Screenshots.SaveOptions options)
    {
      helper.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterFactory((Scenario s) => new SaveScreenshots(options, s));
      });
    }
  }
}
