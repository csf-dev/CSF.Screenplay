//
// SetTheDateUsingAJavaScriptWorkaround.cs
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
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tasks
{
  /// <summary>
  /// Sets the value of an HTML <c>&lt;input type="date"&gt;</c> element, using a JavaScript workaround.
  /// </summary>
  public class SetTheDateUsingAJavaScriptWorkaround : Performable
  {
    readonly DateTime date;
    readonly ITarget target;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(Actors.INamed actor)
      => $"{actor.Name} enters the date {date.ToString("yyyy-MM-dd")} into {target.GetName()} using a JavaScript workaround";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(Actors.IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
      var webElement = target.GetWebElementAdapter(browseTheWeb);

      actor.Perform(Execute.JavaScript.WhichSetsTheValueOf(webElement).To(date.ToString("yyyy-MM-dd")));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Tasks.SetTheDateUsingAJavaScriptWorkaround"/> class.
    /// </summary>
    /// <param name="date">Date.</param>
    /// <param name="target">Target.</param>
    public SetTheDateUsingAJavaScriptWorkaround(DateTime date, ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.date = date;
      this.target = target;
    }
  }
}
