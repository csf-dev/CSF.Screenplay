//
// CastPersonaExtensions.cs
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

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods for an <see cref="ICast"/> relating to the use of personas.
  /// </summary>
  public static class CastPersonaExtensions
  {
    /// <summary>
    /// Gets an <see cref="IActor"/> from the given cast, making use of an <see cref="IPersona"/> type.
    /// </summary>
    /// <param name="cast">Cast.</param>
    /// <typeparam name="TPersona">The 1st type parameter.</typeparam>
    public static IActor Get<TPersona>(this ICast cast) where TPersona : class,IPersona,new()
    {
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));

      var persona = new TPersona();
      if(persona is IGrantsResolvedAbilities)
        return GetActorAndGrantAbilities(cast, persona.Name, (IGrantsResolvedAbilities) persona);

      return cast.Get(persona.Name);
    }

    static IActor GetActorAndGrantAbilities(ICast cast, string name, IGrantsResolvedAbilities abilityGrantingService)
    {
      return cast.Get(name, (r, a) => abilityGrantingService.GrantAbilities(a, r));
    }
  }
}
