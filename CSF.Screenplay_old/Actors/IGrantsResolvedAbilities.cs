//
// IGrantsResolvedAbilities.cs
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
using CSF.FlexDi;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// A service which can grant a set of abilities to an actor, where those abilities may be resolved from an
  /// <see cref="IResolvesServices"/>.
  /// </summary>
  public interface IGrantsResolvedAbilities
  {
    /// <summary>
    /// Grants abilities to the actor.
    /// </summary>
    /// <param name="actor">An actor.</param>
    /// <param name="resolver">A service resolver, for resolving ability instances.</param>
    void GrantAbilities(ICanReceiveAbilities actor, IResolvesServices resolver);
  }
}
