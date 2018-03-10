//
// SetAnAttributeValueTests.cs
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
  [TestFixture,Description("Setting the value of an attribute using JavaScript")]
  public class SetAnAttributeValueTests
  {
    [Test,Screenplay,Description("Removing an attribute should result in successful removal")]
    public void Removing_an_attribute_should_remove_it(ICast cast)
    {
      var joe = cast.Get<Joe>();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<ElementsWithAttributesPage>());
      var element = Given(joe).WasAbleTo(Get.TheElement(ElementsWithAttributesPage.TextInput));

      When(joe).AttemptsTo(Execute.JavaScript.WhichRemovesTheAttribute("readonly").From(element));
      When(joe).AttemptsTo(Click.On(ElementsWithAttributesPage.GetOutputButton));

      Then(joe).ShouldSee(TheText.Of(ElementsWithAttributesPage.TextOutput)).Should().Be(String.Empty);
    }

    [Test,Screenplay,Description("Removing an unrelated attribute should not affect the original")]
    public void Removing_a_disabled_attribute_should_not_the_readonly_attribute(ICast cast)
    {
      var joe = cast.Get<Joe>();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<ElementsWithAttributesPage>());
      var element = Given(joe).WasAbleTo(Get.TheElement(ElementsWithAttributesPage.TextInput));

      When(joe).AttemptsTo(Execute.JavaScript.WhichRemovesTheAttribute("disabled").From(element));
      When(joe).AttemptsTo(Click.On(ElementsWithAttributesPage.GetOutputButton));

      Then(joe).ShouldSee(TheText.Of(ElementsWithAttributesPage.TextOutput)).Should().Be("readonly");
    }

    [Test,Screenplay,Description("Setting a previously-unset attribute should update its value")]
    public void Editing_an_attribute_should_set_it_to_the_new_value(ICast cast)
    {
      var joe = cast.Get<Joe>();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<ElementsWithAttributesPage>());
      var element = Given(joe).WasAbleTo(Get.TheElement(ElementsWithAttributesPage.TextInput));

      When(joe).AttemptsTo(Execute.JavaScript.WhichSetsTheAttribute("placeholder").For(element).To("New placeholder"));
      When(joe).AttemptsTo(Click.On(ElementsWithAttributesPage.GetOutputButton));

      Then(joe).ShouldSee(TheText.Of(ElementsWithAttributesPage.TextPlaceholder)).Should().Be("New placeholder");
    }
  }
}
