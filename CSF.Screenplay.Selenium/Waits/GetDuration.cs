//
// GetWaitDuration.cs
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
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;

namespace CSF.Screenplay.Selenium.Waits
{
  static class GetDuration
  {
    public static TimeSpan ToWait(TimeSpan? specifiedWaitTime, INamed actor)
      => ToWait(specifiedWaitTime, actor as IPerformer);

    public static TimeSpan ToWait(TimeSpan? specifiedWaitTime, IPerformer actor)
      => specifiedWaitTime ?? GetActorWaitTime(actor) ?? GetDefaultWaitTime();

    static TimeSpan GetDefaultWaitTime() => ScreenplayConstants.DefaultWait;

    static TimeSpan? GetActorWaitTime(IPerformer actor)
    {
      if(actor == null) return null;
      if(!actor.HasAbility<ChooseADefaultWaitTime>()) return null;
      var ability = actor.GetAbility<ChooseADefaultWaitTime>();
      return ability.Timeout;
    }
  }
}
