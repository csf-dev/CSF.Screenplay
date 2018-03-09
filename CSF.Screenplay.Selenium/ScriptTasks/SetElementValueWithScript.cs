//
// SetElementValueWithScript.cs
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
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.ScriptResources;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.ScriptTasks
{
  public class SetElementValueWithScript : Performable
  {
    readonly IWebElement element;
    readonly string value;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} sets the value of a <{element.TagName}> tp '{value}'";

    protected override void PerformAs(IPerformer actor)
    {
      actor.Perform(Execute.TheJavaScript<SetAnElementValue>()
                    .WithTheParameters(element, value)
                    .AndIgnoreTheResult());
    }

    public SetElementValueWithScript(IWebElement element, string value)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      this.element = element;
      this.value = value ?? String.Empty;
    }
  }
}
