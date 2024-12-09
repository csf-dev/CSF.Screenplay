//
// GetALocalisedDate.cs
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
  /// Script resource for getting a localised date string
  /// </summary>
  public class GetALocalisedDate : ScriptResource
  {
    readonly ScriptResource argsValidator;

    /// <summary>
    /// Gets the name of this script.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "a JavaScript which converts a date to a locale-formatted string";

    /// <summary>
    /// Gets a collection of scripts which the current script instance depends upon.
    /// </summary>
    /// <returns>The dependencies.</returns>
    protected override ScriptResource[] GetDependencies() => new [] { argsValidator };

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.ScriptResources.GetALocalisedDate"/> class.
    /// </summary>
    public GetALocalisedDate() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.ScriptResources.GetALocalisedDate"/> class.
    /// </summary>
    /// <param name="argsValidator">Arguments validator.</param>
    public GetALocalisedDate(ScriptResource argsValidator)
    {
      this.argsValidator = argsValidator ?? new ArgumentsArrayValidator();
    }
  }
}
