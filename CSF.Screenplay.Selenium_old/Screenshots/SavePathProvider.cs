//
// SavePathProvider.cs
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
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Selenium.Screenshots
{
  /// <summary>
  /// Default implementation of <see cref="IGetsSavePath"/>.
  /// </summary>
  public class SavePathProvider : IGetsSavePath
  {
    const string Extension = "png";

    /// <summary>
    /// Gets a <c>FileInfo</c> for the path to to the file where a screenshot should be saved.
    /// </summary>
    /// <returns>The file information for saving a screenshot.</returns>
    /// <param name="scenario">Scenario.</param>
    /// <param name="name">Name.</param>
    /// <param name="screenshotNumber">Screenshot number.</param>
    /// <param name="options">Options.</param>
    public FileInfo GetSaveFile(Scenario scenario, string name, int screenshotNumber, SaveOptions options)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));
      if(options == null)
        throw new ArgumentNullException(nameof(options));

      if(!options.UseDirectoryForEachFeature)
        return GetFilePathInRootDirectory(scenario, name, screenshotNumber, options);

      if(!options.UseDirectoryForEachScenario)
        return GetFilePathInFeatureDirectory(scenario, name, screenshotNumber, options);

      return GetFilePathInScenarioDirectory(scenario, name, screenshotNumber, options);
    }

    FileInfo GetFilePathInRootDirectory(Scenario scenario, string name, int screenshotNumber, SaveOptions options)
    {
      string filename = $"{scenario.FeatureId.Identity}.{scenario.ScenarioId.Identity}.{screenshotNumber}";

      if(!String.IsNullOrEmpty(name))
        filename = $"{filename}.{name}";

      filename = $"{filename}.{Extension}";

      var fullPath = Path.Combine(options.RootDirectory.FullName,
                                  filename);
      return new FileInfo(fullPath);
    }

    FileInfo GetFilePathInFeatureDirectory(Scenario scenario, string name, int screenshotNumber, SaveOptions options)
    {
      string filename = $"{scenario.ScenarioId.Identity}.{screenshotNumber}";

      if(!String.IsNullOrEmpty(name))
        filename = $"{filename}.{name}";

      filename = $"{filename}.{Extension}";

      var fullPath = Path.Combine(options.RootDirectory.FullName,
                                  scenario.FeatureId.Identity,
                                  filename);
      return new FileInfo(fullPath);
    }

    FileInfo GetFilePathInScenarioDirectory(Scenario scenario, string name, int screenshotNumber, SaveOptions options)
    {
      string filename = $"{screenshotNumber}";

      if(!String.IsNullOrEmpty(name))
        filename = $"{filename}.{name}";

      filename = $"{filename}.{Extension}";

      var fullPath = Path.Combine(options.RootDirectory.FullName,
                                  scenario.FeatureId.Identity,
                                  scenario.ScenarioId.Identity,
                                  filename);
      return new FileInfo(fullPath);
    }
  }
}
