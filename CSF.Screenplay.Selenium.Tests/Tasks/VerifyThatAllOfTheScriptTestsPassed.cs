//
// VerifyThatAllOfTheScriptTestsPassed.cs
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
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using NUnit.Framework;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  public class VerifyThatAllOfTheScriptTestsPassed : Performable
  {
    readonly IReadOnlyCollection<ScriptTestResult> results;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} verifies that all of the script tests were successful";

    protected override void PerformAs(IPerformer actor)
    {
      var failures = results.Where(x => !x.Success).ToArray();

      Assert.That(failures,
                  Has.Length.Zero,
                  "The following scripts failed testing:{0}    {1}",
                  Environment.NewLine,
                  String.Join($"{Environment.NewLine}    ", failures.Select(x => x.Script.Name).ToArray()));
    }

    public VerifyThatAllOfTheScriptTestsPassed(IReadOnlyCollection<ScriptTestResult> results)
    {
      if(results == null)
        throw new ArgumentNullException(nameof(results));

      this.results = results;
    }

    public static IPerformable ForTheResults(IReadOnlyCollection<ScriptTestResult> results)
      => new VerifyThatAllOfTheScriptTestsPassed(results);
  }
}
