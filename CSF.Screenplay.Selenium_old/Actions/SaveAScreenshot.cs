//
// SaveAScreenshot.cs
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
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// A Screenplay Action class which represents saving a screenshot.
  /// </summary>
  public class SaveAScreenshot : Performable
  {
    readonly string name;

    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    protected override string GetReport(INamed actor)
    {
      if(!String.IsNullOrEmpty(name))
        return $"{actor} takes a screenshot of the web page and saves it with the name '{name}'";
      
      return $"{actor} takes a screenshot of the web page and saves it";
    }

    /// <summary>
    /// Performs the action as a given actor.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
      var saveScreenshots = actor.GetAbility<SaveScreenshots>();

      var screenshot = GetScreenshot(browseTheWeb);
      Save(screenshot, saveScreenshots);
    }

    Screenshot GetScreenshot(BrowseTheWeb ability)
    {
      var takesScreenshot = ability.WebDriver as ITakesScreenshot;
      if(takesScreenshot == null)
        throw new InvalidOperationException($"This {nameof(IWebDriver)} is unable to take screenshots.");

      return takesScreenshot.GetScreenshot();
    }

    void Save(Screenshot screenshot, SaveScreenshots saveScreenshots)
    {
      saveScreenshots.Save(screenshot, name);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Actions.SaveAScreenshot"/> class.
    /// </summary>
    /// <param name="name">Name.</param>
    public SaveAScreenshot(string name)
    {
      this.name = name;
    }
  }
}
