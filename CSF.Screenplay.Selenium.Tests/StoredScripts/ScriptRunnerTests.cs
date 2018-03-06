//
// ScriptRunnerTests.cs
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
using CSF.Screenplay.Selenium.StoredScripts;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Tests.StoredScripts
{
  [TestFixture]
  public class ScriptRunnerTests
  {
    [Test]
    public void ExecuteScript_passes_correct_script_to_webdriver()
    {
      // Arrange
      var script = Mock.Of<IProvidesScript>();
      var driver = new Mock<IWebDriver>();
      driver.As<IJavaScriptExecutor>();
      var sut = new ScriptRunner();

      Mock.Get(script).Setup(x => x.GetScript()).Returns("function fooBar(argsArray) {}");
      Mock.Get(script).Setup(x => x.GetEntryPointName()).Returns("fooBar");

      var expectedExecutedScript = @"function fooBar(argsArray) {}
fooBar(arguments);";

      // Act
      sut.ExecuteScript(script, driver.Object);

      // Assert
      driver
          .As<IJavaScriptExecutor>()
          .Verify(x => x.ExecuteScript(expectedExecutedScript), Times.Once);
    }

    [Test]
    public void ExecuteScript_passes_correct_script_arguments_to_webdriver()
    {
      // Arrange
      var script = Mock.Of<IProvidesScript>();
      var driver = new Mock<IWebDriver>();
      driver.As<IJavaScriptExecutor>();
      var sut = new ScriptRunner();

      Mock.Get(script).Setup(x => x.GetScript()).Returns("function fooBar(argsArray) {}");
      Mock.Get(script).Setup(x => x.GetEntryPointName()).Returns("fooBar");

      // Act
      sut.ExecuteScript(script, driver.Object, 1, 2, "three");

      // Assert
     driver
          .As<IJavaScriptExecutor>()
          .Verify(x => x.ExecuteScript(It.IsAny<string>(), 1, 2, "three"), Times.Once);
    }
  }
}
