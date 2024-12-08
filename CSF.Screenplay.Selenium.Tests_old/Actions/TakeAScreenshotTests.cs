//
// TakeAScreenshotTests.cs
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
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Tests.Personas;
using static CSF.Screenplay.StepComposer;
using NUnit.Framework;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;

namespace CSF.Screenplay.Selenium.Tests.Actions
{
  [TestFixture]
  public class TakeAScreenshotTests
  {
    [Test,Screenplay]
    public void Taking_and_saving_a_screenshot_should_not_crash(ICast cast, SaveScreenshots saveScreenshots)
    {
      var joe = cast.Get<Joe>();
      joe.IsAbleTo(saveScreenshots);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Assert.That(() => {
        When(joe).AttemptsTo(TakeAScreenshot.AndSaveItWithTheName("test-screenshot"));
      }, Throws.Nothing);
    }

    [Test,Screenplay]
    public void Taking_and_saving_a_screenshot_two_screenshots_should_not_crash(ICast cast,
                                                                                SaveScreenshots saveScreenshots)
    {
      var joe = cast.Get<Joe>();
      joe.IsAbleTo(saveScreenshots);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Assert.That(() => {
        
        When(joe).AttemptsTo(TakeAScreenshot.AndSaveItWithTheName("first-screenshot"));
        When(joe).AttemptsTo(TakeAScreenshot.AndSaveItWithTheName("second-screenshot"));

      }, Throws.Nothing);
    }
  }
}
