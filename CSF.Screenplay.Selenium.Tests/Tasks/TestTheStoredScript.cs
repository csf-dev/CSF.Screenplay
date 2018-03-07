//
// TestTheStoredScript.cs
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
using System.Text.RegularExpressions;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.StoredScripts;
using CSF.Screenplay.Selenium.Tests.Pages;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  public class TestTheStoredScript : Question<ScriptTestResult>
  {
    const string JasminePassedClassPattern = @"\bjasmine-passed\b";
    static readonly Regex PassMatcher = new Regex(JasminePassedClassPattern, RegexOptions.Compiled);

    readonly IProvidesScript script;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} gets the result for the script \"{script.Name}\"";

    protected override ScriptTestResult PerformAs(IPerformer actor)
    {
      var theTestPage = ScriptTestingHarness.For(script);
      actor.Perform(OpenTheirBrowserOn.ThePage(theTestPage));

      actor.Perform(Wait.ForAtMost(5).Seconds().OrUntil(ScriptTestingHarness.TheResultsBar).IsVisible());

      var classAttribute = actor.Perform(TheAttribute.Named("class").From(ScriptTestingHarness.TheResultsBar));
      var testsPassed = PassMatcher.IsMatch(classAttribute);

      return new ScriptTestResult(script, testsPassed);
    }

    public TestTheStoredScript(IProvidesScript script)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));

      this.script = script;
    }

    public static IQuestion<ScriptTestResult> OfType(Type scriptType)
      => new TestTheStoredScript(GetTheScript(scriptType));

    static IProvidesScript GetTheScript(Type theScriptType)
      => (IProvidesScript) Activator.CreateInstance(theScriptType);
  }
}
