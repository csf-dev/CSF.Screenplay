//
// ScriptTestingHarness.cs
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
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.StoredScripts;

namespace CSF.Screenplay.Selenium.Tests.Pages
{
  public class ScriptTestingHarness : Page
  {
    readonly IProvidesScript scriptProvider;

    public override string GetName() => $"the JavaScript testing harness for \"{scriptProvider.Name}\"";

    public override IUriProvider GetUriProvider()
      => new AppUri($"StoredScriptTest/Index/{scriptProvider.GetType().Name}");

    public static ILocatorBasedTarget TheResultsBar
      => new CssSelector(".jasmine_html-reporter .jasmine-bar", "the Jasmine results bar");

    public ScriptTestingHarness(IProvidesScript scriptProvider)
    {
      if(scriptProvider == null)
        throw new ArgumentNullException(nameof(scriptProvider));

      this.scriptProvider = scriptProvider;
    }

    public static ScriptTestingHarness For<TScript>() where TScript : IProvidesScript, new()
      => new ScriptTestingHarness(new TScript());

    public static ScriptTestingHarness For(Type scriptType)
      => new ScriptTestingHarness((IProvidesScript) Activator.CreateInstance(scriptType));

    public static ScriptTestingHarness For(IProvidesScript script)
      => new ScriptTestingHarness(script);
  }
}
