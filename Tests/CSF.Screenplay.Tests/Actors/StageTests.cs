//
// StageTests.cs
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
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.Screenplay.Tests.Actors
{
  [TestFixture]
  public class StageTests
  {
    [Test,AutoMoqData]
    public void ShineTheSpotlightOn_persona_uses_cast_to_get_actor([Frozen] ICast cast,
                                                                   IActor actor,
                                                                   Stage sut)
    {
      // Arrange
      Mock.Get(cast).Setup(x => x.Get<MyPersona>()).Returns(actor);

      // Act
      var result = sut.ShineTheSpotlightOn<MyPersona>();

      // Assert
      Assert.That(result, Is.SameAs(actor));
    }

    [Test,AutoMoqData]
    public void ShineTheSpotlightOn_persona_records_actor_in_spotlight([Frozen] ICast cast,
                                                                       IActor actor,
                                                                       Stage sut)
    {
      // Arrange
      Mock.Get(cast).Setup(x => x.Get<MyPersona>()).Returns(actor);

      // Act
      var first = sut.ShineTheSpotlightOn<MyPersona>();
      var second = sut.GetTheActorInTheSpotlight();

      // Assert
      Assert.That(first, Is.SameAs(second));
    }

    public class MyPersona : IPersona
    {
      public string Name => "Bob";
    }
  }
}
