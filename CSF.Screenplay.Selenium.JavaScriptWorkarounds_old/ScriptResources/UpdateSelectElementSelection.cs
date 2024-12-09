//
// UpdateSelectElementSelection.cs
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

namespace CSF.Screenplay.Selenium.ScriptResources
{
  /// <summary>
  /// A stored JavaScript which sets the selected option(s) for an HTML <c>&lt;select&gt;</c> element.
  /// </summary>
  public class UpdateSelectElementSelection : ScriptResource
  {
    /// <summary>
    /// Gets the action name for deselecting every option.
    /// </summary>
    public static readonly string DeselectAllActionName = "deselectAll";

    /// <summary>
    /// Gets the action name for selecting an option by its zero-based index.
    /// </summary>
    public static readonly string SelectByIndexActionName = "selectByIndex";

    /// <summary>
    /// Gets the action name for selecting an option by its value.
    /// </summary>
    public static readonly string SelectByValueActionName = "selectByValue";

    /// <summary>
    /// Gets the action name for selecting an option by its displyed text.
    /// </summary>
    public static readonly string SelectByTextActionName = "selectByText";

    /// <summary>
    /// Gets the action name for deselecting an option by its zero-based index.
    /// </summary>
    public static readonly string DeselectByIndexActionName = "deselectByIndex";

    /// <summary>
    /// Gets the action name for deselecting an option by its value.
    /// </summary>
    public static readonly string DeselectByValueActionName = "deselectByValue";

    /// <summary>
    /// Gets the action name for deselecting an option by its displayed text.
    /// </summary>
    public static readonly string DeselectByTextActionName = "deselectByText";

    /// <summary>
    /// Gets the name of this script.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "a JavaScript to set the select options of an HTML <select> element";

    /// <summary>
    /// Gets a collection of scripts which the current script instance depends upon.
    /// </summary>
    /// <returns>The dependencies.</returns>
    protected override ScriptResource[] GetDependencies() => new [] { new ArgumentsArrayValidator() };
  }
}
