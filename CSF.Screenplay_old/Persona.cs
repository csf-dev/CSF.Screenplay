//
// Persona.cs
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
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
  /// <summary>
  /// Derived types may use this as a template with which to create actor instances.
  /// </summary>
  public abstract class Persona : IPersona, IGrantsResolvedAbilities
  {
    /// <summary>
    /// Gets the actor's name.
    /// </summary>
    /// <value>The name.</value>
    public abstract string Name { get; }

    /// <summary>
    /// Grants abilities to the actor.
    /// </summary>
    /// <param name="actor">An actor.</param>
    /// <param name="resolver">A service resolver, for resolving ability instances.</param>
    public virtual void GrantAbilities(ICanReceiveAbilities actor, IResolvesServices resolver) { /* Intentional no-op */ }
  }
}
