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
  [Description("The JavaScripts in the CSF.Screenplay.Selenium.JavaScriptWorkarounds project")]
  public class TestAllScriptsViaTestingHarness
  {
    [Test,Screenplay]
    [Description("Every script should pass a Jasmine test suite")]
    public void Every_script_in_the_main_assembly_must_pass_its_Jasmine_test_suite(ICast cast)
    {
      /* Please note that this single test scenario will test every available script using all of the
       * available Jasmine test suites. Those Jasmine tests themselves are not held within this project.
       * 
       * They are all written using JavaScript and are found within the CSF.Screenplay.WebTestWebsite project,
       * at the path:
       *   Scripts/script-tests/
       * Each class in the CSF.Screenplay.Selenium.JavaScriptWorkarounds project has its own test suite in that
       * directory, named after the name of its class, with the suffix 'tests.js'.
       * 
       * If further JavaScript workarounds are added then each should have its own test suite in that path.
       */
      var joe = cast.Get<Joe>();

      var scriptTypes = Given(joe).Got(AllOfTheExecutableScriptTypes.FromTheMainAssembly());
      var results = When(joe).Gets(AllOfTheScriptTestResults.ForTheScriptTypes(scriptTypes));
      Then(joe).Should(VerifyThatAllOfTheScriptTestsPassed.ForTheResults(results));
    }
  }
}
