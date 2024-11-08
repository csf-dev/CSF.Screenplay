//
// SaveOptions.cs
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

namespace CSF.Screenplay.Selenium.Screenshots
{
  /// <summary>
  /// Represents the available options for saving Screenshots.
  /// </summary>
  public class SaveOptions
  {
    /// <summary>
    /// Gets the root directory.
    /// </summary>
    /// <value>The root directory.</value>
    public DirectoryInfo RootDirectory { get; }

    /// <summary>
    /// Gets a value indicating whether a separate subdirectory of the <see cref="RootDirectory"/> will be created for
    /// each feature.
    /// </summary>
    /// <value><c>true</c> if a directory is to be used for each feature; otherwise, <c>false</c>.</value>
    public bool UseDirectoryForEachFeature { get; }

    /// <summary>
    /// Gets a value indicating whether a separate subdirectory of the <see cref="RootDirectory"/> will be created for
    /// each feature and each scenario within each feature.
    /// </summary>
    /// <value><c>true</c> if a directory is to be used for each scenario; otherwise, <c>false</c>.</value>
    public bool UseDirectoryForEachScenario { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Screenshots.SaveOptions"/> class.
    /// </summary>
    /// <param name="rootDirectory">Root directory.</param>
    /// <param name="useDirectoryForEachFeature">If set to <c>true</c> use directory for each feature.</param>
    /// <param name="useDirectoryForEachScenario">If set to <c>true</c> use directory for each scenario.</param>
    public SaveOptions(DirectoryInfo rootDirectory,
                       bool useDirectoryForEachFeature = false,
                       bool useDirectoryForEachScenario = false)
    {
      if(rootDirectory == null)
        throw new ArgumentNullException(nameof(rootDirectory));
      if(useDirectoryForEachScenario && !useDirectoryForEachFeature)
        throw new ArgumentException("May only save in a per-scenario directory when per-feature directories are also enabled.",
                                    nameof(useDirectoryForEachScenario));

      RootDirectory = rootDirectory;
      UseDirectoryForEachFeature = useDirectoryForEachFeature;
      UseDirectoryForEachScenario = useDirectoryForEachScenario;
    }
  }
}
