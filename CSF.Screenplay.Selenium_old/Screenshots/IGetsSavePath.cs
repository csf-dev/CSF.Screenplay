//
// IGetsSavePath.cs
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
  /// A service which gets the path to which screenshot files should be saved.
  /// </summary>
  public interface IGetsSavePath
  {
    /// <summary>
    /// Gets a <c>FileInfo</c> for the path to to the file where a screenshot should be saved.
    /// </summary>
    /// <returns>The file information for saving a screenshot.</returns>
    /// <param name="scenario">Scenario.</param>
    /// <param name="name">Name.</param>
    /// <param name="screenshotNumber">Screenshot number.</param>
    /// <param name="options">Options.</param>
    FileInfo GetSaveFile(Scenario scenario, string name, int screenshotNumber, SaveOptions options);
  }
}
