//
// TimespanBuilderTests.cs
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
using CSF.Screenplay.Builders;
using NUnit.Framework;

namespace CSF.Screenplay.Tests.Builders
{
  [TestFixture]
  public class TimespanBuilderTests
  {
    [Test,AutoMoqData]
    public void Milliseconds_can_build_a_matching_timespan(int value, SampleBuilder otherBuilder)
    {
      // Arrange
      var builder = TimespanBuilder.Create(value, otherBuilder);
      var timespanProvider = (IProvidesTimespan) builder;

      // Act
      builder.Milliseconds();

      // Assert
      Assert.That(timespanProvider.GetTimespan(), Is.EqualTo(TimeSpan.FromMilliseconds(value)));
    }

    [Test,AutoMoqData]
    public void Seconds_can_build_a_matching_timespan(int value, SampleBuilder otherBuilder)
    {
      // Arrange
      var builder = TimespanBuilder.Create(value, otherBuilder);
      var timespanProvider = (IProvidesTimespan) builder;

      // Act
      builder.Seconds();

      // Assert
      Assert.That(timespanProvider.GetTimespan(), Is.EqualTo(TimeSpan.FromSeconds(value)));
    }

    [Test,AutoMoqData]
    public void Minutes_can_build_a_matching_timespan(int value, SampleBuilder otherBuilder)
    {
      // Arrange
      var builder = TimespanBuilder.Create(value, otherBuilder);
      var timespanProvider = (IProvidesTimespan) builder;

      // Act
      builder.Minutes();

      // Assert
      Assert.That(timespanProvider.GetTimespan(), Is.EqualTo(TimeSpan.FromMinutes(value)));
    }

    [Test,AutoMoqData]
    public void Milliseconds_returns_other_builder(int value, SampleBuilder otherBuilder)
    {
      // Arrange
      var builder = TimespanBuilder.Create(value, otherBuilder);

      // Act
      var result = builder.Milliseconds();

      // Assert
      Assert.That(result, Is.SameAs(otherBuilder));
    }

    [Test,AutoMoqData]
    public void Seconds_returns_other_builder(int value, SampleBuilder otherBuilder)
    {
      // Arrange
      var builder = TimespanBuilder.Create(value, otherBuilder);

      // Act
      var result = builder.Seconds();

      // Assert
      Assert.That(result, Is.SameAs(otherBuilder));
    }

    [Test,AutoMoqData]
    public void Minutes_returns_other_builder(int value, SampleBuilder otherBuilder)
    {
      // Arrange
      var builder = TimespanBuilder.Create(value, otherBuilder);

      // Act
      var result = builder.Minutes();

      // Assert
      Assert.That(result, Is.SameAs(otherBuilder));
    }

    public class SampleBuilder {}
  }
}
