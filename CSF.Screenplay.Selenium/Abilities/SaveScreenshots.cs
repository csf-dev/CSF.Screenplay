//
// SaveScreenshots.cs
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
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Selenium.Screenshots;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Abilities
{
  /// <summary>
  /// A Screenplay Ability allowing a user to save screenshots to file.
  /// </summary>
  public class SaveScreenshots : Ability
  {
    readonly SaveOptions saveOptions;
    readonly IGetsSavePath pathProvider;
    readonly ISavesScreenshotsToFile fileSaver;
    readonly Scenario scenario;
    int screenshotCounter;

    /// <summary>
    /// Saves the screenshot to a file, based upon the name of the current <see cref="Scenario"/> and a given name.
    /// </summary>
    /// <param name="screenshot">Screenshot.</param>
    /// <param name="name">Name.</param>
    public void Save(Screenshot screenshot, string name)
    {
      if(screenshot == null)
        throw new ArgumentNullException(nameof(screenshot));

      var target = pathProvider.GetSaveFile(scenario, name, screenshotCounter++, saveOptions);
      fileSaver.Save(screenshot, target);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Abilities.SaveScreenshots"/> class.
    /// </summary>
    /// <param name="saveOptions">Save options.</param>
    /// <param name="scenario">The current scenario.</param>
    public SaveScreenshots(SaveOptions saveOptions, Scenario scenario) : this(saveOptions, scenario, null, null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Abilities.SaveScreenshots"/> class.
    /// </summary>
    /// <param name="saveOptions">Save options.</param>
    /// <param name="scenario">The current scenario.</param>
    /// <param name="pathProvider">Path provider.</param>
    /// <param name="fileSaver">File saver.</param>
    public SaveScreenshots(SaveOptions saveOptions,
                           Scenario scenario,
                           IGetsSavePath pathProvider,
                           ISavesScreenshotsToFile fileSaver)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));
      if(saveOptions == null)
        throw new ArgumentNullException(nameof(saveOptions));

      this.fileSaver = fileSaver ?? new ScreenshotSaver();
      this.pathProvider = pathProvider ?? new SavePathProvider();
      this.saveOptions = saveOptions;
      this.scenario = scenario;

      screenshotCounter = 1;
    }
  }
}
