//
// ScreenshotSaver.cs
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
using CSF.IO;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Screenshots
{
  /// <summary>
  /// Default implementation of <see cref="ISavesScreenshotsToFile"/>.
  /// </summary>
  public class ScreenshotSaver : ISavesScreenshotsToFile
  {
    /// <summary>
    /// Saves the screenshot to the given file location.
    /// </summary>
    /// <param name="screenshot">Screenshot.</param>
    /// <param name="file">File.</param>
    public void Save(Screenshot screenshot, FileInfo file)
    {
      if(screenshot == null)
        throw new ArgumentNullException(nameof(screenshot));
      if(file == null)
        throw new ArgumentNullException(nameof(file));

      file.Directory.CreateRecursively();
      screenshot.SaveAsFile(file.FullName, ScreenshotImageFormat.Png);
    }
  }
}
