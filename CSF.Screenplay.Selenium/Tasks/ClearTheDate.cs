//
// ClearTheDateField.cs
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
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tasks
{
  /// <summary>
  /// A task which clears the value from an HTML <c>&lt;input type="date"&gt;</c> element.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This is required because web browsers which have the flag
  /// <see cref="Flags.HtmlElements.InputTypeDate.RequiresInputViaJavaScriptWorkaround"/> cannot use the 'normal'
  /// mechanism to clear the value of a date field.  This task abstracts around browsers that can or can't do
  /// that and uses a JavaScript workaround to clear dates when the browser couldn't otherwise do it.
  /// </para>
  /// </remarks>
  public class ClearTheDate : Performable
  {
    readonly ITarget target;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} clears the date from {target.GetName()}";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

      if(browseTheWeb.FlagsDriver.HasFlag(Flags.HtmlElements.InputTypeDate.RequiresInputViaJavaScriptWorkaround))
        actor.Perform(ClearTheTargetUsingAJavaScriptWorkaround(browseTheWeb));
      else
        actor.Perform(Clear.TheContentsOf(target));
    }

    IPerformable ClearTheTargetUsingAJavaScriptWorkaround(BrowseTheWeb browseTheWeb)
    {
      var webElement = target.GetWebElementAdapter(browseTheWeb);
      return Execute.JavaScript.WhichSetsTheValueOf(webElement).To(String.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Tasks.ClearTheDate"/> class.
    /// </summary>
    /// <param name="target">Target.</param>
    public ClearTheDate(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      
      this.target = target;
    }
  }
}
