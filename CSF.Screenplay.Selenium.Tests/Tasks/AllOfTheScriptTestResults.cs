//
// GetAllOfTheScriptTestResults.cs
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
using System.Text.RegularExpressions;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.StoredScripts;
using CSF.Screenplay.Selenium.Tests.Pages;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  public class AllOfTheScriptTestResults : Question<IReadOnlyCollection<ScriptTestResult>>
  {
    const string JasminePassedClassPattern = @"\bjasmine-passed\b";
    static readonly Regex PassMatcher = new Regex(JasminePassedClassPattern, RegexOptions.Compiled);

    readonly IReadOnlyCollection<Type> types;

    protected override string GetReport(INamed actor) => $"{actor.Name} runs all of the Jasmine script tests and gets their results";

    protected override IReadOnlyCollection<ScriptTestResult> PerformAs(IPerformer actor)
      => GetResults(actor).ToArray();

    IEnumerable<ScriptTestResult> GetResults(IPerformer actor)
    {
      foreach(var type in types)
        yield return GetSingleScriptTestResult(GetTheScript(type), actor);
    }

    ScriptTestResult GetSingleScriptTestResult(IProvidesScript theScript, IPerformer actor)
    {
      var theTestPage = ScriptTestingHarness.For(theScript);
      actor.Perform(OpenTheirBrowserOn.ThePage(theTestPage));

      actor.Perform(Wait.ForAtMost(5).Seconds().OrUntil(ScriptTestingHarness.TheResultsBar).IsVisible());

      var classAttribute = actor.Perform(TheAttribute.Named("class").From(ScriptTestingHarness.TheResultsBar));
      var testsPassed = PassMatcher.IsMatch(classAttribute);

      return new ScriptTestResult(theScript, testsPassed);
    }

    IProvidesScript GetTheScript(Type theScriptType)
      => (IProvidesScript) Activator.CreateInstance(theScriptType);

    public AllOfTheScriptTestResults(IReadOnlyCollection<Type> types)
    {
      if(types == null)
        throw new ArgumentNullException(nameof(types));

      this.types = types;
    }

    public static IQuestion<IReadOnlyCollection<ScriptTestResult>> ForTheScriptTypes(IReadOnlyCollection<Type> types)
      => new AllOfTheScriptTestResults(types);
  }
}
