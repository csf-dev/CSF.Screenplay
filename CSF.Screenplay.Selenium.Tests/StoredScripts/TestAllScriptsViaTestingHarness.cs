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
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Tests.Personas;
using CSF.Screenplay.Selenium.Tests.Tasks;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.StoredScripts
{
  [TestFixture]
  [Description("The JavaScript resources stored within the main assembly")]
  public class TestAllScriptsViaTestingHarness
  {
    [Test,Screenplay]
    [Description("Every script should pass a Jasmine test suite")]
    public void Every_script_in_the_main_assembly_must_pass_its_Jasmine_test_suite(ICast cast)
    {
      var joe = cast.Get<Joe>();

      var scriptTypes = Given(joe).Got(AllOfTheExecutableScriptTypes.FromTheMainAssembly());
      var results = When(joe).Gets(AllOfTheScriptTestResults.ForTheScriptTypes(scriptTypes));
      Then(joe).Should(VerifyThatAllOfTheScriptTestsPassed.ForTheResults(results));
    }
  }
}
