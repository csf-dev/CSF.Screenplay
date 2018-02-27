//
// ScreenplaySteps.cs
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
using CSF.Screenplay.Actors;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Convenience subclass of <c>TechTalk.SpecFlow.Steps</c> making it possible to call <c>Given, When &amp; Then</c>
  /// methods without conflicting with the built-in methods of the same names.
  /// </summary>
  public class ScreenplaySteps : Steps
  {
    /// <summary>
    /// Returns the actor instance, as an <see cref="IGivenActor"/>, in order to perform precondition actions.
    /// </summary>
    /// <param name="actor">The actor.</param>
    public IGivenActor Given(IActor actor) => StepComposer.Given(actor);

    /// <summary>
    /// Returns the actor instance, as an <see cref="IWhenActor"/>, in order to perform actions which exercise the
    /// application.
    /// </summary>
    /// <param name="actor">The actor.</param>
    public IWhenActor When(IActor actor) => StepComposer.When(actor);

    /// <summary>
    /// Returns the actor instance, as an <see cref="IThenActor"/>, in order to perform actions which asserts that
    /// the desired outcome has come about.
    /// </summary>
    /// <param name="actor">The actor.</param>
    public IThenActor Then(IActor actor) => StepComposer.Then(actor);
  }
}
