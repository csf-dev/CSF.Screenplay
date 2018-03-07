//
// TestAllScriptsViaTestingHarness.cs
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
using System.Linq;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using CSF.Screenplay.Selenium.Tests.Personas;
using CSF.Screenplay.Selenium.Tests.Tasks;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.StoredScripts
{
  [TestFixture]
  [Description("A test for every stored script included in the main assembly, via the Jasmine testing harness.")]
  public class TestAllScriptsViaTestingHarness
  {
    [Test,Screenplay]
    [Description("Run every script in the main assembly through the Jasmine test harness and verify that they all pass")]
    public void Every_script_in_the_main_assembly_must_pass_its_Jasmine_test_suite(ICast cast)
    {
      var joe = cast.Get<Joe>();

      var scriptTypes = Given(joe).Got(AllOfTheScriptTypes.FromTheMainAssembly());
      var results = When(joe).Gets(AllOfTheScriptTestResults.ForTheScriptTypes(scriptTypes));
      Then(joe).Should(VerifyThatAllOfTheScriptTestsPassed.ForTheResults(results));
    }
  }
}
