//
// ElementsWithAttributesPage.cs
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
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tests.Pages
{
  public class ElementsWithAttributesPage : Page
  {
    public override string GetName() => "the elements-with-attributes page";

    public override IUriProvider GetUriProvider() => new AppUri("ElementsWithAttributes");

    public static ILocatorBasedTarget DivWithClass = new ElementId("DivWithClass", "a div with a class attribute");

    public static ILocatorBasedTarget ClassOutput = new ElementId("DisplayDivClass", "the displayed class");

    public static ILocatorBasedTarget DateInput = new ElementId("DateInput", "the date input field");

    public static ILocatorBasedTarget DateOutput = new ElementId("DisplayDateReadonly", "the displayed date-readonly attribute");

    public static ILocatorBasedTarget TextInput = new ElementId("TextInput", "the text input field");

    public static ILocatorBasedTarget TextOutput = new ElementId("DisplayTextReadonly", "the displayed text-readonly attribute");

    public static ILocatorBasedTarget TextPlaceholder = new ElementId("DisplayTextPlaceholder", "the displayed text-placeholder attribute");

    public static ILocatorBasedTarget GetOutputButton = new ElementId("GetOutput", "the button which refreshes the outputs");
  }
}
