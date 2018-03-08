//
// GetAllOfTheScriptTypes.cs
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
using CSF.Screenplay.Selenium.ScriptResources;
using CSF.Screenplay.Selenium.StoredScripts;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  public class AllOfTheExecutableScriptTypes : Question<IReadOnlyCollection<Type>>
  {
    protected override string GetReport(INamed actor)
      => $"{actor.Name} gets all of the executable script provider types from the main assembly.";

    protected override IReadOnlyCollection<Type> PerformAs(IPerformer actor)
    {
      var assembly = typeof(IProvidesScript).Assembly;
      return assembly.GetExportedTypes().Where(IsExecutableScriptType).ToArray();
    }

    bool IsExecutableScriptType(Type type)
    {
      if(!typeof(IProvidesScript).IsAssignableFrom(type)) return false;
      if(!type.IsClass || type.IsAbstract) return false;
      if(typeof(IInvokesScripts).IsAssignableFrom(type)) return false;
      return true;
    }

    public static IQuestion<IReadOnlyCollection<Type>> FromTheMainAssembly()
      => new AllOfTheExecutableScriptTypes();
  }
}
