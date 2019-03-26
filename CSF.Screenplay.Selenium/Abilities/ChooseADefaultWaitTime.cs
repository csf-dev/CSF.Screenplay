//
// SetADefaultWaitTime.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2019 Craig Fowler
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
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Selenium.Abilities
{
  /// <summary>
  /// An ability which allows an actor to specify/choose a default amount of time to wait.
  /// This default waiting-time applies to all web-driver-wait scenarios where no waiting time has been specified.
  /// Actors without this ability use a hard-coded default of ten seconds for waits.
  /// </summary>
  public class ChooseADefaultWaitTime : Ability
  {
    readonly TimeSpan timeout;

    /// <summary>
    /// Gets the wait timeout.
    /// </summary>
    /// <value>The timeout.</value>
    public TimeSpan Timeout => timeout;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChooseADefaultWaitTime"/> class.
    /// </summary>
    /// <param name="timeout">Timeout.</param>
    public ChooseADefaultWaitTime(TimeSpan timeout)
    {
      this.timeout = timeout;
    }

    /// <summary>
    /// Static builder function to get a new instance of this ability.
    /// </summary>
    /// <returns>An instance of the ability.</returns>
    /// <param name="timeout">Timeout.</param>
    public static ChooseADefaultWaitTime Of(TimeSpan timeout) => new ChooseADefaultWaitTime(timeout);
  }
}
