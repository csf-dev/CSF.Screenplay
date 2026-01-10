//
// SetAnElementValueTests.cs
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
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using CSF.Screenplay.Selenium.Tests.Personas;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.ScriptResources
{
  [TestFixture]
  [Description("The SetAnElementValue stored JavaScript")]
  public class SetAnElementValueTests
  {
    [Test,Screenplay,Description("Executing the script successfully sets the value of the element")]
    public void Executing_the_script_successfully_sets_the_value(ICast cast)
    {
      var joe = cast.Get<Joe>();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());
      var theElement = Given(joe).WasAbleTo(Get.TheElement(PageTwo.SpecialInputField));

      When(joe).AttemptsTo(Execute.JavaScript.WhichSetsTheValueOf(theElement).To("The right value"));

      Then(joe).ShouldSee(TheText.Of(PageTwo.TheDynamicTextArea)).Should().Be("different value");
    }
  }
}
